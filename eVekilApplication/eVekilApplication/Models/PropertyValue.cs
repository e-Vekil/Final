using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }
        public Document Document { get; set; }
        public int DocumentId { get; set; }
        public string Value { get; set; }
    }
}
