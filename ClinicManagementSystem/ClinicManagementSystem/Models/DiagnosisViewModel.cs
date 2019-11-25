using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class DiagnosisViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }

        public Appointment Appointment { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Remark { get; set; }
        public Tests test { get; set; }
        public Medicines medicine { get; set; }

        public List<Medicines> MedicineRecords { get; set; }
        public List<TestRecord> TestRecords { get; set; }
    }
}