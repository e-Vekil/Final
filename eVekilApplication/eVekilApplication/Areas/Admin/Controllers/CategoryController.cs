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
            //using (_db)
            //{
            categories = await _db.Categories.OrderBy(x => x.Visibilty).ToListAsync();
            //}
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Category cat)
        {
            if (ModelState.IsValid)
            {
                //using (_db)
                //{
                Category category = new Category();
                category.Name = cat.Name;
                category.Description = cat.Description;
                category.Visibilty = cat.Visibilty;
                await _db.AddAsync(category);
                await _db.SaveChangesAsync();
                //}
                return RedirectToAction(nameof(List));
            }
            else
            {
                ModelState.AddModelError("", "Something is wrong!");
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category category;
            //using (_db)
            //{
            category = await _db.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            //}
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category cat)
        {
            Category category;
            int id = cat.Id;
            if (ModelState.IsValid)
            {
                //using (_db)
                //{
                category = await _db.Categories.FindAsync(id);
                category.Name = cat.Name;
                category.Description = cat.Description;
                category.Visibilty = cat.Visibilty;
                await _db.SaveChangesAsync();
                //}
                return RedirectToAction(nameof(List));
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category;
            //using (_db)
            //{
            category = await _db.Categories.FindAsync(id);
            _db.Remove(category);
            await _db.SaveChangesAsync();

            //}
            return RedirectToAction(nameof(List));
        }

    }
}