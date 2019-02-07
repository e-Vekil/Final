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

        [HttpPost]
        public async Task<JsonResult> Search(string id)
        {

            if (id == null)
            {
                return Json(new { status = 400 });
            }


            var document = await _db.Documents.Where(d => d.Name.StartsWith(id)).ToListAsync();
            if (document.Count() == 0)
            {
                document = await _db.Documents.Where(d => d.Name.Contains(id)).ToListAsync();

                if (document.Count() == 0)
                {
                    document = await _db.Documents.Where(d => d.Subcategory.Name.Contains(id)).ToListAsync();
                }
                else
                {
                    return Json(new { status = 400 });
                }


            }

            return Json(new
            {
                status = 200,
                data = document.Select(d => new {
                    d.Name,
                    d.Id
                })
            });

        }
        [HttpGet]
        public IActionResult List(int page = 1)
        {
            List<Document> Documents;
            //using (_db)
            //{
                Documents = _db.Documents.Skip((page - 1) * 10).Take(10).Include(x=>x.Advocate).Include(x=>x.Subcategory).ThenInclude(x=>x.Category).OrderBy(x => x.Date).ToList();
            //}

            ViewBag.DocumentTotal = Math.Ceiling(_db.Documents.Count() / 10.0);
            ViewBag.DocumentPage = page;

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
                    if (!document.IsTemplated)
                    {
                        filename = Guid.NewGuid() + document.File.FileName;
                    }
                    else
                    {
                        filename = "Sablon" + Guid.NewGuid() + document.File.FileName;
                    }
                    string  path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            document.File.CopyTo(fs);
                        }

                    }
               
                var advocatename = Request.Form["advocates"].ToString();
                Advocate advocate = await _db.Advocates.Where(x => x.Email == advocatename).FirstOrDefaultAsync();

                var categoryid = Int32.Parse(Request.Form["categories"].ToString());
                Subcategory subc = await _db.Subcategories.Where(x => x.Id == categoryid).FirstOrDefaultAsync();

                List<string> tags = document.Tags.Split(',').ToList();

                if (tags != null)
                {
                    var count = 0;
                    foreach (var tag in tags)
                    {
                        Tags t = new Tags();
                        t.Tagname = tags[count];
                        t.Document = document;
                        await _db.Tags.AddAsync(t);
                        count++;
                    }
                }
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
            //using (_db)
            //{
                document = await _db.Documents.Include(x=>x.Advocate).Include(x=>x.Subcategory).Where(d => d.Id == id).FirstOrDefaultAsync();
            //}
            return View(document);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Document doc)
        {
            Document document = await _db.Documents.FindAsync(doc.Id);
            string advocateName = Request.Form["advocates"].ToString();
            Advocate advocate =  await _db.Advocates.Where(x => x.Email == advocateName).FirstOrDefaultAsync();
            document.Advocate = advocate;
            document.AdvocateId = advocate.Id;
            int subcategoryName = Int32.Parse(Request.Form["Categories"].ToString());
            Subcategory subcategory =  await _db.Subcategories.Where(s => s.Id == subcategoryName).FirstOrDefaultAsync();
            document.Subcategory = subcategory;
            document.SubcategoryId = subcategory.Id;

            //List<Tags> docTags = await _db.Tags.Where(t => t.DocumentId == doc.Id).ToListAsync();
            //var tags = Request.Form["tags"].ToList();
            //if (tags.Count() != 0)
            //{
            //    var count = 0;
            //    foreach (var tag in docTags)
            //    {
            //        tag.Tagname = tags[count];
            //        tag.Document = doc;
            //    }
            //}

           

            if (Request.Form.Files.Count != 0)
            {
                doc.FileName = doc.File.FileName;
            }

            //if (ModelState.IsValid)
            //{
                //using (_db)
                //{
                    if (doc.File!=null)
                    {
                        document.File = doc.File;
                    }
                    if (document != null)
                    {
                        if (Request.Form.Files.Count != 0)
                        {
                            if (doc.File.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || doc.File.ContentType == "application/msword")
                            {
                                string deletedFileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", Request.Form["DeletedFile"].ToString());
                                if (Request.Form.Count != 0)
                                {
                                    if (System.IO.File.Exists(deletedFileName))
                                    {
                                        System.IO.File.Delete(deletedFileName);
                                    }
                                    if (!System.IO.File.Exists(deletedFileName))
                                    {
                                        string newfilename = Guid.NewGuid() + doc.FileName;
                                        document.FileName = newfilename;

                                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newfilename);

                                        using (FileStream fs = new FileStream(path, FileMode.Create))
                                        {
                                            await document.File.CopyToAsync(fs);
                                        }
                                    }
                                }
                            }
                        }
                        
                        document.Name = doc.Name;
                        document.Price = doc.Price;
                        document.Description = doc.Description;

                        if (doc.Tags != null)
                        {
                            List<string> tags = doc.Tags.Split(',').ToList();

                            if (doc.Tags.Length > document.Tags.Length)
                            {
                                foreach (var tag in tags)
                                {
                                    Tags tdb = await _db.Tags.Where(x => x.Tagname == tag).FirstOrDefaultAsync();
                                    if (tdb == null)
                                    {
                                        Tags t = new Tags();
                                        t.Tagname = tag;
                                        t.Document = doc;
                                        await _db.Tags.AddAsync(t);
                                    }
                                }

                                document.Tags = doc.Tags;
                            }
                        }
                        

                        await  _db.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("", "This document is not exists!");
                    }

                //}

                return RedirectToAction(nameof(List));
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Something is wrong");
            //    return View();
            //}

            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Document doc;
            //using (_db)
            //{
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
            //}
            return RedirectToAction(nameof(List));
        }

    }
}