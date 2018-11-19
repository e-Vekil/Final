using System;
using System.Collections.Generic;
using System.IO;
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
    public class DocumentsController : Controller
    {
        private EvekilDb _db;
        public DocumentsController(EvekilDb db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Document> Documents;

            using (_db)
            {
                Documents = _db.Documents.OrderBy(x => x.Date).ToList();
            }

            return View(Documents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Document document)
        {
            if (ModelState.IsValid)
            {
                string filename = "";

                if (document.File.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || document.File.ContentType == "application/msword")
                {
                    filename = Guid.NewGuid() + document.File.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        document.File.CopyTo(fs);
                    }

                }


                var advocatename = Request.Form["advocates"].ToString();
                Advocate advocate = await _db.Advocates.Where(x => x.Email == advocatename).FirstOrDefaultAsync();

                var categoryid = Int32.Parse(Request.Form["categories"].ToString());
                Subcategory subc = await _db.Subcategories.Where(x => x.Id == categoryid).FirstOrDefaultAsync();

                document.Subcategory = subc;
                document.Advocate = advocate;
                document.FileName = filename;
                document.Date = DateTime.Now;

                await _db.Documents.AddAsync(document);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            else
                return View();
        }

    }
}