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
    public class AddMedicinesAndTestViewModel
    {

        public int AppointmentId { get; set; }
        public string Remark { get; set; }
        public Diagnosis Diagnosis { get; set; }
        [ForeignKey("Diagnosis")]
        [Key]
        public int DiagnosisId { get; set; }

        public List<Medicines> MedicinesList { get; set; }

        public List<Tests> TestsList { get; set; }

        public string[] Medicines { get; set; }
        public string[] Tests { get; set; }

        AddDiagnosisAndRecord addDiagnosisAndRecord = new AddDiagnosisAndRecord();
        public List<MedicineRecord> ToMedicineList()
        {
            if (Medicines==null) return null;
            List<MedicineRecord> medicinesList = new List<MedicineRecord>();
            for (int i = 0; i < Medicines.Length; i += 4)
            {
                var medicine = new MedicineRecord();
                medicine.MedicineId = addDiagnosisAndRecord.GetMedicineId(Medicines[i]);
                medicine.Quantity = Int32.Parse(Medicines[i + 1]) * Int32.Parse(Medicines[i + 2]);
                if (medicinesList.FirstOrDefault(t => t.MedicineId == medicine.MedicineId) != null)
                {
                    continue;
                }
                medicinesList.Add(medicine);
            }
            return medicinesList;
        }

        public List<TestRecord> ToTestsList()
        {
            if (Tests == null) return null;
            List<TestRecord> testsList = new List<TestRecord>();

            for (int i = 0; i < Tests.Length; i += 2)
            {
                var test = new TestRecord();
                test.TestId = addDiagnosisAndRecord.GetTestId(Tests[i]);
                if (testsList.FirstOrDefault(t => t.TestId == test.TestId) != null)
                {
                    continue;
                }
                testsList.Add(test);
            }
            return testsList;
        }
    }
}