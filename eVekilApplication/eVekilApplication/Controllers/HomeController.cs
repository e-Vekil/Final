using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly EvekilDb _db;
        public HomeController(EvekilDb db)
        {
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
            int count = await _db.Comments.Where(x => x.DocumentId == id).CountAsync();
            ViewBag.commentCount = count;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendReview(CommentViewModel cm)
        {
            
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}