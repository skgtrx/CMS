using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    public class PasswdViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Enter a minimum of 6 characters")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$",
            ErrorMessage = "Your password Must: 1. be a minimum of 6 characters 2.include a minimum of three of the following character types: uppercase,lowercase,numbers,special characters,")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password must be same as new password")]
        public string ConfirmPassword { get; set; }


        public string Id { get; set; }
    }
}