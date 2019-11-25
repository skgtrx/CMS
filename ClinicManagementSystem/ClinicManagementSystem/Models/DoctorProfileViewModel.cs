using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class DoctorProfileViewModel
    {
        public DoctorFee DoctorFee { get; set; }
        public Users Doctor { get; set; }
    }
}