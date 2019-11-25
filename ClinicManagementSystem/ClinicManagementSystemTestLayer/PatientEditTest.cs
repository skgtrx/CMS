using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemTestLayer
{
    /// <summary>
    /// test cases to test Edit patients details
    /// </summary>
    [TestClass]
    public class PatientEditTest
    {
        // Arrange
        PatientEdit patientEdit = new PatientEdit();
        [TestMethod]
        public void UpdatePatient()
        {
            Users user = new Users();
            Patients patient = new Patients();
            user.Id = 22;
            user.FirstName = "test1";
            user.LastName = "test1";
            user.Email = "test4@gmail.com";
            user.Address = "bangalore";
            user.Phone = "1234512345";
            user.Gender = 1;
            user.DateOfBirth = new DateTime(2019, 10, 21);
            user.Password = "Test@123";
            user.CreatedBy = 0;
            user.RoleId = 4;
            patient.UserId = 22;
            patient.ConservatorName = "xyz";
            patient.ConservatorPhone="0000000000";
            patient.ConservatorRelation = "father";
            patient.Id = 1;
            // Act
            var result = patientEdit.UpdatePatient(user,patient);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
