using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using SharpRaven;
using SharpRaven.Data;

namespace ClinicManagementSystemDAL
{
    public class UserData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This is the method that returns the patient list to the user
        /// </summary>
        /// <returns></returns>
        public List<Patients> GetPatientList()
        {
            try
            {
            return db.Patients.Include(t => t.Users).Where(t => t.Users.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<DoctorAvailability> GetDoctorList()
        {
            try
            {
            return db.DoctorAvailability.Include(t => t.Users).Where(t => t.Users.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        public List<Users> GetUsersList(int roleId)
        {
            try
            {
            return db.Users.Where(u => u.RoleId == roleId).Where(t => t.IsDeleted == false).ToList();
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