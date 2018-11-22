using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
                //List<string> userRoles = null;

                foreach (var user in users)
                {
                    var roles =  await _db.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
                    int i = 1;
                    foreach (var r in roles)
                    {
                        var role = await _db.Roles.Where(x => x.Id == r.RoleId).FirstOrDefaultAsync();
                        //userRoles.Add(role.Name.ToString());

                        if (i == 1) user.Roles = role.Name;
                        else user.Roles += $",{role.Name}";
                        i++;
                    }

                    //user.Roles = string.Join(",", userRoles);

                }

            }
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id, string roles)
        {
            User user;
            using (_db)
            {
                user = await _db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (roles.Contains("Admin")) return Content("You cannot remove admin!");
                    else _db.Users.Remove(user);
                }
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }


    }


}