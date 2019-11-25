using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class TimeSlotViewModel
    {
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int FromSlot { get; set; }
        public int ToSlot { get; set; }
        public List<AppointmentTime> TimeSlots = new List<AppointmentTime>();
        public int SelectedSlot { get; set; }
        public string PatientPhone { get; set; }
        public void RemoveExistingAppointmentSlots(int patientId,DateTime date)
        {
            AppointmentData appointmentData = new AppointmentData();
            var PreviousAppointmentsTimeSlots = appointmentData.GetPatientsCurrentAppointmentTimeSlots(patientId,date);
            foreach (var item in PreviousAppointmentsTimeSlots)
            {

                var appointment = TimeSlots.SingleOrDefault(x => x.Id == item.Id);
                if (appointment != null)
                    TimeSlots.Remove(appointment);
            }
        }
    }
}