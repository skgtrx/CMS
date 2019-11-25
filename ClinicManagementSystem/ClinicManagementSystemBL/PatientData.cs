using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class PatientData
    {
        /// <summary>
        /// Instance of UserInformation for accessing its menthods defined in DAL
        /// Instance of UserData for accessing its methods defined in DAL
        /// </summary>
        UserInformation userInformation = new UserInformation();
        UserData user = new UserData();

        /// <summary>
        /// Retreves the list of Patients with details from database
        /// </summary>
        /// <returns>List of Patients</returns>
        public List<Patients> GetPatientDetails()
        {
            return user.GetPatientList();
        }

        /// <summary>
        /// Validate Phone number
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns>If Phone number exists return UserId else 0</returns>
        public int GetPatientIdIfExists(string Phone)
        {
            return userInformation.PhoneNumberExists(Phone);
        }
    }
}
