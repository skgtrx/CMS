using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class DoctorEditRemove
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Removes the doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveDoctor(int id)
        {
           try
           {
               Users users = db.Users.SingleOrDefault(u => u.Id == id);
               if (users != null)
               {
                    users.IsDeleted = true;
                    users.DeletedAt = DateTime.Now;
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

        public bool EditDoctor(Users user, DoctorFee doctorFee)
        {
            try
            {
                UserInformation userInformation = new UserInformation();
                if (db.Users.SingleOrDefault(u => u.Id == user.Id) != null)
                {
                    Users userUpdate = db.Users.FirstOrDefault(u => u.Id == user.Id);
                    DoctorFee feeUpdate = db.DoctorFee.FirstOrDefault(u => u.DoctorId == user.Id);
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
                    feeUpdate.Fee = doctorFee.Fee;
                    db.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (EmailAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                throw e;
            }
        }

        public bool EditDoctor(Users user)
        {
            try
            {
                UserInformation userInformation = new UserInformation();
                if (db.Users.SingleOrDefault(u => u.Id == user.Id) != null)
                {
                    Users userUpdate = db.Users.FirstOrDefault(u => u.Id == user.Id);
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
            catch (EmailAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                throw ex;
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
