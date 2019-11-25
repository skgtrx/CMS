using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicManagementSystem.Models
{
    public class DoctorRegisterViewModel
    {
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Special character should not be entered")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Special character should not be entered")]
        public string LastName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(60)]
        [Required]
        [Index(IsUnique = true)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [MinLength(0, ErrorMessage = "Enter a minimum number of characters")]
        public string Address { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Required(ErrorMessage = "Telephone Number Required")]
        [Index(IsUnique = true)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(ClinicManagementSystemDOL.Enums.Specialization))]
        public List<ClinicManagementSystemDOL.Enums.Specialization> Specializations { get; set; }

        [Required]
        [RegularExpression(@"(([0-9]*[.])?[0-9]*)", ErrorMessage = "Enter a valid fee please")]
        public double Fee { get; set; }
    }
}