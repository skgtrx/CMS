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
    public class InvoiceDetailsTest
    {
        // Arrange
        InvoiceDetails invoiceDetails = new InvoiceDetails();


        /// <summary>
        /// test caeses for GetMedicineListForInvoice with the diagnosisId
        /// </summary>
        [TestMethod]
        public void GetMedicineListForInvoiceExistingTest()
        {
            // Act
            var list = invoiceDetails.GetMedicineListForInvoice(45);//Existing diagnosis
            bool result;
            if (list.Count > 0)
                result = true;
            else
                result = false;
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMedicineListForInvoiceNonExistingTest()
        {
            // Act
            var result = invoiceDetails.GetMedicineListForInvoice(0);//Non-Existing diagnosis
            // Assert
            Assert.AreEqual(result.Count, 0);
        }
        /// <summary>
        /// test caeses for getting diagnosis details with appointmentId
        /// </summary>

        [TestMethod]
        public void GetDiagnosisExistingTest()
        {
            // Act
            var result = invoiceDetails.GetDiagnosis(0);//Non-Existing appointment
            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetDiagnosisNonExistingTest()
        {
            // Act
            var result = invoiceDetails.GetDiagnosis(61);//Existing appointment
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// test caeses for getting diagnosis details with appointmentId
        /// </summary>

        [TestMethod]
        public void GenerateInvoiceNonExistingTest()
        {
            // Act
            var result = invoiceDetails.GenerateInvoice(0);//Non-Existing appointment
            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GenerateInvoiceExistingTest()
        {
            // Act
            var result = invoiceDetails.GenerateInvoice(61);//Existing appointment
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// test caeses for GetTestsListForInvoice with the diagnosisId
        /// </summary>
        [TestMethod]
        public void GetTestListForInvoiceExistingTest()
        {
            // Act
            var list = invoiceDetails.GetTestListForInvoice(45);//Existing diagnosis
            bool result;
            if (list.Count > 0)
                result = true;
            else
                result = false;
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetTestListForInvoiceNonExistingTest()
        {
            // Act
            var result = invoiceDetails.GetTestListForInvoice(0);//Non-Existing diagnosis
            // Assert
            Assert.AreEqual(result.Count, 0);
        }

        /// <summary>
        /// test caeses for getting diagnosis details with appointmentId
        /// </summary>

        [TestMethod]
        public void GetInvoiceDetailsExistingTest()
        {
            // Act
            var result = invoiceDetails.GetInvoiceDetails(0);//Non-Existing appointment
            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetInvoiceDetailsNonExistingTest()
        {
            // Act
            var result = invoiceDetails.GetInvoiceDetails(61);//Existing appointment
            // Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// test caeses for getting age with the given dateOfBirth
        /// </summary>
        [TestMethod]
        public void GetAgeTest()
        {
            // Act
            var result = invoiceDetails.GetAge(new DateTime(1997, 7, 11));
            // Assert
            Assert.AreEqual(22, result);
        }
    }
}
