using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class AssistantEditRemove
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This method can be used to remove an assistant from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveAssistant(int id)
        {
            try
            {
                Users users = db.Users.SingleOrDefault(u => u.Id == id);
                if (users != null)
                {
                    users.DeletedAt = DateTime.Now;
                    users.IsDeleted = true;
                    db.Users.Attach(users);
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return false; 
            }
            
        }
        /// <summary>
        /// This method can be used to edit the details of any assistant
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool EditAssistant(Users user)
        {
            UserInformation userInformation = new UserInformation();
            try
            {
                if (db.Users.SingleOrDefault(u => u.Id == user.Id) != null)
                {
                    Users userUpdate = db.Users.SingleOrDefault(u => u.Id == user.Id);
                    userUpdate.FirstName = user.FirstName;
                    userUpdate.LastName = user.LastName;
                    userUpdate.DateOfBirth = user.DateOfBirth;
                    userUpdate.Address = user.Address;
                    if (user.Id == userInformation.GetUserIdFromPhone(user.Phone) || !(userInformation.CheckIfPhoneExists(user)))
                    {
                        userUpdate.Phone = user.Phone;
                    }
                    else
                    {
                        throw new PhoneAlreadyExistsEx("Phone number already exists");
                    }

                    if (user.Id == userInformation.GetUserId(user.Email) || !(userInformation.CheckIfEmailExists(user)))
                    {
                        userUpdate.Email = user.Email;
                    }
                    else
                    {
                        throw new EmailAlreadyExistsEx("Email already exists");
                    }
                    userUpdate.Password = user.Password;
                    userUpdate.ModifiedAt = DateTime.Now;
                    db.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }
    }
}
