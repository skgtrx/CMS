using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using System;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class AccountsTest
    {
        /// <summary>
        /// Test Cases for ValidateUserLogin Method
        /// </summary>
        
        [TestMethod]
        public void LoginWithNullInputTest()
        {
            // Arrange
            Accounts account = new Accounts();
            // Act
            bool result = account.ValidateUserLogin("", "");
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LoginWithNullEmailTest()
        {
            // Arrange
            Accounts account = new Accounts();
            // Act
            bool result = account.ValidateUserLogin("", "vGGOWn4ww4sWpXAB/fQoiC5YOR3KV6EPEU0bYnlcGCU=");
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LoginWithNullPassTest()
        {
            // Arrange
            Accounts account = new Accounts();
            // Act
            bool result = account.ValidateUserLogin("Admin@gmail.com", "");
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateUserLoginTest()
        {
            // Arrange
            Accounts account = new Accounts();
            // Act
            bool result = account.ValidateUserLogin("skgtrx@gmail.com", "vGGOWn4ww4sWpXAB/fQoiC5YOR3KV6EPEU0bYnlcGCU=");
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test Cases for SessionUserInfo Method
        /// </summary>

        [TestMethod]
        public void SessionUserInfoTest()
        {
            // Arrange
            Accounts account = new Accounts();
            Users user = new Users();
            user.Email = "skgtrx@gmail.com";
            // Act
            var sessionInfo = account.SessionUserInfo(user);
            bool result = (sessionInfo == null); 
            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test Cases for AddUser, 
        /// DeleteAdmin, DeleteAssistant, DeletePatient, DeleteDoctor,
        /// AdminEdit
        /// </summary>

        [TestMethod]
        public void AddAdminTest()
        {
            // Arrange
            Users user = new Users();
            user.FirstName = "TestAdmin";
            user.LastName = "TestAdmin";
            user.Password = "vGGOWn4ww4sWpXAB/fQoiC5YOR3KV6EPEU0bYnlcGCU=";
            user.Phone = "1111111111";
            user.Address = "TestAddress";
            user.DateOfBirth = DateTime.Parse("03-02-1999");
            user.Email = "Test@gmail.com";
            user.Gender = 1;
            user.RoleId = Convert.ToInt32(Roles.Admin);
            Accounts account = new Accounts();

            // Act
            bool result = account.AddUser(user);
            
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAdminTest()
        {
            // Arrange
            Users user = new Users();
            user.FirstName = "TestAdmin";
            user.LastName = "TestAdmin";
            user.Password = "vGGOWn4ww4sWpXAB/fQoiC5YOR3KV6EPEU0bYnlcGCU=";
            user.Phone = "1111111111";
            user.Address = "TestAddress";
            user.DateOfBirth = DateTime.Parse("03-02-1999");
            user.Email = "Test@gmail.com";
            user.Gender = 1;
            user.RoleId = Convert.ToInt32(Roles.Admin);
            Accounts account = new Accounts();
            var sessionInfo = account.SessionUserInfo(user);
            user.Id = sessionInfo.Id;
            AdminEdit admin = new AdminEdit();

            // Act
            var result = admin.UpdateAdmin(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveAdminTest()
        {
            // Arrange
            Users user = new Users();
            user.Email = "Test@gmail.com";
            AdminRemove removeAccount = new AdminRemove();
            Accounts account = new Accounts();
            var sessionInfo = account.SessionUserInfo(user);

            // Act
            bool result = removeAccount.DeleteAdmin(sessionInfo.Id);

            // Assert
            Assert.IsTrue(result);
        }
                

        [TestMethod]
        public void AddAssistantTest()
        {
            // Arrange
            Users user = new Users();
            user.FirstName = "TestAssistant";
            user.LastName = "TestAssistant";
            user.Password = "vGGOWn4ww4sWpXAB/fQoiC5YOR3KV6EPEU0bYnlcGCU=";
            user.Phone = "1111111111";
            user.Address = "TestAddress";
            user.DateOfBirth = DateTime.Parse("03-02-1999");
            user.Email = "Test@gmail.com";
            user.Gender = 1;
            user.RoleId = Convert.ToInt32(Roles.Assistant);
            Accounts account = new Accounts();

            // Act
            bool result = account.AddUser(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAssistantTest()
        {
            // Arrange
            Users user = new Users();
            user.FirstName = "TestAssistant";
            user.LastName = "TestAssistant";
            user.Password = "vGGOWn4ww4sWpXAB/fQoiC5YOR3KV6EPEU0bYnlcGCU=";
            user.Phone = "1111111111";
            user.Address = "TestAddress";
            user.DateOfBirth = DateTime.Parse("03-02-1999");
            user.Email = "Test@gmail.com";
            user.Gender = 1;
            user.RoleId = Convert.ToInt32(Roles.Assistant);
            Accounts account = new Accounts();
            var sessionInfo = account.SessionUserInfo(user);
            user.Id = sessionInfo.Id;
            AdminEdit admin = new AdminEdit();

            // Act
            var result = admin.UpdateAdmin(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveAssistantTest()
        {
            // Arrange
            Users user = new Users();
            user.Email = "Test@gmail.com";
            AssistantRemove removeAccount = new AssistantRemove();
            Accounts account = new Accounts();
            var sessionInfo = account.SessionUserInfo(user);

            // Act
            bool result = removeAccount.DeleteAssistant(sessionInfo.Id);

            // Assert
            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void AddPatientTest()
        //{
        //    // Arrange
        //    Users user = new Users();
        //    Patients patient = new Patients();
        //    user.RoleId = Convert.ToInt32(Roles.Patient);
        //    user.

        //    user.Email = "skgtrx@gmail.com";
        //    // Act
        //    var sessionInfo = account.SessionUserInfo(user);
        //    bool result = (sessionInfo == null);
        //    // Assert
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void RemovePatientTest()
        //{
        //    // Arrange
        //    Users user = new Users();
        //    Patients patient = new Patients();
        //    user.RoleId = Convert.ToInt32(Roles.Patient);
        //    user.

        //    user.Email = "skgtrx@gmail.com";
        //    // Act
        //    var sessionInfo = account.SessionUserInfo(user);
        //    bool result = (sessionInfo == null);
        //    // Assert
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void AddDoctorTest()
        //{
        //    // Arrange
        //    Accounts account = new Accounts();
        //    Users user = new Users();
        //    user.Email = "skgtrx@gmail.com";
        //    // Act
        //    var sessionInfo = account.SessionUserInfo(user);
        //    bool result = (sessionInfo == null);
        //    // Assert
        //    Assert.IsFalse(result);
        //}
    }
}
