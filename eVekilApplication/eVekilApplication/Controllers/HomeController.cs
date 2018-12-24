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

        public async Task<IActionResult> Document(int id,int subId,int page=1)
        {
            DocumentViewModel dm = new DocumentViewModel();
            if ((id == 0 && subId != 0) || (id != 0 && subId != 0))
            {
                dm.Documents = await _db.Documents.Where(d => d.SubcategoryId == subId).Skip((page - 1) * 6).Take(6).Include(d => d.Advocate).Include(d => d.Subcategory).ThenInclude(d => d.Category).ToListAsync();
                Subcategory sub = await _db.Subcategories.Where(s => s.Id == subId).FirstOrDefaultAsync();
                dm.Category = await _db.Categories.Where(c => c.Id == sub.CategoryId).FirstOrDefaultAsync();
                dm.Subcategories = await _db.Subcategories.Where(s => s.CategoryId == sub.CategoryId).ToListAsync();
                ViewBag.Total = Math.Ceiling(_db.Documents.Where(d => d.SubcategoryId == subId).Count() / 6.0);
                ViewBag.SubId = subId;
                ViewBag.FilterName = sub.Name;
                ViewBag.DocumentCount = dm.Documents.Count();
            }
            else if(id !=0 && subId == 0)
            {
                dm.Documents = await _db.Documents.Where(d => d.Subcategory.CategoryId == id).Skip((page - 1) * 6).Take(6).Include(d => d.Advocate).Include(d => d.Subcategory).ThenInclude(d => d.Category).ToListAsync();
                dm.Subcategories = await _db.Subcategories.Where(s=>s.CategoryId == id).OrderBy(x => x.Name).ToListAsync();
                dm.Category = await _db.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
                ViewBag.Total = Math.Ceiling(_db.Documents.Where(d => d.Subcategory.CategoryId == id).Count() / 6.0);
                ViewBag.SubId = 0;
                ViewBag.DocumentCount = dm.Documents.Count();
            }

            ViewBag.Page = page;
            return View(dm);
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