using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Document Document { get; set; }
        public int DocumentId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public bool IsViewed { get; set; }
    }
}
