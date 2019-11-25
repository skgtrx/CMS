using System;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class AdminProfileView
    {
        /// <summary>
        /// Property: Id to be used for Accessing Admins Profile of particular Id.
        /// </summary>
        public int Id { get; set; }

        // Passing Id into object through class constructor
        public AdminProfileView(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Retreaves all the info of Admin's profile based on the Admin's User Id
        /// </summary>
        /// <returns>User Object with Profile Info</returns>
        public Users GetAdminProfile()
        {
            AdminProfileData AdminProfile = new AdminProfileData(Id);
            var Admin = AdminProfile.GetAdminUser();
            Admin.Password = new EncryptPassword().Decrypt(Admin.Password);
            return Admin;
        }
    }
}
