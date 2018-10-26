using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eVekilApplication.Controllers
{
    public class HomeController : Controller
    {
        public int fefe;
        public String fef;
        public string fawe;
        public IActionResult Index()
        {
            return View();
        }
    }
}