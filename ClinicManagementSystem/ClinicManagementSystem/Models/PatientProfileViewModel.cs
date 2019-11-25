using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicManagementSystemDOL.Models;
namespace ClinicManagementSystem.Models
{
    public class PatientProfileViewModel
    {
        public Patients PatientInfo { get; set; }
        public Users Patient { get; set; }
    }
}