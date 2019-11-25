using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemBL;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class TestsTableDataTest
    {
        /// <summary>
        /// test cases to get tests information
        /// </summary>
        [TestMethod]
        public void TestsInformationNullTest()
        {
            // Arrange
            TestsTableData testsTableData = new TestsTableData();
            // Act
            var result = testsTableData.TestsInformation();
            // Assert
            Assert.IsNotNull(result);
        }
    }

}
