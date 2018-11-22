using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public Document Document { get; set; }
        [Required]
        public int DocumentId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool Status { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
