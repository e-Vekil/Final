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
    public class SubCategoryController : Controller
    {
        private readonly EvekilDb _db;
        public SubCategoryController(EvekilDb db)
        {
            _db = db;
        }

        public async Task<IActionResult> List()
        {
            List<Subcategory> subcategories;
            using (_db)
            {
                subcategories = await _db.Subcategories.Include(x=>x.Category).OrderBy(x => x.Name).ToListAsync();
            }
            return View(subcategories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Subcategory subcat)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    string categoryName = Request.Form["CategoryName"].ToString();
                    Category category = await _db.Categories.Where(c => c.Name == categoryName).FirstOrDefaultAsync();

                    Subcategory subcategory = new Subcategory();
                    subcategory.Name = subcat.Name;
                    subcategory.Category = category;
                    subcategory.CategoryId = category.Id;
                    await _db.AddAsync(subcategory);
                    await _db.SaveChangesAsync();
                }
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
            Subcategory subcategory;
            //using (_db)
            //{
                subcategory = await _db.Subcategories.Where(c => c.Id == id).FirstOrDefaultAsync();
            //}
            return View(subcategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Subcategory subcat)
        {
            Subcategory subcategory;
            int id = subcat.Id;
            if (ModelState.IsValid)
            {
                //using (_db)
                //{
                    subcategory = await _db.Subcategories.Include(x=>x.Category).Where(x => x.Id == id).FirstOrDefaultAsync();
                    string categoryName = Request.Form["CategoryName"].ToString();
                    Category category = await _db.Categories.Where(c => c.Name == categoryName).FirstOrDefaultAsync();

                    subcategory.Name = subcat.Name;
                    subcategory.Category = category;
                    subcategory.CategoryId = category.Id;

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
            Subcategory subcategory;
            using (_db)
            {
                subcategory = await _db.Subcategories.FindAsync(id);
                _db.Remove(subcategory);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }
    }
}