using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;

namespace ClinicManagementSystemBL
{
    public class ScheduleBasedAppointmentManagement
    {
        /// <summary>
        /// Instance of ScheduleBasedAppointment for accessing the methods defiened in DAL
        /// </summary>
        ScheduleBasedAppointment scheduleBasedAppointment = new ScheduleBasedAppointment();

        /// <summary>
        /// Retreaves the entire list of Doctor Availability from the database
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List of Doctor Available slots</returns>
        public List<DoctorAvailability> GetAvailability(DateTime date)
        {
            return scheduleBasedAppointment.GetAvailability(date);
        }

        /// <summary>
        /// Retreaves the entire list of Doctor Availability from the database baased on User Id and Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List of Doctor Available slots</returns>
        public DoctorAvailability GetAvailability(int doctorId, DateTime date)
        {
            return scheduleBasedAppointment.GetAvailability(doctorId, date);
        }
    }
}
