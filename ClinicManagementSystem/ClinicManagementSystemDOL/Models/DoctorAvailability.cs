using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Models
{
    public class DoctorAvailability
    {
        public Users Users { get; set; }
        [Key, Column(Order = 0)]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [Key,Column(Order =1)]
        public DateTime Date { get; set; }

        [Required]
        public byte FromTime { get; set; }

        [Required]
        public byte ToTime { get; set; }
        
    }
}