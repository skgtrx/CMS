using ClinicManagementSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class PatientRemove
    {
        /// <summary>
        /// Deletes the Patient from Database based on the UserId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Flag as true if deleted successfully else false</returns>
        public bool DeletePatient(int id)
        {
            PatientEditRemove patientRemove = new PatientEditRemove();
            return patientRemove.RemovePatient(id);
        }
    }
}
