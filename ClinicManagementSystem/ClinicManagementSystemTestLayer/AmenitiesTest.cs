using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;

namespace ClinicManagementSystemTestLayer
{
    [TestClass]
    public class AmenitiesTest
    {
        /// <summary>
        /// Test cases for Adding Amenities like medicine and tests
        /// </summary>
        [TestMethod]
        public void AddMedicineTest()
        {
            // Arrange
            Amenities amenities = new Amenities();
            Medicines medicine = new Medicines();
            medicine.Name = "Ghutti";
            medicine.Cost = 100;
            // Act
            var result = amenities.AddMedicines(medicine);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddTestsTest()
        {
            // Arrange
            Amenities amenities = new Amenities();
            Tests test = new Tests();
            test.Name = "Test";
            test.Cost = 95;
            // Act
            var result = amenities.AddTests(test);
            // Assert
            Assert.IsTrue(result);
        }
    }
}
