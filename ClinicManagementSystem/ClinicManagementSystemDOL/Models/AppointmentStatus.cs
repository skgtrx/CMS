using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Models
{
    public class AppointmentStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public byte Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
