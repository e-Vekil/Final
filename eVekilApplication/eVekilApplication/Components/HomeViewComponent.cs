using eVekilApplication.Data;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Components
{
    public class HomeViewComponent:ViewComponent
    {
        private readonly EvekilDb _db;
        public HomeViewComponent(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HomeViewModel hm = new HomeViewModel();
                var categories = await _db.Categories.ToListAsync();
                var subcategories = await _db.Subcategories.Include(s=>s.Category).ToListAsync();
                hm.Categories = categories;
                hm.Subcategories = subcategories;
            return View(hm);
        }
    }
}
