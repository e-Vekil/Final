using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Infrastructure.Email;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly EvekilDb _db;
        private readonly UserManager<User> _userManager;
        public HomeController(UserManager<User> userManager, EvekilDb db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Document(int id)
        {
            ViewBag.categoryId = $"{id}";
            return View();
        }

        public async Task<IActionResult> DocumentDesc(int id)
        {
            ViewBag.documentId = $"{id}";
            int count = await _db.Comments.Where(x => x.DocumentId == id && x.Status == true).CountAsync();
            ViewBag.commentCount = count;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendReview(Comment cm,[FromServices]EmailService service)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return Content("Siz daxil olmamısınız!");

            int documentId = Convert.ToInt32(Request.Form["DocumentId"]);
            Document doc =  await _db.Documents.Where(d => d.Id == documentId).Include(d => d.Advocate).Include(d => d.Subcategory).ThenInclude(d => d.Category).FirstOrDefaultAsync();
            Comment comment = new Comment();
            comment.Document = doc;
            comment.DocumentId = documentId;
            comment.Text = cm.Text;
            comment.Date = DateTime.Now;
            comment.User = user;
            //string username = HttpContext.Session.GetString("id");
            //if (username != null)
            //{
            //    User user = await _db.Users.Where(u => u.Id == username).FirstOrDefaultAsync();
            //    comment.User = user;
            //}

            comment.Status = false;
            if (ModelState.IsValid)
            {
                await _db.Comments.AddAsync(comment);
                await _db.SaveChangesAsync();
                var message = $@"Istifadəçi : {HttpContext.Session.GetString("name")} 
Sənədin Adı:{doc.Name} 
 Comment:{comment.Text}";
                await service.SendMailAsync("ibrahimxanlimurad@hotmail.com","NEW COMMENT", message);
                return RedirectToAction("DocumentDesc",new { id = doc.Id });
            }
            else
            {
                ModelState.AddModelError("", "Sehv Bash Verdi");
                return View();

            }
        }


        [HttpGet    ]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                Comment comment = await _db.Comments.Where(c => c.Id == id).Include(c => c.User).FirstOrDefaultAsync();
                _db.Remove(comment);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
                return RedirectToAction(nameof(DocumentDesc));
            }
        }



        [HttpGet]
        public async Task<IActionResult> AddToShoppingCard(int id)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            Document doc = await _db.Documents.Include(x=>x.Advocate).Include(x=>x.Subcategory).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return RedirectToAction("Registration", "Account");
            }
            else
            {
                ShoppingCard SC = new ShoppingCard();
                SC.Document = doc;
                SC.DocumentId = doc.Id;
                SC.User = user;
                SC.UserId = user.Id;

                await _db.ShoppingCard.AddAsync(SC);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteFromShoppingCard(int id)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            Document doc = await _db.Documents.Include(x => x.Advocate).Include(x => x.Subcategory).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return RedirectToAction("Registration", "Account");
            }
            else
            {
                ShoppingCard SC = await _db.ShoppingCard.Where(x => x.Document == doc && x.UserId == user.Id).FirstOrDefaultAsync();

                _db.ShoppingCard.Remove(SC);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}