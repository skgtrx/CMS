using System;
using System.Collections.Generic;
using System.Linq;
using ClinicManagementSystemDOL.Models;
using System.Data.Entity;
using ClinicManagementSystemDOL.Exceptions;
using SharpRaven.Data;
using SharpRaven;

namespace ClinicManagementSystemDAL
{
    public class UserInformation
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// This is the method that can be used to retrieve the password from the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetPassword(string email)
        {
            string password = null;
            try
            {
                password = db.Users.SingleOrDefault(u => u.Email == email && u.IsDeleted == false).Password;
                return password;
            }
            catch (Exception e)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(e));
                return null;
            }
        }

        public bool ResetPasswdLink(ResetPasswd info)
        {
            try
            {
                db.ResetPassword.Add(info);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(ex));
                return false;
            }
        }

        public bool ResetPasswdValidity(string link)
        {
            try
            {
                var linkTimeStamp = db.ResetPassword.Where(l => l.Id == link).Select(l => l.ResetRequestDateTime).SingleOrDefault();
                if (DateTime.Now.Subtract(linkTimeStamp).TotalMinutes <= 10) // 10 Minutes
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(ex));
                return false;
            }
        }

        public int IdFrmResetLink(string link)
        {
            try
            {
                var userId = db.ResetPassword.Where(l => l.Id == link).Select(l => l.UserId).SingleOrDefault();
                return userId;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public bool CheckIfUserExists(Users user)
        {
            try
            {
                if (db.Users.SingleOrDefault(u => u.Email == user.Email) != null && db.Users.SingleOrDefault(u => u.Phone == user.Phone) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(ex));
                return false;
            }
    
        }
        public bool CheckIfEmailExists(Users user)
        {
            try
            {
                if (db.Users.SingleOrDefault(u => u.Email == user.Email) != null )
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                var ravenClient = new RavenClient("https://94cd599d4c8749b3b612b54f58d9180b@sentry.io/1820886");
                ravenClient.Capture(new SentryEvent(ex));
                return true;
            }

        }

        internal int GetUserIdFromPhone(string phone)
        {
            try
            {
                return db.Users.FirstOrDefault(u => u.Phone == phone).Id;
            }
            catch (Exception)
            {

                return 0;//check for exceptions
            }
        }

        public bool CheckIfPhoneExists(Users user)
        {
            try
            {
                if (db.Users.SingleOrDefault(u => u.Phone == user.Phone) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return true; ;
            }

        }

        public int GetUserId(string email)
        {
            try
            {
                return db.Users.FirstOrDefault(u => u.Email == email).Id;
            }
            catch (Exception)
            {

                return 0;//check for exceptions
            }
        }

        public bool AddDoctorAvailability(DoctorAvailability doctorAvailability)
        {
            try
            {
                db.DoctorAvailability.Add(doctorAvailability);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddDoctorSpecilization(DoctorSpecialization specilization)
        {
            try
            {
                db.DoctorSpecialization.Add(specilization);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddPatientDetails(Patients patient)
        {
            try
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Users RetreiveUserInformation(Users user)
        {
            try
            {
                Users userInfo = new Users();
                userInfo.RoleId = db.Users.SingleOrDefault(u => u.Email == user.Email).RoleId;
                userInfo.Id = db.Users.SingleOrDefault(u => u.Email == user.Email).Id;
                userInfo.FirstName = db.Users.SingleOrDefault(u => u.Email == user.Email).FirstName;
                return userInfo;

            }
            catch(Exception ex)
            {
                throw new UserNotFoundEx("User doesn't exist");
                //return null;//Check for null in controller
            }
        }
        public bool AddUser(Users user, params object[] userObjects)
        {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (CheckIfEmailExists(user))
                        {
                            throw new EmailAlreadyExistsEx("Email already exists");
                        }
                        else if (CheckIfPhoneExists(user))
                        {
                            throw new PhoneAlreadyExistsEx("Phone already exists");
                        }
                        else
                        {
                            user.CreatedAt = DateTime.Now;
                            
                            db.Users.Add(user);
                            int userId = GetUserId(user.Email);
                            switch (user.RoleId)
                            {
                                case 2:
                                    //DoctorAvailability doctorAvailability = (DoctorAvailability)userObjects[0];
                                    //doctorAvailability.UserId = userId;
                                    //db.DoctorAvailability.Add(doctorAvailability);
                                    List<DoctorSpecialization> specilizations = (List<DoctorSpecialization>)userObjects[1];
                                    
                                    foreach (var specilization in specilizations)
                                    {
                                        specilization.UserId = userId;
                                        db.DoctorSpecialization.Add(specilization);
                                    }
                                    DoctorFee doctorFee = (DoctorFee)userObjects[2];
                                    doctorFee.DoctorId = userId;
                                    db.DoctorFee.Add(doctorFee);
                                    break;
                                case 4:
                                    Patients patient = (Patients)userObjects[0];
                                    patient.UserId = userId;
                                    db.Patients.Add(patient);
                                    break;
                            }
                            db.SaveChanges();
                            dbTransaction.Commit();
                            return true;
                        }
                    }
                    catch (EmailAlreadyExistsEx ex)
                    {
                        throw ex;
                    }
                    catch(PhoneAlreadyExistsEx ex)
                    {
                        throw ex;
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        return false;
                    }
                }
        }
        public int PhoneNumberExists(string phone)
        {
            try
            {
                return db.Users.FirstOrDefault(u => u.Phone == phone).Id;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool UpdateUserPassword(int id, string passwd)
        {
            try
            {
                Users users = db.Users.SingleOrDefault(u => u.Id == id);
                if (users != null)
                {
                    users.Password = passwd;
                    db.Users.Attach(users);
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
