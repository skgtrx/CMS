using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystemDOL.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public Users Doctor { get; set; }
        [ForeignKey("Doctor"), Column(Order = 0)]
        
        public int? DoctorId { get; set; }

        public Users Patient { get; set; }
        [ForeignKey("Patient"), Column(Order = 1)]
        [Required]
        public int? PatientId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public AppointmentTime AppointmentTime { get; set; }
        [ForeignKey("AppointmentTime"), Column(Order = 2)]
        public byte AppointmentTimeId { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }
        [ForeignKey("AppointmentStatus"), Column(Order = 3)]
        public byte AppointmentStatusId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Details { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeletedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedAt { get; set; }
    }
}
