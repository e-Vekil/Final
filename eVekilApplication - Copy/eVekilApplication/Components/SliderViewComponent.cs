using eVekilApplication.Data;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Components
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly EvekilDb _db;
        public SliderViewComponent(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HomeViewModel hm = new HomeViewModel();
            var categories = await _db.Categories.ToListAsync();
            hm.Categories = categories;
            return View(hm);
        }
    }
}
