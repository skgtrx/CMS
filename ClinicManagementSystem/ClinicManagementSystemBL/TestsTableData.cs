using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class TestsTableData
    {
        /// <summary>
        /// Retreaves the list of all the Tests from Database.
        /// </summary>
        /// <returns>List of Tests</returns>
        public List<Tests> TestsInformation()
        {
            AllMedicinesAndTestsData testData = new AllMedicinesAndTestsData();
            return testData.TestsData();
        }
    }

}
