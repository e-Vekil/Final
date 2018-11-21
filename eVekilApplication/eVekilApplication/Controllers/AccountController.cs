using eVekilApplication.Data;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Controllers
{
    [Authorize(Roles = "User")]
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
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
                                return RedirectToAction("Home", "Account", new { area = "" });
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
                        RegisterDate = DateTime.Now
                    };
                    var identityResult = await _userManager.CreateAsync(user, rvm.Register.Password);
                    if (identityResult.Succeeded)
                    {
                        HttpContext.Session.SetString("id", user.Id);
                        HttpContext.Session.SetString("name", user.UserName);
                        await _userManager.AddToRoleAsync(user, "user");
                        var signInResult = await _signInManager.PasswordSignInAsync(user, rvm.Register.Password, true, true);
                        if (signInResult.Succeeded)
                        {
                            HttpContext.Session.SetString("isLoged", "true");
                            return RedirectToAction("Home", "Account");
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

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            string userid = HttpContext.Session.GetString("id");
            //bool success = Int32.TryParse(HttpContext.Session.GetString("id"), out userid);

            User userr = await _db.Users.Where(x => x.Id == userid).FirstOrDefaultAsync();

            RegisterModel user = new RegisterModel()
            {
                Name = userr.Name,
                Surname = userr.Surname,
                Email = userr.Email,
                Password = "",
                ConfirmPassword = ""
            };
            if (userr == null)
            {
                return Content("Siz daxil olmamısınız.");
            }
            else
            {
                return View(user);
            };

        }

        [HttpPost]
        public async Task<IActionResult> Details(RegisterModel rvm)
        {
            string userid = HttpContext.Session.GetString("id");
            User user = await _db.Users.Where(x => x.Id == userid).FirstOrDefaultAsync();


            if (user == null)
            {
                return Content("Siz daxil olmamısınız.");
            }
            else
            {
                string ispassword = Request.Form["changepassword"].ToString();
                if (ispassword == "true")
                {
                    if (ModelState.IsValid)
                    {
                        var result = await _userManager.AddPasswordAsync(user, rvm.Password);
                        if (result.Errors.Count() > 1) ModelState.AddModelError("", "Şifrə səhv formadadır");


                        result = await _userManager.RemovePasswordAsync(user);
                        if (result.Succeeded)
                        {
                            result = await _userManager.AddPasswordAsync(user, rvm.Password);
                            if (result.Succeeded)
                            {
                                return RedirectToAction(nameof(Home));
                            }
                            else
                            {
                                //ModelState.AddModelError("", result.Errors.FirstOrDefault().ToString());
                                return View(rvm);
                            }
                        }
                        else return Content("Nə isə səhv getdi...");
                    }
                    else
                    {
                        return View(rvm);
                    }
                   
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        user.Name = rvm.Name;
                        user.Surname = rvm.Surname;
                        user.Email = rvm.Email;

                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Home));
                    }
                    else return View(rvm);
                   
                }
            };

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
