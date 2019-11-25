using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class AssistantData
    {
        /// <summary>
        /// Instance of UserData for accessing the methods defined in DAL
        /// </summary>
        UserData user = new UserData();

        /// <summary>
        /// Retreaves the list assistants from the database
        /// </summary>
        /// <returns>List of Assistants</returns>
        public List<Users> GetAssistantsList()
        {
            return user.GetUsersList(Convert.ToInt32(Roles.Assistant));
        }
    }
}
