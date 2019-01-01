using eVekilApplication.Data;
using eVekilApplication.Infrastructure.Email;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, EvekilDb db, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            _authenticationSchemeProvider = authenticationSchemeProvider;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Registration()
        {
            var allSchemeProvider = (await _authenticationSchemeProvider.GetAllSchemesAsync()).Select(a => a.DisplayName).Where(n => !String.IsNullOrEmpty(n));
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                return RedirectToAction(nameof(Home));
            }
            RegistrationViewModel reg = new RegistrationViewModel()
            {
                Providers = allSchemeProvider
            };
            return View(reg);
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ResetPassword
                (ResetPasswordViewModel obj)
        {
            User user = _userManager.FindByNameAsync(obj.UserName).Result;

            IdentityResult result = _userManager.ResetPasswordAsync
                      (user, obj.Token, obj.Password).Result;
            if (result.Succeeded)
            {   
                ViewBag.Message = "Password reset successful!";
                return View("Registration");
            }
            else
            {
                ViewBag.Message = "Error while resetting the password!";
                return View("Registration");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> SendPasswordResetLink(string username, [FromServices]EmailService service)
        {

            if (username == null)
            {
                ViewBag.Message = "Xahis edirik username daxil edin!";
                return View("ChangePassword");
            }

            User user = _userManager.FindByNameAsync(username).Result;

            if (user == null || !(_userManager.IsEmailConfirmedAsync(user).Result))
            {
                ViewBag.Message = "Error while resetting your password!";
                return View("Registration");
            }

            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

            var resetLink = Url.Action("ResetPassword",
                            "Account", new { token = token },
                             protocol: HttpContext.Request.Scheme);

            // code to email the above link

            var acceptMessage =
$@"Dəyərli {user.UserName} !
Zəhmət olmasa aşağıdakı linkə klik şifrənizi dəyişin:
{resetLink}
Bizi Seçdiyiniz üçün Təşəkkürlər.Hörmətlə e-vekil.az
        ";
            await service.SendMailAsync(user.Email, "E-VAKIL.AZ TESDIQ", acceptMessage);
            // see the earlier article

            ViewBag.Message = "Password reset link has been sent to your email address!";
            return View("Registration");

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string provider)
        {
            return Challenge(new AuthenticationProperties {RedirectUri ="/" }, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task LoginGoogle()
        {
            await HttpContext.ChallengeAsync("Google", new AuthenticationProperties() { RedirectUri = "/Account/Home" });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationViewModel rvm, [FromServices]EmailService service)
        {
            if (rvm.Login != null)
            {
                if (ModelState.IsValid) 
                {
                    User user = await _userManager.FindByEmailAsync(rvm.Login.Email);
                    if (user != null)
                    {
                        if (user.EmailConfirmed)
                        {
                            var userRole = await _userManager.IsInRoleAsync(user, "Admin");
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
                                    HttpContext.Session.Remove("emailConfirmed");
                                    HttpContext.Session.Remove("isSended");
                                    return RedirectToAction("Home", "Account", new { area = "" });
                                }
                            }
                            return View();
                        }
                        else
                        {
                            ModelState.AddModelError("", "Email təsdiq olunmayıb");
                        }
                    
                    }

                }
                return View();
            }
            else if (rvm.Register != null)
            {
                ViewBag.Info = "Register";
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
                        //Email Confirm
                         
                        string Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        string Tokenlink = Url.Action("ConfirmEmail", "Account", new
                        {
                            userId = user.Id,
                            token = Token
                        });
                        var acceptMessage = 
$@"Dəyərli {user.UserName} !
Zəhmət olmasa aşağıdakı linkə klik edərək e-poçtunuzu təsdiq edin:
https://localhost:44302/{Tokenlink}
Bizi Seçdiyiniz üçün Təşəkkürlər.Hörmətlə e-vekil.az
        ";
                        await service.SendMailAsync(user.Email, "E-VAKIL.AZ TESDIQ", acceptMessage);
                        //Email Confirm End

                        HttpContext.Session.SetString("isSended", "true");
                        HttpContext.Session.SetString("email", user.Email);
                        await _userManager.AddToRoleAsync(user, "user");
                        var message = $@"{user.Name} {user.Surname} {user.RegisterDate} tarixində saytdan qeydiyyatdan keçmişdir.";
                        await service.SendMailAsync("ibrahimxanlimurad@hotmail.com", "USER REGISTER", message);
                        return RedirectToAction("Registration", "Account");
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null)
            {
                return Content("Error");
            }
            var user =  await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return Content("Error");
            }

            IdentityResult result =await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("emailConfirmed","true");
                return RedirectToAction("Home", "Account");
            }
            else
            {
                return RedirectToAction("Registration", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Content("Siz daxil olmamısınız.");
            }
            else
            {
                return View(user);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            //string userid = HttpContext.Session.GetString("id");
            //bool success = Int32.TryParse(HttpContext.Session.GetString("id"), out userid);
            //User userr = await _db.Users.Where(x => x.Id == userid).FirstOrDefaultAsync();


            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;


            //RegisterModel user = new RegisterModel()
            //{
            //    Name = userr.Name,
            //    Surname = userr.Surname,
            //    Email = userr.Email,
            //    Password = "",
            //    ConfirmPassword = ""
            //};
            //if (userr == null)
            //{
            //    return Content("Siz daxil olmamısınız.");
            //}
            //else
            //{
            //    return View(user);
            //};

            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return Content("Siz daxil olmamısınız.");
            else
            {
                DetailsViewModel ruser = new DetailsViewModel()
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.UserName,
                    Email = user.Email,
                    Password = "",
                    ConfirmPassword = "",
                    OldPassword = ""
                };

                return View(ruser);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Details(DetailsViewModel rvm)
        {
            //string userid = HttpContext.Session.GetString("id");
            //User user = await _db.Users.Where(x => x.Id == userid).FirstOrDefaultAsync();
            User user = await _userManager.GetUserAsync(HttpContext.User);

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

                        //user.PasswordHash = AppUserManager.PasswordHasher.HashPassword(usermodel.Password);
                        //var result = await AppUserManager.UpdateAsync(user);


                        //var result = await _userManager.RemovePasswordAsync(user);
                        //if (result.Succeeded)
                        //{
                        //    result = await _userManager.AddPasswordAsync(user, rvm.Password);
                        //    if (result.Succeeded)
                        //    {
                        //        return RedirectToAction(nameof(Home));
                        //    }
                        //    else
                        //    {
                        //        //ModelState.AddModelError("", result.Errors.FirstOrDefault().ToString());
                        //        return View(rvm);
                        //    }
                        //}
                        //else return Content("Nə isə səhv getdi...");

                        var result = await _userManager.ChangePasswordAsync(user, rvm.OldPassword, rvm.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(Home));
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                            return View(rvm);
                        }
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
                        user.UserName = rvm.Username;
                        user.Email = rvm.Email;

                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Home));
                    }
                    else return View(rvm);
                   
                }
            };

        }


        public async Task<IActionResult> PurchasedDocuments()
        {
            List<PurchasedDocument> Downloads;
            User user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null) return Content("Siz daxil olmamısınız.");
            else
            {
                Downloads = await _db.PurchasedDocuments.Include(x => x.User).Include(x => x.Document).Where(x => x.User == user).OrderByDescending(x => x.Date).ToListAsync();

                return View(Downloads);
            }
        }



        public async Task<IActionResult> Purchase()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null) return Content("Siz daxil olmamısınız.");
            else
            {
                List<ShoppingCard> SP = await _db.ShoppingCard.Include(x => x.User).Include(x => x.Document).Where(x => x.User == user).ToListAsync();
                foreach (var item in SP)
                {
                    PurchasedDocument PD = new PurchasedDocument()
                    {
                        User = item.User,
                        UserId = item.UserId,
                        Document = item.Document,
                        DocumentId = item.DocumentId,
                        Date = DateTime.Now,
                        Price = item.Document.Price,
                        IsCompleted = true
                    };

                    await _db.PurchasedDocuments.AddAsync(PD);
                    await _db.SaveChangesAsync();
                }

                //List<PurchasedDocument> AllPD = await _db.PurchasedDocuments.Where(x => x.User == user).ToListAsync();
                
                //if (AllPD.Count == SP.Count)
                //{
                //}
                //else return Content("Nə isə səhv getdi, xahiş edirik bizimlə əlaqə saxlayın.");

                _db.ShoppingCard.RemoveRange(SP);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");

            }

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
            
    }
}
