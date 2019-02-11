using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Infrastructure.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ConnectionController : Controller
    {
        private readonly EvekilDb db;
        public ConnectionController(EvekilDb _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var connections = await db.Connects.OrderByDescending(c => c.SendDate).ToListAsync();
            return View(connections);
        }

        [HttpGet]
        public async Task<IActionResult> Reply(int id)
        {
            var user =await db.Connects.FindAsync(id);
            ViewBag.ReplyId = id;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(int id,[FromServices] EmailService service)
        {
            var user = await db.Connects.Where(u => u.Id == id).FirstOrDefaultAsync();
            var replyText = Request.Form["ReplyText"].ToString();

            if (user.Email == "")
            {
                ModelState.AddModelError("","User Mail duzgun deyil");
                return View();
            }

            if (replyText== "")
            {
                ModelState.AddModelError("", "Reply duzgun deyil");
                return View();
            }
          
            await service.SendMailAsync(user.Email, "E-VEKIL.AZ INFO", replyText);
            user.Status = true;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(List));


        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await db.Connects.FindAsync(id);
            db.Connects.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}