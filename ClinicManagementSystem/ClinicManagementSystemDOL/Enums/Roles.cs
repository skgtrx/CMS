using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Enums
{
    public enum Roles
    {
        [Description("Admin")]
        Admin = 1,

        [Description("Doctor")]
        Doctor = 2,

        [Description("Assistant")]
        Assistant = 3,

        [Description("Patient")]
        Patient = 4

    }
}
