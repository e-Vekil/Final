﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly EvekilDb _db;
        public HomeController(EvekilDb db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Document()
        {
            return View();
        }

        public async Task<IActionResult> DocumentDesc()
        {
            List<Comment> comments;
            using (_db)
            {
                comments = await _db.Comments.Include(c=>c.User).Include(c=>c.Document).ToListAsync();
            }
            return View(comments);
        }
    }
}