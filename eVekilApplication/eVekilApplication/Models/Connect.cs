using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class Connect
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
        public string Reply { get; set; }
        public DateTime? ReplyDate { get; set; }
        public bool Status { get; set; }
    }
}
