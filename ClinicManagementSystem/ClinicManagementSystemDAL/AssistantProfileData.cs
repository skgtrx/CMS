using System;
using ClinicManagementSystemDOL.Models;
using System.Linq;
using SharpRaven;
using SharpRaven.Data;

namespace ClinicManagementSystemDAL
{
    public class AssistantProfileData
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public int Id { get; set; }

        public AssistantProfileData(int id)
        {
            Id = id;
        }
        /// <summary>
        /// Method is used to retrieve assistant data
        /// </summary>
        /// <returns></returns>
        public Users GetAssistantUser()
        {
            try
            {
                var Assistant = db.Users.FirstOrDefault(a => a.Id == Id && a.RoleId == 3 && a.IsDeleted == false);
                return Assistant;
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
