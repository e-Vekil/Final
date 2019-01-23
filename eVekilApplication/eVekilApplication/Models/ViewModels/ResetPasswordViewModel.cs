using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
