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
    public class PatientDataTest
    {
        /// <summary>
        /// Test case for testing patients list retrival
        /// </summary>


        // Arrange
        PatientData patientData = new PatientData();

        [TestMethod]
        public void GetPatientDetailsTest()
        {
            // Act
            var result = patientData.GetPatientDetails();
            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Test case for getting the userId if exists using phone number
        /// </summary>
        [TestMethod]
        public void GetPatientIdIfExistsExistingUserTest()
        {
            // Act
            var result = patientData.GetPatientIdIfExists("9999977777");
            // Assert
            Assert.AreEqual(result.ToString(),"23");
        }
    }
}
