using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using System;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class AppointmentsTest
    {
        /// <summary>
        /// Test cases for Getting Appointments lists. 
        /// </summary>
        [TestMethod]
        public void GetAppointmentListTest()
        {
            // Arrange
            AppointmentData appointment = new AppointmentData();

            // Act
            var result = appointment.GetAppointmentList().Count > 0;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetPatientAppointmentListTest()
        {
            // Arrange
            AppointmentData appointment = new AppointmentData();

            // Act
            var result = appointment.GetPatientAppointmentList(6).Count > 0;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetDoctorAppointmentListTest()
        {
            // Arrange
            AppointmentData appointment = new AppointmentData();

            // Act
            var result = appointment.GetDoctorAppointmentList(4).Count > 0;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetClosedAppointmentListTest()
        {
            // Arrange
            AppointmentData appointment = new AppointmentData();

            // Act
            var result = appointment.GetClosedAppointmentList().Count > 0;

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test cases for Adding, Editting and Removing Appointment.
        /// </summary>
        [TestMethod]
        public void AddAppointmentTest()
        {
            // Arrange
            Appointments appointments = new Appointments();
            Appointment appointment = new Appointment();
            appointment.DoctorId = 4;
            appointment.PatientId = 6;
            appointment.AppointmentTimeId = 1;
            appointment.AppointmentStatusId = 1;
            appointment.Date = DateTime.Parse("12-14-2019");
            appointment.Details = "Test Appointment";

            // Act
            var result = appointments.AddAppointment(appointment);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EditAppointmentTest()
        {
            // Arrange
            AppointmentEdit editAppointment = new AppointmentEdit();

            // Act
            var result = editAppointment.EditAppointment(1002, 4);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveAppointmentTest()
        {
            // Arrange
            AppointmentRemove appointment = new AppointmentRemove();

            // Act
            var result = appointment.DeleteAppointment(1002);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
