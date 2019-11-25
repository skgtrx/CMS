using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class AllMedicinesAndTestsData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This method can be used to retrieve all the medicines that are present in the inventory
        /// </summary>
        /// <returns>The list of medicines present in the inventory</returns>
        public List<Medicines> MedicinesData()
        {
            try
            {
                return db.Medicines.Where(t => t.DeletedAt == null).ToList();
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        /// <summary>
        /// This method can be used to retrieve all the tests that are present in the inventory
        /// </summary>
        /// <returns>The list of tests present in the inventory</returns>
        public List<Tests> TestsData()
        {
            try
            { 
            return db.Tests.Where(t => t.DeletedAt == null).ToList();
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
