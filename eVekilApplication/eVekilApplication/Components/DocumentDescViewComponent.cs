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
    public class DocumentDescViewComponent:ViewComponent
    {
        private readonly EvekilDb _db;
        public DocumentDescViewComponent(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            DocumentDescViewModel dm = new DocumentDescViewModel();
            CommentViewModel cm = new CommentViewModel();
            Document document = await _db.Documents.Where(d => d.Id == id).Include(d => d.Subcategory).ThenInclude(d => d.Category).FirstOrDefaultAsync();
            cm.Comments = await _db.Comments.OrderByDescending(c=>c.Id).Where(c=>c.DocumentId == id).Include(c => c.User).Include(c => c.Document).ToListAsync();
            
            dm.Cm = cm;
            dm.Document = document;

            return View(dm);
        }
    }
}
