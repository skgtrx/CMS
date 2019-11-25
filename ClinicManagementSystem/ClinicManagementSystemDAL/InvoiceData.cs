using System;
using System.Collections.Generic;
using System.Linq;
using ClinicManagementSystemDOL.Models;
using System.Data.Entity;
using SharpRaven.Data;
using SharpRaven;

namespace ClinicManagementSystemDAL
{
    public class InvoiceData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This method can be used to retrieve the invoice data
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        public InvoiceRecord GetInvoiceData(int appointmentId)
        {
            try
            {
            return db.InvoiceRecord.Include(t => t.Appointment).Include(t => t.Appointment.Doctor).Include(t => t.Appointment.Patient).FirstOrDefault(t => t.AppointmentId == appointmentId);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public List<MedicineRecord> GetMedicineList(int diagnosisId)
        {
            try
            {
                return db.MedicineRecord.Include(t => t.Medicines).Where(t => t.DiagnosisId == diagnosisId).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public InvoiceRecord GenerateInvoice(int appointmentId)
        {
            InvoiceRecord invoiceRecord = new InvoiceRecord();
            try
            {
                invoiceRecord.AppointmentId = appointmentId;
                invoiceRecord.TotalCost = GetInvoiceCost(appointmentId);
                invoiceRecord.InvoiceDateTime = DateTime.Now;
                db.InvoiceRecord.Add(invoiceRecord);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return null;
            }
            
            return invoiceRecord;
        }

        public Diagnosis GetDiagnosisDetails(int appointmentId)
        {
            try
            {
                return db.Diagnosis.FirstOrDefault(t => t.AppointmentId == appointmentId);
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return null;
            }
            
        }

        public List<TestRecord> GetTestList(int diagnosisId)
        {
            try
            {
            return db.TestRecord.Include(t => t.Tests).Where(t => t.DiagnosisId == diagnosisId).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public double GetInvoiceCost(int appointmentId)
        {
            double totalSum = 0;
            try
            {
                var diagonsisId = db.Diagnosis.FirstOrDefault(d => d.AppointmentId == appointmentId).Id;
                var medicinesIds = db.MedicineRecord.Where(m => m.DiagnosisId == diagonsisId).Select(m => m.MedicineId).ToList();
                foreach (var item in medicinesIds)
                {
                    totalSum += db.Medicines.FirstOrDefault(x => x.Id == item).Cost * db.MedicineRecord.FirstOrDefault(x => x.MedicineId == item && x.DiagnosisId == diagonsisId).Quantity;
                }
                var testsIds = db.TestRecord.Where(t => t.DiagnosisId == diagonsisId).Select(t => t.TestId).ToList();
                foreach (var item in testsIds)
                {
                    totalSum += db.Tests.FirstOrDefault(x => x.Id == item).Cost;
                }
                int? DoctorId = db.Appointment.FirstOrDefault(t => t.Id == appointmentId).DoctorId;
                totalSum += db.DoctorFee.FirstOrDefault(t => t.DoctorId == DoctorId).Fee;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return 0;
            }
            
            return totalSum;

        }
    }
}
