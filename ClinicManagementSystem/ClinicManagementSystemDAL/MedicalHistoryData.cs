using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class MedicalHistoryData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Method to get the medicine data from the database
        /// </summary>
        /// <param name="DiagnosisId"></param>
        /// <returns></returns>
        public List<MedicineRecord> GetMedicineData(int DiagnosisId)
        {
            try
            {
            return db.MedicineRecord.Include(t => t.Medicines).Where(t => t.DiagnosisId == DiagnosisId).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<Diagnosis> GetDiagnosisData(int PatientId)
        {
            try
            {
            return db.Diagnosis.Include(t => t.Appointment).Include(t => t.Appointment.Patient).Include(t => t.Appointment.Doctor).Where(t => t.Appointment.PatientId == PatientId).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<TestRecord> GetTestData(int DiagnosisId)
        {
            try
            {
            return db.TestRecord.Include(t => t.Tests).Where(t => t.DiagnosisId == DiagnosisId).ToList();
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
