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
    public class ShoppingBasketViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly EvekilDb _db;
        public ShoppingBasketViewComponent(UserManager<User> userManager, EvekilDb db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int basketCount;
            List<ShoppingCard> SC = new List<ShoppingCard>();
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                basketCount = 0;
                return View(basketCount);
            }
            else
            {
                SC = await _db.ShoppingCard.Where(x => x.User == user).ToListAsync();
                basketCount = SC.Count;

                return View(basketCount);
            }
        }
    }
}