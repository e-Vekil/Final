using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
