using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDAL;

namespace ClinicManagementSystemBL
{
    public class PatientProfileView
    {
        /// <summary>
        /// Property: Id to be used for Accessing Patient's Profile of particular Id.
        /// </summary>
        public int Id { get; set; }

        // Passing Id into object through class constructor
        public PatientProfileView(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Retreaves all the info of Patient's profile based on the Patient's User Id
        /// </summary>
        /// <returns>User Object with Profile Info</returns>
        public Users GetPatientProfile()
        {
            PatientProfileData PatientProfile = new PatientProfileData(Id);
            var Patient = PatientProfile.GetPatientUser();
            Patient.Password = new EncryptPassword().Decrypt(Patient.Password);
            return Patient;
        }

        /// <summary>
        /// Retrives all the info of patient about conservator stored in seperate table in database
        /// </summary>
        /// <returns>Patient Info as Patient Object</returns>
        public Patients GetPatientInfo()
        {
            PatientProfileData PatientInfo = new PatientProfileData(Id);
            var Patient = PatientInfo.GetPatientInfo();
            return Patient;
        }
    }
}
