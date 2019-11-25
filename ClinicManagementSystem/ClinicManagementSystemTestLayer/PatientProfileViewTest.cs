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
    public class PatientProfileViewTest
    {
        /// <summary>
        /// Test cases for patient profile view for existing patient
        /// </summary>

        
        // Arrange
        PatientProfileView patientProfileView = new PatientProfileView(22); //userId 40 is a  existing patient


        [TestMethod]
        public void GetPatientProfileExistingTest()
        {
            // Act
            var result = patientProfileView.GetPatientProfile();
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPatientInfoExistingTest()
        {
            // Act
            var result = patientProfileView.GetPatientInfo();
            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Test cases for patient profile view for non-existing patient
        /// </summary>
        PatientProfileView patientProfile = new PatientProfileView(0);
        [TestMethod]
        public void GetPatientProfileNonExistingTest()
        {
            // Act
            var result = patientProfile.GetPatientProfile();
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPatientInfoNonExistingTest()
        {
            // Act
            var result = patientProfile.GetPatientInfo();
            // Assert
            Assert.IsNull(result);
        }
    }
}
