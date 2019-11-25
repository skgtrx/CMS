using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemDOL.Models;
using System.Data.SqlClient;
using ClinicManagementSystemDOL.Exceptions;
using System.Data.Entity;
using SharpRaven.Data;
using SharpRaven;

namespace ClinicManagementSystemDAL
{
    public class AmenitiesData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This method can be used to add new medicines to the inventory
        /// </summary>
        /// <param name="medicines" is the model that contains the name as well as the cost of the medicines in the inventory></param>
        /// <returns>This method returns true if the medicine was added successfully else returns false</returns>
        public bool AddMedicines(Medicines medicines)
        {
            try
            {
                if (CheckIfMedicineExists(medicines) == false)
                {
                    Medicines medicineData = db.Medicines.OrderByDescending(t => t.CreatedAt).FirstOrDefault(t => t.Name == medicines.Name);
                    if (medicineData != null)
                    {
                        medicineData.DeletedAt = DateTime.Now;
                        medicineData.ModifiedAt = DateTime.Now;
                        db.Medicines.Attach(medicineData);
                        db.Entry(medicineData).State = EntityState.Modified;
                    }
                    medicines.CreatedAt = DateTime.Now;
                    db.Medicines.Add(medicines);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new MedicineAlreadyExistsEx("Medicine already Exists");
                }
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw new MedicineAlreadyExistsEx("Medicine already Exists");
            }
        }
        /// <summary>
        /// This is the method that can be used to check if the medicine already exists in the inventory
        /// </summary>
        /// <param name="medicines" contains the name and the ost of the medicine></param>
        /// <returns>Returns true if the medicine exists else it returns false</returns>
        private bool CheckIfMedicineExists(Medicines medicines)
        {
            try
            {
                if (db.Medicines.Where(t => t.Cost == medicines.Cost && t.Name == medicines.Name).Count() == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// This method can be used to add new tests to the inventory
        /// </summary>
        /// <param name="medicines" is the model that contains the name as well as the cost of the test in the inventory></param>
        /// <returns>This method returns true if the tests was added successfully else returns false</returns>
        public bool AddTests(Tests test)
        {
            try
            {
                if (CheckIfTestExists(test) == false)
                {
                    Tests testData = db.Tests.OrderByDescending(t => t.CreatedAt).FirstOrDefault(t => t.Name == test.Name);
                    if (testData != null)
                    {
                        testData.DeletedAt = DateTime.Now;
                        testData.ModifiedAt = DateTime.Now;
                        db.Tests.Attach(testData);
                        db.Entry(testData).State = EntityState.Modified;
                    }
                    test.CreatedAt = DateTime.Now;
                    db.Tests.Add(test);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new TestsAlreadyExistsEx("Test already Exists");
                }
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw new TestsAlreadyExistsEx("Test already Exists");
            }
        }
        /// <summary>
        /// This method can be used to check if the test is already present in the database
        /// </summary>
        /// <param name="test" is the model that contains the name and the cost of every test></param>
        /// <returns>Returns true if the test exists else it returns fals</returns>
        private bool CheckIfTestExists(Tests test)
        {
            try
            {
                if (db.Tests.Where(t => t.Cost == test.Cost && t.Name == test.Name).Count() == 0)
                {
                    return false;
                }
                return true;
            }
            
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
    }
}
