using eVekilApplication.Data;
using eVekilApplication.Models;
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
            List<Document> documents;
            documents = await _db.Documents.Where(d=>d.Subcategory.CategoryId == id).Include(d=>d.Advocate).Include(d=>d.Subcategory).ThenInclude(d=>d.Category).ToListAsync();
            return View(documents);
        }
    }
}
