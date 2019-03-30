using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.ViewModels
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TransactionId { get; set; }
        public List<Document> Documents { get; set; }
        public float TotalPrice { get; set; }
    }
}
