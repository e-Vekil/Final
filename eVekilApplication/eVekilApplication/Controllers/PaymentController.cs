using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Controllers
{
    public class PaymentController : Controller
    {
        private readonly EvekilDb _db;
        private readonly UserManager<User> _userManager;
        public PaymentController(EvekilDb db, UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var basketInfo = await _db.ShoppingCard.ToListAsync();
            if(basketInfo != null)
            {
                //Payment payment = new Payment();
                var total = 0;
                PaymentModel pm = new PaymentModel();
                var user = await _userManager.GetUserAsync(HttpContext.User);
                pm.Name = user.Name;
                pm.Surname = user.Surname;
                List<Document> purchasedDocuments = new List<Document>();
                pm.Documents = purchasedDocuments;
                //payment.User = user;
                foreach (var item in basketInfo)
                {
                    if(item.UserId == user.Id)
                    {
                        //PaymentDocument pm = new PaymentDocument();
                        //pm.Payment = payment;
                         var document = _db.Documents.Find(item.DocumentId);
                         purchasedDocuments.Add(document);
                         total += document.Price;
                    }
                }
                pm.TotalPrice = total;
                //payment.TotalPrice = total;
                pm.TransactionId = GenerateOrderID();
                //payment.Status = false;
                //payment.Date = DateTime.Now;

                return View(pm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public string GenerateOrderID()
        {
            Random rnd = new Random();
            Int64 s1 = rnd.Next(000000, 999999);
            Int64 s2 = Convert.ToInt64(DateTime.Now.ToString("ddMMyyyyHHmmss"));
            string s3 = s1.ToString() + "" + s2.ToString();
            return s3;
        }
    }
}