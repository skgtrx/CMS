using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using System;

namespace ClinicManagementSystemBL
{
    public class AdminEdit
    {
        /// <summary>
        /// Instance of EncryptPassword for encrypting password received from the User.
        /// </summary>
        EncryptPassword encrypt = new EncryptPassword();

        /// <summary>
        /// Updates Admin Profile Info by wrapping it into User Object.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Flag as true if Updated Successfully else false</returns>
        public bool UpdateAdmin(Users user)
        {
            try
            {
                AdminEditRemove adminEdit = new AdminEditRemove();
                user.Password = encrypt.Encrypt(user.Password);
                return adminEdit.EditAdmin(user);

            }
            catch (EmailAlreadyExistsEx ex)
            {
                throw new EmailAlreadyExistsEx("Email already exists");
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                throw new PhoneAlreadyExistsEx("Phone number already exists");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
