using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class AppointmentsData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This is the method that can be used to display the list of all appointments
        /// </summary>
        /// <returns>A list contatining all the appointments</returns>
        public List<Appointment> DisplayAppointments()
        {
            try
            {
                return db.Appointment.Include(t => t.AppointmentTime).Include(t => t.Patient).Include(t => t.Doctor).Include(t => t.AppointmentStatus).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
}
        /// <summary>
        /// This method is used to display the list of all appointments for a particular patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>The list of appointments</returns>
        public List<Appointment> GetPatientAppointmentsData(int patientId)
        {
            try
            {
            return db.Appointment.Include(t =>t.AppointmentStatus).Include(t=>t.AppointmentTime).Include(t => t.Patient).Include(t => t.Doctor).Where(t => t.PatientId ==patientId).ToList();
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
        /// <returns></returns>
        public List<Appointment> DisplayClosedAppointments()
        {
            try
            {
            return db.Appointment.Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Include(t => t.Patient).Include(t => t.Doctor).Where(t => t.AppointmentStatusId == (int)ClinicManagementSystemDOL.Enums.AppointmentStatus.Closed).ToList();
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
        /// <param name="patientId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Appointment> GetPatientAppointmentsData(int patientId,DateTime date)
        {
            try
            {
            return db.Appointment.Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Where(t => t.PatientId == patientId && t.Date==date).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<AppointmentTime> GetPatientsCurrentAppointmentTimeSlots(int patientId, DateTime date)
        {
            try
            {
                return db.Appointment.Where(t => t.PatientId == patientId && t.Date == date).Select(t => t.AppointmentTime).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Appointment> GetDoctorAppointmentsData(int doctorId)
        {
            try
            {
            return db.Appointment.Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Include(t => t.Patient).Include(t => t.Doctor).Where(t => t.DoctorId == doctorId && t.Patient.IsDeleted == false).ToList();
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
        /// <returns></returns>
        public List<AppointmentTime> GetTimeSlot()
        {
            try
            {
            return db.AppointmentTime.OrderBy(t=>t.Id).ToList();
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
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Appointment> GetDoctorAppointmentsData(int doctorId, DateTime date)
        {
            try
            {
            return db.Appointment.Include(t => t.AppointmentStatus).Include(t => t.AppointmentTime).Where(t => t.DoctorId == doctorId && t.Date== date && t.Patient.IsDeleted == false).ToList();
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