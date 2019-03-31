using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using eVekilApplication.Models.PaymentModels;
using eVekilApplication.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eVekilApplication.Controllers
{
    public class PaymentController : Controller
    {
        private readonly EvekilDb _db;
        private readonly UserManager<User> _userManager;

        private const string RequestToServerURL = "https://rest.goldenpay.az/";
        private const string RequestToServerURLGetPaymentKey = RequestToServerURL + "web/service/merchant/getPaymentKey";
        private const string RequestToServerURLGetPaymentResult = RequestToServerURL + "web/service/merchant/getPaymentResult";
        private const string RequestToServerURLPayPage = RequestToServerURL + "web/paypage?payment_key=";

        private const string MerchantName = "e-vakil";
        private const string AuthKey = "bd48712368924d409a89c423013c9481";

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
                var total = 0;
                PaymentModel pm = new PaymentModel();
                var user = await _userManager.GetUserAsync(HttpContext.User);
                pm.Name = user.Name;
                pm.Surname = user.Surname;
                List<Document> purchasedDocuments = new List<Document>();
                pm.Documents = purchasedDocuments;
                var cardTypes = await _db.CardTypes.ToListAsync();
                pm.CardTypes = cardTypes;
                foreach (var item in basketInfo)
                {
                    if(item.UserId == user.Id)
                    {
                         var document = _db.Documents.Find(item.DocumentId);
                         purchasedDocuments.Add(document);
                         total += document.Price;
                    }
                }
                pm.TotalPrice = total;
                pm.TransactionId = GenerateOrderID();
                return View(pm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private string GetMD5HashCode(string input)
        {
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
                return System.BitConverter.ToString(data).Replace("-", "").ToLower();
            }
            catch (Exception err)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Checkout(PaymentModel payment)
        {
            CPaymentItem cPaymentItem = new CPaymentItem();
            cPaymentItem.merchantName = MerchantName;
            cPaymentItem.amount = (int)(Convert.ToDouble(payment.TotalPrice) * 100);
            cPaymentItem.cardType = payment.CardType.Key;
            cPaymentItem.lang = "lv";
            cPaymentItem.description = "any description you want";
            cPaymentItem.hashCode = GetMD5HashCode(AuthKey +
                                                    MerchantName + payment.CardType.Key + cPaymentItem.amount + cPaymentItem.description);

            var jsonValues = JsonConvert.SerializeObject(cPaymentItem);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestToServerURLGetPaymentKey);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";
            request.Accept = "application/json";
            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonValues);
                streamWriter.Flush();
            }

            string responseData = string.Empty;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseData = reader.ReadToEnd();
            }

            CRespGetPaymentKey cRespPymnt = (CRespGetPaymentKey)JsonConvert.DeserializeObject(responseData, typeof(CRespGetPaymentKey));

            if (cRespPymnt.status.code != 1)
                throw new Exception("Error while getting paymentKey, code=" + cRespPymnt.status.code + ", message=" + cRespPymnt.status.message);

            return Redirect(RequestToServerURLPayPage + cRespPymnt.paymentKey);
        }

        public ActionResult Callback(string payment_key)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestToServerURLGetPaymentResult + "?payment_key="
                    + payment_key + "&hash_code=" + GetMD5HashCode(AuthKey + payment_key));
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseData = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseData = reader.ReadToEnd();
            }

            CRespGetPaymentResult paymentResult = (CRespGetPaymentResult)JsonConvert.DeserializeObject(responseData, typeof(CRespGetPaymentResult));
            if(paymentResult.status.code == 1)
            {

            }
                return View(paymentResult);


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