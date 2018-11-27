using System;
using System.Collections.Generic;
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
    public class DownloadsController : Controller
    {
        private EvekilDb _db;
        public DownloadsController(EvekilDb db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<PurchasedDocument> Downloads;
            using (_db)
            {
                Downloads = await _db.PurchasedDocuments.Include(x => x.User).Include(x => x.Document).OrderByDescending(x => x.Date).ToListAsync();
            }

            return View(Downloads);
        }
    }
}