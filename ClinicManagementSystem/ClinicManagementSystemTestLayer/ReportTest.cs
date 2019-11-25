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
    public class ReportTest
    {
        /// <summary>
        /// Test cases for getting the patients report by PatientId
        /// </summary>
        [TestMethod]
        public void GetReportByPatientNullTest()
        {
            // Arrange
            Report report = new Report();
            // Act
            var result = report.GetReportByPatient("4354564543");
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test cases for getting the report by Date
        /// </summary>

        [TestMethod]
        public void GetReportByDateNullTest()
        {
            // Arrange
            Report testsTableData = new Report();
            // Act
            var result = testsTableData.GetReportByDate(new DateTime(2019,10,08));
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test cases for getting the report by Appointment month
        /// </summary>

        [TestMethod]
        public void GetReportByAppointmentNullTest()
        {
            // Arrange
            Report testsTableData = new Report();
            // Act
            var result = testsTableData.GetReportByAppointment("ThisMonth");
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test cases for getting the report by Appointment Status
        /// </summary>
        [TestMethod]
        public void GetReportByAppointmentByStatusOpenNullTest()
        {
            // Arrange
            Report testsTableData = new Report();
            // Act
            var result = testsTableData.GetReportByAppointment(ClinicManagementSystemDOL.Enums.AppointmentStatus.Open.ToString());
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetReportByAppointmentByStatusPendingNullTest()
        {
            Report testsTableData = new Report();
            // Act
            var result = testsTableData.GetReportByAppointment(ClinicManagementSystemDOL.Enums.AppointmentStatus.Pending.ToString());
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetReportByAppointmentByStatusRejectedNullTest()
        {
            // Arrange
            Report testsTableData = new Report();
            // Act
            var result = testsTableData.GetReportByAppointment(ClinicManagementSystemDOL.Enums.AppointmentStatus.Rejected.ToString());
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetReportByAppointmentByStatusClosedNullTest()
        {
            // Arrange
            Report testsTableData = new Report();
            // Act
            var result = testsTableData.GetReportByAppointment(ClinicManagementSystemDOL.Enums.AppointmentStatus.Closed.ToString());
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
