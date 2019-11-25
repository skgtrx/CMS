using ClinicManagementSystemBL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class MedicalHistoryTest
    {
        /// <summary>
        /// test case to get medical history with the diagnosisId
        /// </summary>


        // Arrange
        MedicalHistory medicalHistory = new MedicalHistory();

        [TestMethod]
        public void GetMedicineNamesExistingTest()
        {
            // Act
            var list = medicalHistory.GetMedicineNames(45);//Existing diagnosis
            bool result;
            if (list.Count > 0)
                result = true;
            else
                result = false;
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMedicineNamesNonExistingTest()
        {
            // Act
            var result = medicalHistory.GetMedicineNames(0);//Non-Existing diagnosis
            // Assert
            Assert.AreEqual(result.Count,0);
        }

        /// <summary>
        /// test case to get medical history with the patientId
        /// </summary>
        [TestMethod]
        public void GetDiagnosisHistoryExistingTest()
        {
            // Act
            var list = medicalHistory.GetDiagnosisHistory(40);//Existing patientId
            bool result;
            if (list.Count > 0)
                result = true;
            else
                result = false;
            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GetDiagnosisHistoryNonExistingTest()
        {
            // Act
            var result = medicalHistory.GetDiagnosisHistory(0);//Non-existing userId
            // Assert
            Assert.AreEqual(result.Count, 0);
        }
    }
}
