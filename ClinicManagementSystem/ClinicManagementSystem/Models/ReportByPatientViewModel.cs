using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class ReportByPatientViewModel
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Required(ErrorMessage = "Telephone Number Required")]
        [Index(IsUnique = true)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
    }
}