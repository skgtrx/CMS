using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDAL
{
    public class ScheduleBasedAppointment
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Scheduler for the doctor
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<DoctorAvailability> GetAvailability(DateTime date)
        {
            try
            {
                return db.DoctorAvailability.Where(t => t.Date == date).Include(t => t.Users).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return null;
            }
        }
        public DoctorAvailability GetAvailability(int doctorId,DateTime date)
        {
            try
            {
                return db.DoctorAvailability.SingleOrDefault(t => t.UserId == doctorId && t.Date == date);
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
