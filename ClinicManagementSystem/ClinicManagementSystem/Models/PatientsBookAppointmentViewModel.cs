using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class PatientsBookAppointmentViewModel
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        public List<AppointmentTime> AvailableTimeSlots = new List<AppointmentTime>();
        public int SelectedTimeSlot { get; set; }
        
        [Required]
        public TimeSlots TimeSlot { get; set; }
        [Required]
        
        [MaxLength(500)]
        public string Details { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage ="Phone Number should be of 10 digits")]
        [MinLength(10, ErrorMessage = "Phone Number should be of 10 digits")]
        public string PatientPhoneNumber { get; set; }
        public Appointment ToAppointment(PatientsBookAppointmentViewModel model)
        {
            Appointment appointment = new Appointment();

            appointment.DoctorId = model.DoctorId;
            appointment.AppointmentTimeId = (byte)model.SelectedTimeSlot;
            appointment.AppointmentStatusId = (int)ClinicManagementSystemDOL.Enums.AppointmentStatus.Pending;
            appointment.Date = DateTime.Parse(model.Date.ToShortDateString());
            appointment.Details = model.Details.Trim();

            return appointment;
        }
    }
}