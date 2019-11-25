using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemBL
{
    public class PatientEdit
    {
        /// <summary>
        /// Instance of EncryptPassword for encrypting passwords
        /// </summary>
        EncryptPassword encrypt = new EncryptPassword();

        /// <summary>
        /// Updates Patient's info based on input as User's info and Patient's info
        /// </summary>
        /// <param name="user"></param>
        /// <param name="patient"></param>
        /// <returns>Falg as true if updated successfully else false</returns>
        public bool UpdatePatient(Users user, Patients patient)
        {
            try
            {
                user.Password = encrypt.Encrypt(user.Password);
                PatientEditRemove patientEdit = new PatientEditRemove();
                return patientEdit.EditPatient(user,patient);

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
