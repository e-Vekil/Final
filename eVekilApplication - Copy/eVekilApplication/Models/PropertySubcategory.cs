using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class PropertySubcategory
    {
        public int Id { get; set; }
        public Property Property { get; set; }
        public int ProperyId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
    }
}
