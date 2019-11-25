using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class MedicineTableData
    {
        /// <summary>
        /// Retreves the list of medicines from database
        /// </summary>
        /// <returns>List of Medicines</returns>
        public List<Medicines> MedicinesInformation()
        {
            AllMedicinesAndTestsData medincineData = new AllMedicinesAndTestsData();
            return medincineData.MedicinesData();
        }
    }
}
