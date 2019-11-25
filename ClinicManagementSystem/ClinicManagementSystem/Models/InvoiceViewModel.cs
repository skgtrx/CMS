using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel(int AppointmentId)
        {
            InvoiceDetails invoiceDetails = new InvoiceDetails();

            Diagnosis = invoiceDetails.GetDiagnosis(AppointmentId);
            Invoice = invoiceDetails.GetInvoiceDetails(AppointmentId);
            if (Invoice == null)
            {
                invoiceDetails.GenerateInvoice(AppointmentId);
                Invoice = invoiceDetails.GetInvoiceDetails(AppointmentId);
            }
            Medicines = invoiceDetails.GetMedicineListForInvoice(Diagnosis.Id);
            Tests = invoiceDetails.GetTestListForInvoice(Diagnosis.Id);

            this.DoctorFee = Invoice.TotalCost - (Medicines.Sum(t => t.Medicines.Cost * t.Quantity) + Tests.Sum(t => t.Tests.Cost));

            Age = invoiceDetails.GetAge(Invoice.Appointment.Patient.DateOfBirth);
        }

        public Diagnosis Diagnosis { get; set; }

        public List<MedicineRecord> Medicines { get; set; }

        public List<TestRecord> Tests { get; set; }

        public InvoiceRecord Invoice { get; set; }

        public double DoctorFee { get; set; }

        public int Age { get; set; }
    }
}