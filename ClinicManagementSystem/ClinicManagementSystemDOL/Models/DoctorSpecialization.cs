using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Models
{
    public class DoctorSpecialization
    {

        public Users Users { get; set; }
        [Key]
        [ForeignKey("Users"), Column(Order = 0)]
        public int UserId { get; set; }

        public Specialization Specialization { get; set; }
        [Key]
        [ForeignKey("Specialization"), Column(Order = 1)]
        public int SpecializationId { get; set; }
    }
}
