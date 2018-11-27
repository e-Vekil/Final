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
    public class SubcategoriesViewComponent : ViewComponent
    {
        private readonly EvekilDb _db;
        public SubcategoriesViewComponent(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Subcategory> Subcategories = await _db.Subcategories.ToListAsync();
            return View(Subcategories);
        }
    }
}