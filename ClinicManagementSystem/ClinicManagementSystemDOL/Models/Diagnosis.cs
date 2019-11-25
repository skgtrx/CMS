using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Models
{
    public class Diagnosis
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Appointment Appointment { get; set; }
        [ForeignKey("Appointment"), Column(Order = 0)]
        [Index(IsUnique = true)]
        public int AppointmentId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Remark { get; set; }
    }
}
