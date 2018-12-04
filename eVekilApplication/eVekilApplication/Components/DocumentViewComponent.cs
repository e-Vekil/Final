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
    public class DocumentViewComponent:ViewComponent
    {
        private readonly EvekilDb _db;
        public DocumentViewComponent(EvekilDb db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            DocumentViewModel dm = new DocumentViewModel();
            dm.Documents = await _db.Documents.Where(d => d.Subcategory.CategoryId == id).Include(d => d.Advocate).Include(d => d.Subcategory).ThenInclude(d => d.Category).ToListAsync();
            dm.category = await _db.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            return View(dm);
        }
    }
}
