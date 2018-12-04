using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Areas.Admin.Models.ViewModels;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVekilApplication.Areas.Admin.Components
{
    public class NotificationBellViewComponent : ViewComponent
    {
        private readonly EvekilDb _db;
        public NotificationBellViewComponent(EvekilDb db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            NotificationBellViewModel NBVM = new NotificationBellViewModel();

            List<User> Users = await _db.Users.Where(x => x.IsViewed == false).OrderByDescending(x => x.RegisterDate).ToListAsync();
            List<PurchasedDocument> purchasedDocuments = await _db.PurchasedDocuments.Include(x => x.User).Include(x => x.Document).Where(x => x.IsViewed == false).OrderByDescending(x => x.Date).ToListAsync();
            List<Comment> Comments = await _db.Comments.Include(x => x.User).Include(x => x.Document).Where(x => x.IsViewed == false).OrderByDescending(x => x.Date).ToListAsync();

            NBVM.Comments = Comments;
            NBVM.Users = Users;
            NBVM.PurchasedDocuments = purchasedDocuments;

            NBVM.Count = Comments.Count + Users.Count + purchasedDocuments.Count;
            return View(NBVM);
        }
       
    }
}