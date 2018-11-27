using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class CreatedDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        //public Category Category { get; set; }
        //public int CategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
    }
}
