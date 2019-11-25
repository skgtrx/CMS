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
    public class DoctorsDataTest
    {
        // Arrange
        DoctorsData doctorsData = new DoctorsData();
        
        
        /// <summary>
        /// Test cases for getting doctors List
        /// </summary>
        [TestMethod]
        public void GetDoctorsListTest()
        {
            // Act
            var list = doctorsData.GetDoctorsList();
            bool result;
            if (list.Count > 0) result = true;
            else result = false;
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test cases for getting doctors specilization List
        /// </summary>
        [TestMethod]
        public void GetDoctorSpecilizationListTest()
        {
            // Act
            var list = doctorsData.GetDoctorSpecilizationList();
            bool result;
            if (list.Count > 0) result = true;
            else result = false;
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test cases for getting available time slots
        /// </summary>
        [TestMethod]
        public void GetDoctorAvailableTimeSlotsExistingTest()
        {
            // Act
            var list = doctorsData.GetDoctorAvailableTimeSlots(27, new DateTime(2019, 10, 11));
            bool result;
            if (list.Count > 0) result = true;
            else result = false;
            // Assert
            Assert.IsTrue(result);
        }
    }
}
