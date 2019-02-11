using eVekilApplication.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Components
{
    public class ConnectViewComponent:ViewComponent
    {
        private readonly EvekilDb db;
        public ConnectViewComponent(EvekilDb _db)
        {
            db = _db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
