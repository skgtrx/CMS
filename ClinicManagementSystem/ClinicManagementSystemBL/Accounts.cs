using ClinicManagementSystemDAL;
using System;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Exceptions;

namespace ClinicManagementSystemBL
{
    public class Accounts
    {
        /// <summary>
        /// Instance of EncryptPassword for encrypting password received from the User.
        /// Instance of UserInformation for accessing info of user based on identity.
        /// </summary>
        EncryptPassword encrypt = new EncryptPassword();
        UserInformation userInformation = new UserInformation();

        /// <summary>
        /// Validates the user login by matching correct email and password from Db.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>flag as true for valid credentials and false for invalid credentials</returns>
        public bool ValidateUserLogin(string email, string password)
        {
            string userPassword= userInformation.GetPassword(email);
            if (userPassword==null)
            {
                return false;
            }
            else
            {
                if (password.Equals(encrypt.Decrypt(userPassword)))
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Retreave info from Database required for creating session
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User Object with info UserId, RoleId and FirstName</returns>
        public Users SessionUserInfo(Users user)
        {
            return userInformation.RetreiveUserInformation(user);//Check for null or Exception
        }

        /// <summary>
        /// Takes input as User data of all types - Admin, Assistant, Doctor and Patient.
        /// Send the info from User to Database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userObjects"></param>
        /// <returns>Flag as true if Added Successfully else false</returns>
        public bool AddUser(Users user, params object[] userObjects)
        {
            try
            {
                user.Password = encrypt.Encrypt(user.Password);
                return userInformation.AddUser(user, userObjects);
            }
            catch (EmailAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Updates Password based on UserId and New Password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwd"></param>
        /// <returns>Flag as true if Updated Successfully else false</returns>
        public bool UpdatePassword(int id, string passwd)
        {
            passwd = encrypt.Encrypt(passwd);
            UserInformation user = new UserInformation();
            if (user.UpdateUserPassword(id, passwd))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check for the existance of Email in Database for a live user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Flag as true if exists else false</returns>
        public bool EmailExist(string email)
        {
            Users user = new Users();
            user.Email = email;
            return new UserInformation().CheckIfEmailExists(user);
        }

        /// <summary>
        /// Check for the validity of reset link sent to the user.
        /// </summary>
        /// <param name="link"></param>
        /// <returns>Flag as true if Link still valid else false</returns>
        public bool ResetLinkValid(string link)
        {
            try
            {
                UserInformation user = new UserInformation();
                if (user.ResetPasswdValidity(link))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retreaves the UserId from the reset link after link is valid for further process
        /// </summary>
        /// <param name="link"></param>
        /// <returns>UserId for further use</returns>
        public int GetIdFrmLink(string link)
        {
            try
            {
                UserInformation user = new UserInformation();
                return user.IdFrmResetLink(link);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Update a record in Database for the Reset password Link sent to the user. 
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Flag as true if Added Successfully else false</returns>
        public bool AddResetPasswordLinkRecord(ResetPasswd info)
        {
            try
            {
                UserInformation user = new UserInformation();
                if (user.ResetPasswdLink(info))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
