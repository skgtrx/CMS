using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class DiagnosisAndRecordsAdd
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Method can be used to get the diagnosis Id
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        public int GetDiagnosisId(int appointmentId)
        {
            try
            {
                return db.Diagnosis.FirstOrDefault(d => d.AppointmentId == appointmentId).Id;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return 0;//check for exceptions
            }
        }
        /// <summary>
        /// This method can be used to get the medicine Id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetMedicineId(string name)
        {
            try
            {
                return db.Medicines.SingleOrDefault(m => m.Name == name && m.DeletedAt == null).Id;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return 0;//check for exceptions
            }
        }
        /// <summary>
        /// This method can be used to get the test Id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetTestId(string name)
        {
            try
            {
                return db.Tests.SingleOrDefault(t => t.Name == name && t.DeletedAt == null).Id;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return 0;//check for exceptions
            }
        }
        /// <summary>
        /// This method can be used to retrieve the medicines and tests of a diagnosis
        /// </summary>
        /// <param name="diagnosis"></param>
        /// <param name="userObjects"></param>
        /// <returns></returns>
        public bool AddDiagnosisAndRecords(Diagnosis diagnosis, params object[] userObjects)
        {
            using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Diagnosis.Add(diagnosis);
                    int diagnosisId = GetDiagnosisId(diagnosis.AppointmentId);
                    if (userObjects.Length>0)
                    {
                        if (userObjects[0] != null)
                        {
                            List<MedicineRecord> medicines = (List<MedicineRecord>)userObjects[0];
                            foreach (var medicine in medicines)
                            {
                                MedicineRecord medicineRecord = new MedicineRecord();
                                medicineRecord.MedicineId = medicine.MedicineId;
                                medicineRecord.DiagnosisId = diagnosisId;
                                medicineRecord.Quantity = medicine.Quantity;
                                db.MedicineRecord.Add(medicineRecord);
                            }

                        }
                        if (userObjects[1] != null)
                        {
                            var tests = (List<TestRecord>)userObjects[1];
                            foreach (var test in tests)
                            {
                                TestRecord testRecord = new TestRecord();
                                testRecord.TestId = test.TestId;
                                testRecord.DiagnosisId = diagnosisId;
                                db.TestRecord.Add(testRecord);
                            }
                        }
                    }
                    
                    var appointment=db.Appointment.SingleOrDefault(t => t.Id == diagnosis.AppointmentId);
                    appointment.AppointmentStatusId = (int) ClinicManagementSystemDOL.Enums.AppointmentStatus.Closed;
                    appointment.ModifiedAt = DateTime.Now;
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
