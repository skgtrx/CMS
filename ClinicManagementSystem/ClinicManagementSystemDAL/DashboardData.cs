using ClinicManagementSystemDOL.Enums;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class DashboardData
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //For Admin and assistant dashboard
        /// <summary>
        /// Method to get the user count
        /// </summary>
        /// <returns></returns>
        public int GetUserCount()
        {
            try
            {
                return db.Users.Count(t => t.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// Method to get patient count
        /// </summary>
        /// <returns></returns>
        public int GetPatientCount()
        {
            try
            {
            return db.Users.Count(t => t.RoleId == (int)Roles.Patient && t.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// Method to get doctor count
        /// </summary>
        /// <returns></returns>
        public int GetDoctorCount()
        {
            try
            {
            return db.Users.Count(t => t.RoleId == (int)Roles.Doctor && t.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// Method to get appointment count
        /// </summary>
        /// <returns></returns>
        public int GetAppointmentCount()
        {
            try
            {
                return db.Appointment.Include(t => t.Patient).Include(t => t.Doctor).Count(t => t.Doctor.IsDeleted == false && t.Patient.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// Method to get the count of todays appointments
        /// </summary>
        /// <returns></returns>
        public int GetTodaysAppointmentCount()
        {
            try
            {
            return db.Appointment.Include(t => t.Patient).Include(t => t.Doctor).Count(t => t.Date == DateTime.Today && t.Doctor.IsDeleted == false && t.Patient.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        //For Patients Dashboard
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public int GetPendingPatientAppointments(int PatientId)
        {
            try
            {
            return db.Appointment.Include(t => t.Patient).Count(t => t.AppointmentStatusId == (byte)AppointmentStatus.Pending && t.PatientId == PatientId && t.Patient.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public int GetTotalPatientAppointments(int PatientId)
        {
            try
            {
            return db.Appointment.Count(t => t.PatientId == PatientId && t.Patient.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public int GetUpcomingPatientAppointments(int PatientId)
        {
            try
            {
            return db.Appointment.Count(t => t.Date > DateTime.Today && t.PatientId == PatientId && t.Patient.IsDeleted == false);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <returns></returns>
        //For Doctor Dashboard
        public int GetDoctorAppointments(int DoctorId)
        {
            try
            {
            return db.Appointment.Count(t => t.DoctorId == DoctorId);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <returns></returns>
        public int GetDoctorTodaysAppointments(int DoctorId)
        {
            try
            {
            return db.Appointment.Count(t => t.DoctorId == DoctorId && t.Date == DateTime.Today.Date);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <returns></returns>
        public int GetDoctorsPatient(int DoctorId)
        {
            try
            {
            return db.Appointment.Where(t => t.DoctorId == DoctorId).Select(t => t.PatientId).Distinct().Count();
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
