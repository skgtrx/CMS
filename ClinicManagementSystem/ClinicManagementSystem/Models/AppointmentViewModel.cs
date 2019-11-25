using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class AppointmentViewModel
    {
        public List<DateTime> DatesList = new List<DateTime>();
        public DateTime SelectedDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int SelrctedTimeSlot { get; set; }
    }
}