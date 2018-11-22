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
                cm.Comments = await _db.Comments.Include(x=>x.User).Include(x=>x.Document).ToListAsync();
            }
            return View(cm);
        }

        public async Task<IActionResult> Accept()
        {
            return View();
        }
     
    }
}