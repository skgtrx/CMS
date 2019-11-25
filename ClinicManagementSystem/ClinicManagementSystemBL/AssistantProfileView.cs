using System;
using System.Collections.Generic;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class AssistantProfileView
    {
        /// <summary>
        /// Property: Id to be used for Accessing Assistants Profile of particular Id.
        /// </summary>
        public int Id { get; set; }

        // Passing Id into object through class constructor
        public AssistantProfileView(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Retreaves all the info of Assistant's profile based on the Assistant's User Id
        /// </summary>
        /// <returns>User Object with Profile Info</returns>
        public Users GetAssistantProfile()
        {
            AssistantProfileData AssistantProfile = new AssistantProfileData(Id);
            var Assistant = AssistantProfile.GetAssistantUser();
            Assistant.Password = new EncryptPassword().Decrypt(Assistant.Password);
            return Assistant;
        }
    }
}
