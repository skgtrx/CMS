using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class DoctorProfileData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public int Id { get; set; }

        public DoctorProfileData(int id)
        {
            Id = id;
        }
        /// <summary>
        /// used to get doctor information
        /// </summary>
        /// <returns></returns>
        public DoctorFee GetDoctorUser()
        {
            try
            {
                var Doctor = db.DoctorFee.Include(t => t.Doctor).FirstOrDefault(a => a.Doctor.Id == Id && a.Doctor.RoleId == (int)Roles.Doctor && a.Doctor.IsDeleted == false);
                return Doctor;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public DoctorAvailability GetDoctorInfo()
        {
            try
            {
                var DoctorInfo = db.DoctorAvailability.Include(t => t.Users).FirstOrDefault(a => a.UserId == Id && a.Users.IsDeleted == false);
                return DoctorInfo;
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
