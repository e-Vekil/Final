using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly EvekilDb _db;
        public CommentController(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IActionResult> List()
        {
            CommentViewModel cm = new CommentViewModel();
            using (_db)
            {
                cm.Comments = await _db.Comments.Include(x=>x.User).Include(x=>x.Document).OrderByDescending(x=>x.Id).ToListAsync();
            }
            return View(cm);
        }

        public async Task<IActionResult> Accept(int id)
        {
            try
            {
                Comment comment = await _db.Comments.Where(c => c.Id == id).Include(c => c.User).FirstOrDefaultAsync();
                comment.Status = true;
                await  _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
                return RedirectToAction(nameof(List));
            }


        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Comment comment = await _db.Comments.Where(c => c.Id == id).Include(c => c.User).FirstOrDefaultAsync();
                _db.Remove(comment);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
                return RedirectToAction(nameof(List));
            }


        }
    }
}