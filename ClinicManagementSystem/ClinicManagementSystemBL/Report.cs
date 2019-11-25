using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class Report
    {
        /// <summary>
        /// Instance of ReportData fro accessing its methods defined in the DAL
        /// </summary>
        ReportData reportData = new ReportData();

        /// <summary>
        /// Retreaves the List of appointments of a particular patient identified by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>List of Appointments</returns>
        public List<Appointment> GetReportByPatient(string phoneNumber)
        {
            return reportData.GetReportByPatients(phoneNumber);
        }

        /// <summary>
        /// Retreaves the List of appointments of a particular date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Appointment> GetReportByDate(DateTime date)
        {
            return reportData.GetReportByDate(date);
        }

        /// <summary>
        /// Retreaves the List of appointments based on particular option selected from UI by user.
        /// </summary>
        /// <param name="option"></param>
        /// <returns>List of Appointments</returns>
        public List<Appointment> GetReportByAppointment(string option)
        {
            return reportData.GetReportByAppointment(option);
        }
    }
}
