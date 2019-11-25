using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Exceptions
{
    public class UserCanNotBeAddedEx:Exception
    {
        public UserCanNotBeAddedEx(string message) : base(message)
        {

        }
    }
}
