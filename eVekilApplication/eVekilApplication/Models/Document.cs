using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Advocate Advocate { get; set; }
        public int AdvocateId { get; set; }
        //public Category Category { get; set; }
        //public int CategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public string TermsOfUse { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
    }
}
