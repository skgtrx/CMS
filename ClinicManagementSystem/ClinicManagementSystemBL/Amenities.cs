using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class Amenities
    {
        /// <summary>
        /// Adds Medcines into the Database
        /// </summary>
        /// <param name="medicines"></param>
        /// <returns>Flag as true if added successfully else false</returns>
        public bool AddMedicines(Medicines medicines)
        {
            return (new AmenitiesData()).AddMedicines(medicines);
        }

        /// <summary>
        /// Adds Tests into the Database
        /// </summary>
        /// <param name="tests"></param>
        /// <returns>Flag as true if added successfully else false</returns>
        public bool AddTests(Tests tests)
        {
            return (new AmenitiesData()).AddTests(tests);
        }
    }
}
