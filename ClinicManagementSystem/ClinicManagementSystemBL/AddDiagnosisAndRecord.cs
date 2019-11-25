using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;

namespace ClinicManagementSystemBL
{
    public class AddDiagnosisAndRecord
    {
        /// <summary>
        /// Instance of DiagnosisAndRecordsAdd for accessing the methods and properties defined in DAL
        /// </summary>
        DiagnosisAndRecordsAdd addDiagnosisAndRecords = new DiagnosisAndRecordsAdd();

        /// <summary>
        /// Adds a record of successful diagnosis in Database
        /// </summary>
        /// <param name="diagnosis"></param>
        /// <param name="userObjects"></param>
        /// <returns>Flag as true if Record added successfully else false</returns>
        public bool CreateDiagnosisAndRecord(Diagnosis diagnosis, params object[] userObjects)
        {
            try
            {
                return addDiagnosisAndRecords.AddDiagnosisAndRecords(diagnosis, userObjects);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Getting Id of medicine based on the name of it from Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Medicine Id for further uses</returns>
        public int GetMedicineId(string name)
        {
            return addDiagnosisAndRecords.GetMedicineId(name);
        }

        /// <summary>
        /// Getting Id of test based on the name of it from Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Test Id for further uses</returns>
        public int GetTestId(string name)
        {
            return addDiagnosisAndRecords.GetTestId(name);
        }
    }
}
