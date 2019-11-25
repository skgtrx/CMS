using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Exceptions
{
    public class TestsAlreadyExistsEx : Exception
    {
        public TestsAlreadyExistsEx(string message) : base(message)
        {
        }
    }
}
