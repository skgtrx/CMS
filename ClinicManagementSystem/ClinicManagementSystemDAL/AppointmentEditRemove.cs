using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class AppointmentEditRemove
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This method is used to remove the unwanted appointments from the system
        /// </summary>
        /// <param name="id" is the ID of a particular apointment></param>
        /// <returns>True if the appointment is successfully removed else it returns false</returns>
        public bool RemoveAppointment(int id)
        {
            try
            {
                if (db.Appointment.SingleOrDefault(u => u.Id == id) != null)
                {
                    Appointment appointment = db.Appointment.SingleOrDefault(u => u.Id == id);
                    db.Appointment.Remove(appointment);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return false;
            }

        }
        /// <summary>
        /// This method is used to edit appointments based on ID and status
        /// </summary>
        /// <param name="id" is the appointment ID of an appointment></param>
        /// <param name="status" is the status of the appointment></param>
        /// <returns>True if the appointment status is changed else it returns false</returns>
        public bool EditAppointment(int id, int status)
        {
            try
            {
                if (db.Appointment.SingleOrDefault(u => u.Id == id) != null)
                {
                    Appointment appointment = db.Appointment.SingleOrDefault(u => u.Id == id);
                    appointment.ModifiedAt = DateTime.Now;
                    appointment.AppointmentStatusId = (byte)status;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return false;
            }

        }
    }
}
