using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using System;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class AdminProfileViewTest
    {
        /// <summary>
        /// Test case for getting Admin's profile Data
        /// </summary>
        [TestMethod]
        public void GetAdminProfileTest()
        {
            // Arrange
            AdminProfileView admin = new AdminProfileView(1022);
            // Act
            var result = admin.GetAdminProfile();
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
