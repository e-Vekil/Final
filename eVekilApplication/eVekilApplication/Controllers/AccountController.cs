﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Controllers
{
    public class AccountController:Controller
    {
        public IActionResult Registiration()
        {
            return View();
        }
    }
}
