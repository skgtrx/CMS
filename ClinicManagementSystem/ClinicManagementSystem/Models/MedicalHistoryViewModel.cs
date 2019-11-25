using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ClinicManagementSystemBL;

namespace ClinicManagementSystem.Models
{
    public class MedicalHistoryViewModel
    {
        public Diagnosis DiagnosisHistories { get; set; }

        public List<MedicineRecord> MedicineList { get; set; }

        public List<TestRecord> TestList { get; set; }
        public List<MedicineRecord> GetMedicines(int DiagnosisId)
        {
            return new MedicalHistory().GetMedicineNames(DiagnosisId);
        }

        public List<Diagnosis> GetDiagnosisHistory(int PatientId)
        {
            return new MedicalHistory().GetDiagnosisHistory(PatientId);
        }

        public List<TestRecord> GetTests(int DiagnosisId)
        {
            return new MedicalHistory().GetTestNames(DiagnosisId);
        }
    }
}