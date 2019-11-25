using System;
using System.Linq;
using System.Web.Security;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;

namespace ClinicManagementSystem.Controllers
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string email)
        {
            Accounts account = new Accounts();
            Users user = new Users();
            user.Email = email;
            var sessionInfo = account.SessionUserInfo(user);
            var roleId = sessionInfo.RoleId;
            if (roleId == 1)
            {
                return new string[] { "Admin" };
            }
            else if (roleId == 2)
            {
                return new string[] { "Doctor"};
            }
            else if (roleId == 3)
            {
                return new string[] { "Assistant"};
            }
            else
            {
                return new string[] { "Patient"};
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}