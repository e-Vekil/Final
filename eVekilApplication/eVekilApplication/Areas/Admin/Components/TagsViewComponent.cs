using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Areas.Admin.Components
{
    public class TagsViewComponent:ViewComponent
    {
        private readonly EvekilDb db;
        public TagsViewComponent(EvekilDb _db)
        {
            db = _db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            List<Tags> tags = await db.Tags.OrderBy(t => t.Tagname).ToListAsync();
            if (id == "add")
            {
                ViewBag.Edit = "false";
            }
            else
            {
                var docid = Convert.ToInt32(id);
                ViewBag.Edit = "true";
                ViewBag.DocId = docid;
            }
            return View(tags);
        }
    }
}
