using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string PaymentKey { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Document> Documents { get; set; }
        public CardType CartType { get; set; }
        public int CartTypeId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
