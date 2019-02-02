using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Advocate Advocate { get; set; }
        public int AdvocateId { get; set; }
        //public Category Category { get; set; }
        //public int CategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public string Tags { get; set; }
        //public bool IsTemplate { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
