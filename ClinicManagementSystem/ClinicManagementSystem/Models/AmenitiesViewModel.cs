using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class AmenitiesViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Required(ErrorMessage = "Medicine Name Required")]
        [RegularExpression(@"^[A-Za-z\s-]+$", ErrorMessage = "Special character are not allowed & only 1 medicine can be added at once")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"(([0-9]*[.])?[0-9]*)", ErrorMessage = "Enter a valid cost please")]
        public double Cost { get; set; }

        public Medicines ToMedicines()
        {
            Medicines medicines = new Medicines();
            medicines.Name = this.Name.Trim();
            medicines.Cost = this.Cost;
            return medicines;
        }

        public Tests ToTests()
        {
            Tests tests = new Tests();
            tests.Name = this.Name.Trim();
            tests.Cost = this.Cost;
            return tests;
        }
    }
}