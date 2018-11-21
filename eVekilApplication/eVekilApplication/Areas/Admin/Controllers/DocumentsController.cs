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
                Documents = _db.Documents.Include(x=>x.Advocate).Include(x=>x.Subcategory).ThenInclude(x=>x.Category).OrderBy(x => x.Date).ToList();
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Document document;
            using (_db)
            {
                document = await _db.Documents.Include(x=>x.Advocate).Include(x=>x.Subcategory).Where(d => d.Id == id).FirstOrDefaultAsync();
            }
            return View(document);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Document doc)
        {
            Document document;
            string advocateName = Request.Form["advocates"].ToString();
            Advocate advocate =  await _db.Advocates.Where(x => x.Email == advocateName).FirstOrDefaultAsync();
            doc.Advocate = advocate;
            doc.AdvocateId = advocate.Id;
            string subcategoryName = Request.Form["Categories"].ToString();
            Subcategory subcategory =  await _db.Subcategories.Where(s => s.Name == subcategoryName).FirstOrDefaultAsync();
            doc.Subcategory = subcategory;
            doc.SubcategoryId = subcategory.Id;
            if(Request.Form["File"].ToString() != "")
            {
                doc.FileName = doc.File.FileName;
            }
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    document = await _db.Documents.FindAsync(doc.Id);
                    if (document.File!=null)
                    {
                        document.File = doc.File;
                    }
                    if (document != null)
                    {
                        if (document.File.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || document.File.ContentType == "application/msword")
                        {
                            string deletedFileName = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads", Request.Form["DeletedFile"].ToString());
                            if(Request.Form["File"].ToString() != null)
                            {
                                if (!System.IO.File.Exists(deletedFileName))
                                {
                                    string newfilename = Guid.NewGuid() + doc.FileName;

                                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newfilename);

                                    using (FileStream fs = new FileStream(path, FileMode.Create))
                                    {
                                        await document.File.CopyToAsync(fs);
                                    }
                                }
                            }

                        }
                        
                        document.Name = doc.Name;
                        document.TermsOfUse = doc.TermsOfUse;
                        document.FileName = doc.File.FileName;
                        document.File = doc.File;

                       await  _db.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("", "This document is not exists!");
                    }

                }

                return RedirectToAction(nameof(List));
            }
            else
            {
                ModelState.AddModelError("", "Something is wrong");
                return View();
            }

            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Document doc;
            using (_db)
            {
                doc =  await _db.Documents.Include(x=>x.Advocate).Where(d => d.Id == id).FirstOrDefaultAsync();
                if (doc != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", doc.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    _db.Remove(doc);
                }
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }

    }
}