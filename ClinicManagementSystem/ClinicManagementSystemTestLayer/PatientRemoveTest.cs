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
    public class PatientRemoveTest
    {
        /// <summary>
        /// Test cases for removing a patient
        /// </summary>
        [TestMethod]
        public void DeletePatientExistingPatientTest()
        {
            // Arrange
            PatientRemove patientRemove = new PatientRemove();
            // Act
            var result =patientRemove.DeletePatient(43);
            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void DeletePatientNonExistingPatientTest()
        {
            // Arrange
            PatientRemove patientRemove = new PatientRemove();
            // Act
            var result = patientRemove.DeletePatient(0);
            // Assert
            Assert.IsFalse(result);
        }
    }
}
