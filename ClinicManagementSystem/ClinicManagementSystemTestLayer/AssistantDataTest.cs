using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using System;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class AssistantDataTest
    {
        /// <summary>
        /// Test case for Getting Assistants list
        /// </summary>
        [TestMethod]
        public void AssistantListNullTest()
        {
            // Arrange
            AssistantData assistantList = new AssistantData();

            // Act
            var result = assistantList.GetAssistantsList().Count > 0 ;

            // Assert
            Assert.IsTrue(result);
        }
    }
}
