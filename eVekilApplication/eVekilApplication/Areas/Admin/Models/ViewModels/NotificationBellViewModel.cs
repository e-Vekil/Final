using eVekilApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Areas.Admin.Models.ViewModels
{
    public class NotificationBellViewModel
    {
        public List<Comment> Comments { get; set; }
        public List<User> Users { get; set; }
        public List<PurchasedDocument> PurchasedDocuments { get; set; }

        public int Count { get; set; }
    }
}
