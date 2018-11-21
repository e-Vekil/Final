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
    public class UsersController : Controller
    {
        private readonly EvekilDb _db;
        public UsersController(EvekilDb db)
        {
            _db = db;
        }

        public async Task<IActionResult> List()
        {
            List<User> users;
            using (_db)
            {
                users = await _db.Users.OrderByDescending(x => x.RegisterDate).ToListAsync();
            }
            return View(users);
        }
    }
}