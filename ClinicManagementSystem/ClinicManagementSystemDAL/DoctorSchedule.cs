using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class DoctorSchedule
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Method to retrieve the time slots of a particular appointment
        /// </summary>
        /// <returns></returns>
        public List<AppointmentTime> GetTimeSlots()
        {
            try
            {
                return db.AppointmentTime.OrderBy(t => t.Id).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// Checks doctor availability
        /// </summary>
        /// <param name="doctorAvailability"></param>
        /// <returns></returns>
        public bool MarkAvailability(List<DoctorAvailability> doctorAvailability)
        {
            using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in doctorAvailability)
                {
                    DoctorAvailability availability = db.DoctorAvailability.SingleOrDefault(t => t.Date == item.Date && t.UserId == item.UserId);
                    if (availability != null)
                    {
                        availability.FromTime = item.FromTime;
                        availability.ToTime = item.ToTime;
                    }
                    else
                    {
                        db.DoctorAvailability.Add(item);
                    }
                    db.SaveChanges();
                }
                dbTransaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                    var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                    ravenClient.Capture(new SentryEvent(e));
                    dbTransaction.Rollback();
                    return false;
            }
        }
                   
        }
        public AppointmentTime GetExistingFromSchedule(int doctorId,DateTime date)
        {
            try
            {
                DoctorAvailability schedule = db.DoctorAvailability.SingleOrDefault(t => t.UserId == doctorId && t.Date == date.Date);
                var timeSlots = db.AppointmentTime.ToList();
                foreach (var item in timeSlots)
                {
                    if ((int)item.Id == (int)schedule.FromTime)
                    {
                        return item;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return null;
            }
        }
        public AppointmentTime GetExistingToSchedule(int doctorId, DateTime date)
        {
            try
            {
                DoctorAvailability schedule = db.DoctorAvailability.SingleOrDefault(t => t.UserId == doctorId && t.Date == date.Date);
                var timeSlots = db.AppointmentTime.ToList();
                foreach (var item in timeSlots)
                {
                    if ((int)item.Id == (int)schedule.ToTime)
                    {
                        return item;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return null;
            }
        }
    }
}
