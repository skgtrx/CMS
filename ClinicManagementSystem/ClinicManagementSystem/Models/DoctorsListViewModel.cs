using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagementSystem.Models
{
    public class DoctorsListViewModel
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Availability { get; set; }
        public List<string> Specialization = new List<string>();
        public DateTime SelectedDate { get; set; }
    }
}