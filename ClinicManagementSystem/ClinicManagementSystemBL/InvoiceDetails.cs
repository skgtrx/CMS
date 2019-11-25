using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDAL;

namespace ClinicManagementSystemBL
{
    public class InvoiceDetails
    {
        /// <summary>
        /// Instance of InvoiceData for accessing its methods defined in DAL
        /// </summary>
        InvoiceData invoiceData = new InvoiceData();

        /// <summary>
        /// Retrives the list of MedicineRecord ad Invoice based on Diagnosis Id
        /// </summary>
        /// <param name="diagnosisId"></param>
        /// <returns>List of Medicine Record</returns>
        public List<MedicineRecord> GetMedicineListForInvoice(int diagnosisId)
        {
            return invoiceData.GetMedicineList(diagnosisId);
        }

        /// <summary>
        /// Retrives the Diagnosis Info based on Appointment Id from database
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>Diagnosis Info as Diagnosis Object</returns>
        public Diagnosis GetDiagnosis(int appointmentId)
        {
            return invoiceData.GetDiagnosisDetails(appointmentId);
        }

        /// <summary>
        /// Gerenrates Invoice and stores it into database
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>Invoice Record as InvoiceRecord Object</returns>
        public InvoiceRecord GenerateInvoice(int appointmentId)
        {
            return invoiceData.GenerateInvoice(appointmentId);
        }

        /// <summary>
        /// Retrives the list of tests for a particular diagnosis
        /// </summary>
        /// <param name="diagnosisId"></param>
        /// <returns>List of Tests</returns>
        public List<TestRecord> GetTestListForInvoice(int diagnosisId)
        {
            return invoiceData.GetTestList(diagnosisId);
        }

        /// <summary>
        /// Retrives Invoice Record based on Appointment Id
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>Invoice Data as InvoiceRecord object</returns>
        public InvoiceRecord GetInvoiceDetails(int appointmentId)
        {
            return invoiceData.GetInvoiceData(appointmentId);
        }

        /// <summary>
        /// Calculate Age from Date of Birth
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns>Int: Age</returns>
        public int GetAge(DateTime dateOfBirth)
        {
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                return DateTime.Now.Year - dateOfBirth.Year - 1;
            }

            return DateTime.Now.Year - dateOfBirth.Year;
        }
    }
}
