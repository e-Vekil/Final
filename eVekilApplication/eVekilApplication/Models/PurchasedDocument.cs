using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class PurchasedDocument
    {
        public int Id { get; set; }
        public Document Document { get; set; }
        public int DocumentId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public bool IsViewed { get; set; }
    }
}
