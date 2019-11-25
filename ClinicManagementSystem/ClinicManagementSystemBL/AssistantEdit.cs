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
    public class AssistantEdit
    {
        /// <summary>
        /// Instance of EncryptPassword for Encrypting Password
        /// </summary>
        EncryptPassword encrypt = new EncryptPassword();

        /// <summary>
        /// Updates the Assistant info and stores it into database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Flag as true if updated successfully else false</returns>
        public bool UpdateAssistant(Users user)
        {
            try
            {
                user.Password = encrypt.Encrypt(user.Password);
                AssistantEditRemove assistantEdit = new AssistantEditRemove();
                return assistantEdit.EditAssistant(user);
                
            }
            catch (EmailAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch(Exception e)
            {
                throw e;
            }
  
        }
    }
}
