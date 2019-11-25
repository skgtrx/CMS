using ClinicManagementSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class AdminRemove
    {
        /// <summary>
        /// Deletes an admin user just by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Flag as true if deleted successfully else false</returns>
        public bool DeleteAdmin(int id)
        {
            AdminEditRemove adminRemove = new AdminEditRemove();
            if (adminRemove.RemoveAdmin(id))
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
