using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Models
{
    public class DoctorFee
    {
        public Users Doctor { get; set; }
        [ForeignKey("Doctor")]
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [DefaultValue(100)]
        public double Fee { get; set; }
    }
}
