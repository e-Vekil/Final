using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Components
{
    public class ShoppingCardViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly EvekilDb _db;
        public ShoppingCardViewComponent(UserManager<User> userManager, EvekilDb db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ShoppingCard> SC = new List<ShoppingCard>();
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                SC = null;
                return View(SC);
            }
            else
            {
                SC = await _db.ShoppingCard.Include(x=>x.Document).Where(x => x.User == user).ToListAsync();
                return View(SC);
            }
        }
    }
}