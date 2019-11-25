using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using SharpRaven;
using SharpRaven.Data;

namespace ClinicManagementSystemDAL
{
    public class DoctorData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DoctorFee> GetDoctorsList()
        {
            try
            {
            return db.DoctorFee.Include(t=>t.Doctor).Where(t => t.Doctor.RoleId == (int)Roles.Doctor && t.Doctor.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<DoctorFee> GetAllDoctorsList()
        {
            try
            {
            return db.DoctorFee.Include(t => t.Doctor).Where(t => t.Doctor.RoleId == (int)Roles.Doctor && t.Doctor.IsDeleted == false).Distinct().ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<DoctorSpecialization> GetDoctorSpecilizationsList()
        {
            try
            {
                return db.DoctorSpecialization.Include(t => t.Users).Include(t=>t.Specialization).Where(t => t.Users.RoleId == (int)Roles.Doctor && t.Users.IsDeleted == false).ToList();
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
