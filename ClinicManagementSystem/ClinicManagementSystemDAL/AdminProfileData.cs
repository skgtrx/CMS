using System;
using ClinicManagementSystemDOL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemDOL.Enums;
using SharpRaven.Data;
using SharpRaven;

namespace ClinicManagementSystemDAL
{
    public class AdminProfileData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public int Id { get; set; }

        public AdminProfileData(int id)
        {
            Id = id;
        }
        /// <summary>
        /// This is the method that can be used to get the admin details based on the ID passed to the constructor
        /// </summary>
        /// <returns>The user model containing the user details</returns>
        public Users GetAdminUser()
        {
            try
            {
                var Admin = db.Users.FirstOrDefault(a => a.Id == Id && a.RoleId == (int)Roles.Admin);
                return Admin;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
        
    }
}
