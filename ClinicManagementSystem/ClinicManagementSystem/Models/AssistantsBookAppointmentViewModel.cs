using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class AssistantsBookAppointmentViewModel : RegisterViewModel
    {
        [DataType(DataType.Date)]
        [Required]
        //[Range(typeof(DateTime), DateTime.Today.ToString(), "", ErrorMessage = "Date must be after or equal to current date")]
        public DateTime Date { get; set; }
        public List<AppointmentTime> AvailableTimeSlots = new List<AppointmentTime>();
        public int SelectedTimeSlot { get; set; }

        [Required]
        public TimeSlots TimeSlot { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Minimum 10 letters required")]
        [MaxLength(100)]
        public string Details { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        
        //[MaxLength(10, ErrorMessage = "Phone Number should be of 10 digits")]
        //[MinLength(10, ErrorMessage = "Phone Number should be of 10 digits")]
        //[DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Emergency Contact Number Required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PatientPhoneNumber { get; set; }
        public Appointment ToAppointment(PatientsBookAppointmentViewModel model)
        {
            Appointment appointment = new Appointment();

            appointment.DoctorId = model.DoctorId;
            appointment.AppointmentTimeId = (byte)model.SelectedTimeSlot;
            appointment.AppointmentStatusId = (int)ClinicManagementSystemDOL.Enums.AppointmentStatus.Pending;
            appointment.Date = model.Date;
            appointment.Details = model.Details.Trim();

            return appointment;
        }
        public AssistantsBookAppointmentViewModel()
        {
            this.PatientPhoneNumber = "";
        }
        public Appointment ToAppointment(AssistantsBookAppointmentViewModel model)
        {
            Appointment appointment = new Appointment();

            appointment.DoctorId = model.DoctorId;
            appointment.AppointmentTimeId = (byte)model.SelectedTimeSlot;
            appointment.AppointmentStatusId = (int)ClinicManagementSystemDOL.Enums.AppointmentStatus.Pending;
            appointment.Date = model.Date;
            appointment.Details = model.Details;

            return appointment;
        }
    }
}