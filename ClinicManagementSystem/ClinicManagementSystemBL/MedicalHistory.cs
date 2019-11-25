using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class MedicalHistory
    {
        /// <summary>
        /// Instance of MedicalHistory for accessing methods defined in DAL
        /// </summary>
        MedicalHistoryData medicalHistory = new MedicalHistoryData();

        /// <summary>
        /// Retreves the list of medicine records based on Diagnosis Id
        /// </summary>
        /// <param name="DiagnosisId"></param>
        /// <returns>List of Medicine Recored</returns>
        public List<MedicineRecord> GetMedicineNames(int DiagnosisId)
        {
            return medicalHistory.GetMedicineData(DiagnosisId);
        }

        /// <summary>
        /// Retreaves the list of Diagnosis for a patient based on its Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns>List of Diagnosis</returns>
        public List<Diagnosis> GetDiagnosisHistory(int PatientId)
        {
            return medicalHistory.GetDiagnosisData(PatientId);
        }

        /// <summary>
        /// Retreaves the list of Test records based on diagnosis id
        /// </summary>
        /// <param name="DiagnosisId"></param>
        /// <returns>List of Test Records</returns>
        public List<TestRecord> GetTestNames(int DiagnosisId)
        {
            return medicalHistory.GetTestData(DiagnosisId);
        }
    }
}
