using System;
using ClinicManagementSystemBL;
using ClinicManagementSystem.Models;
using System.Web.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        /// <summary>
        /// Returns the form view which provides option to show Report by Date
        /// </summary>
        /// <returns>The daily appointment list to view</returns>
        public ActionResult ByDate()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentData report = new AppointmentData();
            var daily = report.GetAppointmentList();
            return View(daily);
        }

        /// <summary>
        /// It calls the Partial View to show the list of appointments by provided date
        /// </summary>
        /// <param name="getDate">Contains the date by which Report is to be displayed</param>
        /// <returns>To the view which shows the list of appointments for the given date</returns>
        public ActionResult Daily(DateTime getDate)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Report report = new Report();
            var dailyAppointments = report.GetReportByDate(getDate);
            return View("_Appointments", dailyAppointments);
        }

        /// <summary>
        /// It shows the Form which where we can search appointments monthly, weekly and by status of appointment
        /// </summary>
        /// <returns>The view which contains the list of appointments as per desired search</returns>
        public ActionResult ByAppointment()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentData report = new AppointmentData();
            var appointments = report.GetAppointmentList();
            return View(appointments);
        }

        /// <summary>
        /// This shows the view which contains the list appointments
        /// </summary>
        /// <param name="viewModel">Contains the option by which we can search for report</param>
        /// <returns>To the Partial view for showing the appointments</returns>
        public ActionResult Appointment(AppointmentSearchViewModel viewModel)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Report report = new Report();
            var FilteredAppointments = report.GetReportByAppointment(viewModel.Option.ToString());
            return PartialView("_Appointments", FilteredAppointments);
        }

        /// <summary>
        /// Action that calls for the view which can show report by patient phone number
        /// </summary>
        /// <returns>The View which shows the list of appointments of the Patient whose phone number is passed</returns>
        public ActionResult ByPatient()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentData report = new AppointmentData();
            var daily = report.GetAppointmentList();
            return View(daily);
        }

        /// <summary>
        /// Action that calls for the view which takes phone number and pass it to partial view to show the Patient's Appointment List
        /// </summary>
        /// <param name="phoneNumber">Phone number of the Patient whose report to be searched</param>
        /// <returns>The Partial view which shows the report of the patient</returns>
        public ActionResult Patient(string phoneNumber)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Report report = new Report();
            var PatientAppointments = report.GetReportByPatient(phoneNumber);
            return View("_Appointments", PatientAppointments);
        }
    }
}