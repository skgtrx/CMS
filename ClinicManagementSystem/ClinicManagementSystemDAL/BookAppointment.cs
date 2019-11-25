using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemDOL.Models;
using System.Data.Entity;
using ClinicManagementSystemDOL.Exceptions;
using SharpRaven;
using SharpRaven.Data;

namespace ClinicManagementSystemDAL
{
    public class BookAppointment
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Method is used to book appointments
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public bool BookAppointments(Appointment appointment)
        {
            using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                        db.Appointment.Add(appointment);
                        db.SaveChanges();
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
    }
}
