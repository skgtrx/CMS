using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using SharpRaven;
using SharpRaven.Data;

namespace ClinicManagementSystemDAL
{
    public class ReportData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Method to get report by the patients
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public List<Appointment> GetReportByPatients(string phoneNumber)
        {
            try
            {
            return db.Appointment.Include(t => t.Doctor).Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Include(t => t.Patient).Where(t => t.Patient.Phone == phoneNumber).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<Appointment> GetReportByDate(DateTime date)
        {
            try
            {
            return db.Appointment.Include(t => t.Doctor).Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Include(t => t.Patient).Where(t => t.Date == date).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<Appointment> GetReportByAppointment(string option)
        {
            try
            {
            if (option.Equals("ThisMonth",StringComparison.InvariantCultureIgnoreCase))
            {
                return db.Appointment.Include(t => t.Doctor).Include(t => t.Patient).Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Where(t => t.Date.Month == DateTime.Now.Month && t.Date.Year == DateTime.Now.Year).ToList();
            }
            else if (option.Equals("Pending", StringComparison.InvariantCultureIgnoreCase))
            {
                return db.Appointment.Include(t => t.Doctor).Include(t => t.Patient).Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Where(t => t.AppointmentStatus.Name == "Pending").ToList();
            }
            else if (option.Equals("Open", StringComparison.InvariantCultureIgnoreCase))
            {
                return db.Appointment.Include(t => t.Doctor).Include(t => t.Patient).Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Where(t => t.AppointmentStatus.Name == "Open").ToList();
            }
            else if (option.Equals("Rejected", StringComparison.InvariantCultureIgnoreCase))
            {
                return db.Appointment.Include(t => t.Doctor).Include(t => t.Patient).Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Where(t => t.AppointmentStatus.Name == "Rejected").ToList();
            }
            else
            {
                return db.Appointment.Include(t => t.Doctor).Include(t => t.Patient).Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Where(t => t.AppointmentStatus.Name == "Closed").ToList();
            }
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
    }
}
