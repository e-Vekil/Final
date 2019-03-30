using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class PaymentDocument
    {
        public int Id { get; set; }
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
        public Document Document { get; set; }
        public int DocumentId { get; set; }
    }
}
