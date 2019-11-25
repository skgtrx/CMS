using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;

namespace ClinicManagementSystem.Models
{
    public class AdminProfileEditViewModel
    {
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Special character should not be entered")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Special character should not be entered")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [MinLength(0, ErrorMessage = "Enter a minimum number of characters")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Required(ErrorMessage = "Telephone Number Required")]
        [Index(IsUnique = true)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(60)]
        [Required]
        [Index(IsUnique = true)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Enter a minimum of 6 characters")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$",
            ErrorMessage = "Your password Must: 1. be a minimum of 6 characters 2.include a minimum of three of the following character types: uppercase,lowercase,numbers,special characters,")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password must be same as new password")]
        public string ConfirmPassword { get; set; }
    }
}