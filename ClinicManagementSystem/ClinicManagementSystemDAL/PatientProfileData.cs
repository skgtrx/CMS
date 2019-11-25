using System;
using System.Linq;
using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;

namespace ClinicManagementSystemDAL
{
    public class PatientProfileData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public int Id { get; set; }
        public PatientProfileData(int id)
        {
            Id = id;
        }

        public Users GetPatientUser()
        {
            try
            {
            var Patient = db.Users.FirstOrDefault(a => a.Id == Id && a.RoleId == 4);
            return Patient;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public Patients GetPatientInfo()
        {
            try
            {
            var PatientInfo = db.Patients.FirstOrDefault(a => a.UserId == Id);
            return PatientInfo;
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
