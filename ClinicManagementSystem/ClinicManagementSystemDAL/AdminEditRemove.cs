
using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDAL
{
    public class AdminEditRemove
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This is the method that can be used to soft delete an admin from the system.
        /// </summary>
        /// <param name="id"is the Unique ID number given to every admin on the system ></param>
        /// <returns>Returns true if the admin profile was deleted else returns false</returns>
        public bool RemoveAdmin(int id)
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
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// This method is used to edit the admin profile
        /// </summary>
        /// <param name="user" is the user model that stores all the information of a particular user></param>
        /// <returns>True if the user profile is edited else returns false</returns>
        public bool EditAdmin(Users user)
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
                    userUpdate.Phone = user.Phone;
                    userUpdate.ModifiedAt = DateTime.Now;
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
                    db.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (EmailAlreadyExistsEx e)
            {
                throw e;
            }
            catch (PhoneAlreadyExistsEx e)
            {
                throw e;
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
