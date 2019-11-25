using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// This is the index method for all the user roles which is using for dashboard display
        /// </summary>
        /// <remarks>
        /// Takes the userid from the session and displaying the dashborad according to the need of that user
        /// </remarks>
        /// <returns>Display dashboard according to users who is logged in</returns>
        [Route("~/Home")]
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Dashboard dashboard = new Dashboard();
            if((int)Session["roleId"] == (int)Roles.Admin || (int)Session["roleId"] == (int)Roles.Assistant )
            {
                ViewBag.Patients = dashboard.GetCountOfPatient();
                ViewBag.Appointments = dashboard.GetCountOfAppointment();
                ViewBag.Doctors = dashboard.GetCountOfDoctor();
                ViewBag.Users = dashboard.GetCountOfUser();
                ViewBag.TodaysAppointments = dashboard.GetCountOfTodaysAppointment();
            }
            else if((int)Session["roleId"] == (int)Roles.Patient)
            {
                ViewBag.UpcomingPatientAppointment = dashboard.GetUpcomingPatientAppointments((int)Session["userId"]);
                ViewBag.TotalPatientAppointment = dashboard.GetTotalPatientAppointments((int)Session["userId"]);
                ViewBag.PendingPatientAppointment = dashboard.GetPendingPatientAppointments((int)Session["userId"]);
            }
            else if((int)Session["roleId"] == (int)Roles.Doctor)
            {
                ViewBag.DoctorAppointments = dashboard.GetDoctorAppointments((int)Session["userId"]);
                ViewBag.DoctorTodaysAppointments = dashboard.GetDoctorTodaysAppointments((int)Session["userId"]);
                ViewBag.DoctorsPatient = dashboard.GetDoctorsPatient((int)Session["userId"]);
            }
            
            return View();
        }
    }
}
