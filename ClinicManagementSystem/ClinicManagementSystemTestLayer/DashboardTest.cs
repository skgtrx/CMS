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
    public class DashboardTest
    {
        // Arrange
        Dashboard dashboard = new Dashboard();
        
        
        /// <summary>
        /// test case to get the count of users
        /// </summary>
        [TestMethod]
        public void GetCountOfUserTest()    //all users
        {
            // Act
            var result = dashboard.GetCountOfUser();
            // Assert
            Assert.AreEqual(42, result);    //42 users curently(13/11/2019)
        }
        [TestMethod]
        public void GetCountOfDoctorTest()  //All doctors
        {
            // Act
            var result = dashboard.GetCountOfDoctor();
            // Assert
            Assert.AreEqual(result, 11);    //11 doctors curently(13/11/2019)
        }
        

        [TestMethod]
        public void GetCountOfPatientTest()
        {
            // Act
            var result = dashboard.GetCountOfPatient();
            // Assert
            Assert.AreEqual(result, 24);    //24 patients curently(13/11/2019)
        }
        [TestMethod]
        public void GetCountOfAppointmentTest()
        {
            // Act
            var result = dashboard.GetCountOfAppointment();
            // Assert
            Assert.AreEqual(result, 29);    //29 appointments in total till today(13/11/2019)
        }
        [TestMethod]
        public void GetCountOfTodaysAppointmentTest()
        {
            // Act
            var result = dashboard.GetCountOfTodaysAppointment();
            // Assert
            Assert.AreEqual(0, result);     //0 appointments for today(13/11/2019)
        }

        /// <summary>
        /// test case to get patient related data
        /// </summary>
        [TestMethod]
        public void GetUpcomingPatientAppointmentsNonExistingPatientTest()
        {
            // Act
            var result = dashboard.GetUpcomingPatientAppointments(0);   //Non-existing user
            // Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void GetUpcomingPatientAppointmentsExistingPatientTest()
        {
            // Act
            var result = dashboard.GetUpcomingPatientAppointments(40);   //Non-existing user
            // Assert
            Assert.AreEqual(result, 1);     //1 upcomming appointment for patientId=40
        }

        [TestMethod]
        public void GetPendingPatientAppointmentsNonExistingPatientTest()
        {
            // Act
            var result = dashboard.GetPendingPatientAppointments(0);
            // Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void GetPendingPatientAppointmentsExistingPatientTest()
        {
            // Act
            var result = dashboard.GetPendingPatientAppointments(40);   //Existing patientId=40
            // Assert
            Assert.AreEqual(result, 14);
        }

        [TestMethod]
        public void GetTotalPatientAppointmentsNonExistingPatient()
        {
            // Act
            var result = dashboard.GetTotalPatientAppointments(0);
            // Assert
            Assert.AreEqual(result, 0);
        }
        [TestMethod]
        public void GetTotalPatientAppointmentsExistingPatient()
        {
            // Act
            var result = dashboard.GetTotalPatientAppointments(40);   //Existing patientId=40
            // Assert
            Assert.AreEqual(result, 18);
        }
    }
}
