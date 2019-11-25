using System;
using System.Collections.Generic;
using ClinicManagementSystemDOL.Enums;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ClinicManagementSystemDOL.Models;

namespace ClinicManagementSystem.Models
{
    public class DoctorScheduleViewModel
    {
        DateTime Date { get; set; }
        public int DoctorId { get; set; }

        //[EnumDataType(typeof(TimeSlots))]
        //public TimeSlots? From { get; set; }
        public List<AppointmentTime> From = new List<AppointmentTime>();

        //[EnumDataType(typeof(TimeSlots))]
        //public TimeSlots? To { get; set; }
        public List<AppointmentTime> To = new List<AppointmentTime>();
        public List<DoctorAvailability> AvailabilityList = new List<DoctorAvailability>();
        public string[] Availability { get; set; }
        public int SelectedFromId { get; set; }
        public int SelectedToId { get; set; }
        public AppointmentTime PreviousFromScheduleTime=null;// = new AppointmentTime();
        public AppointmentTime PreviousToScheduleTime=null;// = new AppointmentTime();
        public void ToAvailabilityList()
        {
            DoctorAvailability doctorAvailability;
            for(int i = 0; i < Availability.Length; i+=3)
            {
                doctorAvailability = new DoctorAvailability();
                doctorAvailability.Date =DateTime.Parse(Availability[i]);
                doctorAvailability.FromTime =byte.Parse(Availability[i + 1]);
                doctorAvailability.ToTime = byte.Parse(Availability[i + 2]);
                doctorAvailability.UserId = DoctorId;
                AvailabilityList.Add(doctorAvailability);
            }
        }
    }
}