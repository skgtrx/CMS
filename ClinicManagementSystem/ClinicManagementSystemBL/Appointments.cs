using ClinicManagementSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Exceptions;

namespace ClinicManagementSystemBL
{
    public class Appointments
    {
        /// <summary>
        /// Instance of BookAppointment for accessing methods defined in DAL
        /// </summary>
        BookAppointment bookAppointment = new BookAppointment();

        /// <summary>
        /// Adds Appointment booked by Patient/Assistant based on Info provided
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns>Flag as true if added successfully else false</returns>
        public bool AddAppointment(Appointment appointment)
        {
            try
            {
                return bookAppointment.BookAppointments(appointment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
