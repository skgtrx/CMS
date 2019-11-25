using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;

namespace ClinicManagementSystemBL
{
    public class DoctorsData
    {
        /// <summary>
        /// Retrives the list of Available Doctors
        /// </summary>
        /// <returns>List of Available Doctors</returns>
        public List<DoctorFee> GetDoctorsList()
        {
            DoctorData doctorData = new DoctorData();
            return doctorData.GetDoctorsList();
        }

        /// <summary>
        /// Retrives List of Doctor Specialization from database
        /// </summary>
        /// <returns>List of Doctor Specialization</returns>
        public List<DoctorSpecialization> GetDoctorSpecilizationList()
        {
            DoctorData doctorData = new DoctorData();
            return doctorData.GetDoctorSpecilizationsList();
        }

        /// <summary>
        /// Retrives the list of Doctor Appointment for a paricular date
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns>List of Appointments</returns>
        public List<Appointment> GetDoctorTimeSlots(int doctorId, DateTime date)
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            return appointmentsData.GetDoctorAppointmentsData(doctorId, date);
        }

        /// <summary>
        /// Retrives list of free time slots of a doctor on a particular date
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns>List of Availavle Time slots</returns>
        public List<AppointmentTime> GetDoctorTimeSlots()
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            return appointmentsData.GetTimeSlot();
        }

        /// <summary>
        /// Retrives the list Available time slots for a particular doctor on a particular date
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns>List of Availavle Time slots</returns>
        public List<AppointmentTime> GetDoctorAvailableTimeSlots(int doctorId, DateTime date)
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            List<Appointment> BookedAppointmentTimeSlots= appointmentsData.GetDoctorAppointmentsData(doctorId, date);
            List<AppointmentTime> AllTimeSlots= appointmentsData.GetTimeSlot();
            List<AppointmentTime> availableTimeSlots = new List<AppointmentTime>();
            foreach (AppointmentTime TimeSlots in AllTimeSlots)
            {
                Boolean SlotBooked = false;
                foreach (Appointment BookedTimeSlots in BookedAppointmentTimeSlots)
                {
                    if (TimeSlots.Id == BookedTimeSlots.AppointmentTimeId)
                    {
                        SlotBooked = true;
                    }
                }
                if (SlotBooked == false)
                {
                    availableTimeSlots.Add(TimeSlots);
                }
            }
            return availableTimeSlots;
        }

        /// <summary>
        /// Retrives the list of fee of all the doctors
        /// </summary>
        /// <returns>List of Doctor Fee</returns>
        public List<DoctorFee> GetAllDoctorsList()
        {
            DoctorData doctorData = new DoctorData();
            return doctorData.GetAllDoctorsList();
        }
    }
}
