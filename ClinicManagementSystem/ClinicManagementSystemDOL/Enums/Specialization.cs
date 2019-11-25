using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Enums
{
    public enum Specialization
    {
        [Description("Cardiologists")]
        Cardiologists = 1,

        [Description("Dermatologists")]
        Dermatologists = 2,

        [Description("General Practitioner")]
        GeneralPractitioner = 3,

        [Description("Neurologist")]
        Neurologist = 4,

        [Description("Psychiatrist")]
        Psychiatrist = 5,

        [Description("Surgeon")]
        Surgeon = 6,

        [Description("Physician")]
        Physician = 7,

        [Description("Gynecologist")]
        Gynecologist = 8,

        [Description("Immunologist")]
        Immunologist = 9,

        [Description("Oncologist")]
        Oncologist = 10

    }
}
