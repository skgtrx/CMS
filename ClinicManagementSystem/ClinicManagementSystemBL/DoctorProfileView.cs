using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;

namespace ClinicManagementSystemBL
{
    public class DoctorProfileView
    {
        /// <summary>
        /// Property: Id to be used for Accessing Doctors Data of particular Id.
        /// </summary>
        public int Id { get; set; }

        // Passing Id into object through class constructor
        public DoctorProfileView(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Retreaves Doctor Profile Info from the database based on particular User Id.
        /// </summary>
        /// <returns>Doctor Fee Info in DoctorFee Object</returns>
        public DoctorFee GetDoctorProfile()
        {
            DoctorProfileData DoctorProfile = new DoctorProfileData(Id);
            var Doctor = DoctorProfile.GetDoctorUser();
            Doctor.Doctor.Password = new EncryptPassword().Decrypt(Doctor.Doctor.Password);
            return Doctor;
        }

        /// <summary>
        /// Retereaves the info of doctor's availability from database based on particular User Id
        /// </summary>
        /// <returns>Doctor Availibility Info in DoctorAvailibility Object</returns>
        public DoctorAvailability GetDoctorAvailability()
        {
            DoctorProfileData DoctorAvailability = new DoctorProfileData(Id);
            var DoctorInfo = DoctorAvailability.GetDoctorInfo();
            return DoctorInfo;
        }
    }
}
