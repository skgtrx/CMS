using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using System;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class AdminDataTest
    {
        /// <summary>
        /// Test cases for Getting Admin List
        /// </summary>
        [TestMethod]
        public void AdminListNullTest()
        {
            // Arrange
            AdminData adminList = new AdminData();

            // Act
            var result = adminList.GetAdminsList().Count > 0;

            // Assert
            Assert.IsTrue(result);
        }
    }
}
