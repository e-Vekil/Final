using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly EvekilDb _db;
        public CategoryController(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IActionResult> List()
        {
            List<Category> categories;
            using (_db)
            {
                categories =  await _db.Categories.OrderBy(x=>x.Name).ToListAsync();
            }
            return View(categories);
        }
    }
}