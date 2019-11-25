using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using System;

namespace ClinicManagementSystemBL
{
    public class DoctorEdit
    {
        /// <summary>
        /// Instance of EncryptPassword for encrypting passwords
        /// </summary>
        EncryptPassword encrypt = new EncryptPassword();
        
        /// <summary>
        /// Updates the Doctor Info based on Normal user info and Fee Info
        /// </summary>
        /// <param name="user"></param>
        /// <param name="doctorFee"></param>
        /// <returns>Flag as true if updated successfully else false</returns>
        public bool UpdateDoctor(Users user, DoctorFee doctorFee)
        {
            try
            {
                user.Password = encrypt.Encrypt(user.Password);
                DoctorEditRemove DoctorEdit = new DoctorEditRemove();
                return DoctorEdit.EditDoctor(user, doctorFee);
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
                throw e;
            }

        }

        /// <summary>
        /// Updates the Doctor Info based on Normal user info
        /// </summary>
        /// <param name="user"></param>
        /// <param name="doctorFee"></param>
        /// <returns>Flag as true if updated successfully else false</returns>
        public bool UpdateDoctor(Users user)
        {
            try
            {
                user.Password = encrypt.Encrypt(user.Password);
                DoctorEditRemove DoctorEdit = new DoctorEditRemove();
                return DoctorEdit.EditDoctor(user);
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
                throw e;
            }
        }
    }
}
