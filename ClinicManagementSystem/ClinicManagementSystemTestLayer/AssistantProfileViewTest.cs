using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using System;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class AssistantProfileViewTest
    {
        /// <summary>
        /// Test case for getting assistant profile data
        /// </summary>
        [TestMethod]
        public void GetAssistantProfileTest()
        {
            // Arrange
            AssistantProfileView assistant = new AssistantProfileView(1023);

            // Act
            var result = assistant.GetAssistantProfile();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
