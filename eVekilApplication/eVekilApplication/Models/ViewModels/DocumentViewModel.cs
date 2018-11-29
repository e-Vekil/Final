using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.ViewModels
{
    public class DocumentViewModel
    {
        public List<Document> Documents { get; set; }
        public Category category { get; set; }
    }
}
