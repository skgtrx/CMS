using ClinicManagementSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class AssistantRemove
    {
        /// <summary>
        /// Deletes Assistant based on its UserId from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Flag as true if deleted successfully else false</returns>
        public bool DeleteAssistant(int id)
        {
            AssistantEditRemove assistantRemove = new AssistantEditRemove();
            if (assistantRemove.RemoveAssistant(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
