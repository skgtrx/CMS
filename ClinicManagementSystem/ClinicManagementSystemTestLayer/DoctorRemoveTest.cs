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
    public class DoctorRemoveTest
    {
        /// <summary>
        /// test case for deleting doctor
        /// </summary>


        // Arrange
        DoctorRemove doctorRemove = new DoctorRemove();

        [TestMethod]
        public void DeleteDoctorNonExistingTest()
        {
            // Act
            var result = doctorRemove.DeleteDoctor(0);//Non-existing user
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteDoctorExistingTest()
        {
            // Act
            var result = doctorRemove.DeleteDoctor(62);//Existing user
            // Assert
            Assert.IsTrue(result);
        }
    }
}
