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
    public class AdvocatesViewComponent : ViewComponent
    {
        private readonly EvekilDb _db;
        public AdvocatesViewComponent(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Advocate> Advocates = await _db.Advocates.ToListAsync();
            return View(Advocates);
        }
    }
}