using ClinicManagementSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class DoctorRemove
    {
        /// <summary>
        /// Deletes Doctor from database based UserId of the doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Flag as true if deleted successfully else false</returns>
        public bool DeleteDoctor(int id)
        {
            DoctorEditRemove doctorRemove = new DoctorEditRemove();
            return doctorRemove.RemoveDoctor(id);
        }
    }
}
