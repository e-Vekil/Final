using eVekilApplication.Data;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EvekilDb _db;
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, EvekilDb db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel rvm)
        {
            if (rvm.Login != null)
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByEmailAsync(rvm.Login.Email);
                    var userRole = await _userManager.IsInRoleAsync(user, "Admin");
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, rvm.Login.Password, rvm.Login.IsRemmember, true);
                        if (result.Succeeded)
                        {
                            HttpContext.Session.SetString("id", user.Id);
                            HttpContext.Session.SetString("name", user.UserName);
                            HttpContext.Session.SetString("isLoged", "true");

                            if (userRole)
                            {
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else
                            {
                                HttpContext.Session.SetString("id", user.Id);
                                HttpContext.Session.SetString("name", user.UserName);
                                HttpContext.Session.SetString("isLoged", "true");
                                return RedirectToAction("Index", "Home", new { area = "" });
                            }
                        }
                        return View();
                    }

                }
                return View();
            }
            else if (rvm.Register != null)
            {
                if (ModelState.IsValid)
                { 
                    User user = new User()
                    {
                        UserName = rvm.Register.Surname.ToLower(),                 /*!!!!!1!!!*/
                        Name = rvm.Register.Name,
                        Surname = rvm.Register.Surname,
                        Email = rvm.Register.Email,
                    };
                    var identityResult = await _userManager.CreateAsync(user, rvm.Register.Password);
                    if (identityResult.Succeeded)
                    {
                        HttpContext.Session.SetString("id", user.Id);
                        HttpContext.Session.SetString("name", user.Name);
                        await _userManager.AddToRoleAsync(user, "user");
                        var signInResult = await _signInManager.PasswordSignInAsync(user, rvm.Register.Password, true, true);
                        if (signInResult.Succeeded)
                        {
                            HttpContext.Session.SetString("isLoged", "true");
                            return RedirectToAction("Index", "Account");
                        }
                    }
                    else
                    {
                        return View();
                    }

                }
                return View();

            }
            else
            {
                return Content("Nəisə səhv getdi.");
            }
        }

        public IActionResult Home()
        {
            return View();
        }


    }
}
