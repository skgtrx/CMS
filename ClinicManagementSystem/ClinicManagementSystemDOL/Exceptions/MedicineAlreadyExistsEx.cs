using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Exceptions
{
    public class MedicineAlreadyExistsEx : Exception
    {
        public MedicineAlreadyExistsEx(string message) : base(message)
        {
        }
    }
}
