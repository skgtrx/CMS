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
    public class DoctorProfileViewTest
    {
        /// <summary>
        /// test cases to get doctor profile data for non-existing doctor
        /// </summary>


        // Arrange
        DoctorProfileView doctorProfileView = new DoctorProfileView(0);

        [TestMethod]
        public void GetDoctorProfileNonExistingDoctorTest()
        {
            // Act
            var result = doctorProfileView.GetDoctorProfile();
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetDoctorAvailabilityNonExistingDoctorTest()
        {
            // Act
            var result = doctorProfileView.GetDoctorAvailability();
            Assert.IsNull(result);
        }

        /// <summary>
        /// test cases to get doctor profile data for existing doctor
        /// </summary>
        DoctorProfileView doctorProfile = new DoctorProfileView(62);//existing doctor
        [TestMethod]
        public void GetDoctorProfileExistingDoctorTest()
        {
            // Act
            var result = doctorProfile.GetDoctorProfile();
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetDoctorAvailabilityExistingDoctorTest()
        {
            // Act
            var result = doctorProfile.GetDoctorAvailability();
            // Assert
            Assert.IsNull(result);
        }
    }
}
