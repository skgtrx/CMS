using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace ClinicManagementSystemDAL
{
    public class PatientEditRemove
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Method that removes a patient from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemovePatient(int id)
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
            catch(Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return false;
            }
        }

        public bool EditPatient(Users user, Patients patient)
        {
            using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    UserInformation userInformation = new UserInformation();
                    if (db.Users.SingleOrDefault(u => u.Id == user.Id) != null && db.Patients.SingleOrDefault(p => p.UserId == user.Id) != null)
                    {
                        Users userUpdate = db.Users.SingleOrDefault(u => u.Id == user.Id);
                        Patients patientUpdate = db.Patients.SingleOrDefault(p => p.UserId == user.Id);
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
                        patientUpdate.ConservatorName = patient.ConservatorName;
                        patientUpdate.ConservatorRelation = patient.ConservatorRelation;
                        patientUpdate.ConservatorPhone = patient.ConservatorPhone;
                        db.SaveChanges();
                        dbTransaction.Commit();

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                catch (EmailAlreadyExistsEx ex)
                {
                    dbTransaction.Rollback();
                    throw ex;
                }
                catch (PhoneAlreadyExistsEx ex)
                {
                    dbTransaction.Rollback();
                    throw ex;
                }
                catch (Exception e)
                {
                    var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                    ravenClient.Capture(new SentryEvent(e));
                    dbTransaction.Rollback();
                    throw e;
                }
            }
        }
    }
}
