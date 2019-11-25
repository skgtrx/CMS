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
    public class MedicineTableDataTest
    {
        /// <summary>
        /// Test case to test MedicinesInformation method
        /// </summary>



        // Arrange
        MedicineTableData medicineTableData = new MedicineTableData();

        [TestMethod]
        public void MedicinesInformationTest()
        {
            // Act
            var result =medicineTableData.MedicinesInformation();
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
