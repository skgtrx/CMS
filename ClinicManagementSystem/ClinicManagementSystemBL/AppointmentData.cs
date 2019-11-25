using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class AppointmentData
    {
        /// <summary>
        /// Retreaves the list of all the appointments from Database
        /// </summary>
        /// <returns>List of Appointments</returns>
        public List<Appointment> GetAppointmentList()
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            return appointmentsData.DisplayAppointments();
        }

        /// <summary>
        /// Retreaves the list of all the appointments based on patient id
        /// </summary>
        /// <returns>List of Appointments</returns>
        public List<Appointment> GetPatientAppointmentList(int id)
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            return appointmentsData.GetPatientAppointmentsData(id);
        }

        /// <summary>
        /// Retreaves the list of all the appointments associated to a doctor id
        /// </summary>
        /// <returns>List of Appointments</returns>
        public List<Appointment> GetDoctorAppointmentList(int id)
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            return appointmentsData.GetDoctorAppointmentsData(id);
        }

        /// <summary>
        /// Retreaves the list of all the appointments with closed status
        /// </summary>
        /// <returns>List of Appointments</returns>
        public List<Appointment> GetClosedAppointmentList()
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            return appointmentsData.DisplayClosedAppointments();
        }

        public List<AppointmentTime> GetPatientsCurrentAppointmentTimeSlots(int patientId,DateTime date)
        {
            AppointmentsData appointmentsData = new AppointmentsData();
            return appointmentsData.GetPatientsCurrentAppointmentTimeSlots(patientId,date);
        }
    }
}

