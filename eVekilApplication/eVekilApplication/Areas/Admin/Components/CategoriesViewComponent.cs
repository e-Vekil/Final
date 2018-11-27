using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Areas.Admin.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly EvekilDb _db;
        public CategoriesViewComponent(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> Categories = await _db.Categories.ToListAsync();
            return View(Categories);
        }
    }
}