using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.ViewModels
{
    public class DocumentDescViewModel
    {
        public Document Document { get; set; }
        public CommentViewModel Cm { get; set; }
        public IQueryable<Document> documents { get; set; }
    }
}
