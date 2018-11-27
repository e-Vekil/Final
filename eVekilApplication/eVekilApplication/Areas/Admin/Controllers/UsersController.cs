using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Areas.Admin.Models.ViewModels;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EvekilDb _db;
        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, EvekilDb db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
        public IActionResult Edit(string id)
        {
            EditUserViewModel evm = new EditUserViewModel()
            {
                Id = id,
                Password = "",
                ConfirmPassword = ""
            };
            return View(evm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit( EditUserViewModel evm)
        {
            User user = await _db.Users.Where(x => x.Id == evm.Id).FirstOrDefaultAsync();
            string ispassword = Request.Form["changepassword"].ToString();

            if (ispassword == "true")
            {
                if (ModelState.IsValid)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, evm.Password);
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(evm);
                    }
                }
                else return View(evm);
            }else
            {
                string role = Request.Form["roles"].ToString();
                string roledelete = Request.Form["rolesdelete"].ToString();

                if (role == "")
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, roledelete);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(evm);
                    }
                }
                else
                {
                    var result = await _userManager.AddToRoleAsync(user, role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(evm);
                    }
                }
                
            }
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