using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using System;
using System.Collections.Generic;

namespace ClinicManagementSystemBL
{
    public class AdminData
    {
        /// <summary>
        /// Instance of UserData for Accessing methods defined in DAL 
        /// </summary>
        UserData user = new UserData();

        /// <summary>
        /// Retreaves the list of all the admins based on role id
        /// </summary>
        /// <returns>List of Admins</returns>
        public List<Users> GetAdminsList()
        {
            return user.GetUsersList(Convert.ToInt32(Roles.Admin));
        }
    }
}
