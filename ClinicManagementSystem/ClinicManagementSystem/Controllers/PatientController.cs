using ClinicManagementSystem.Models;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [RoutePrefix("Patient")]
    public class PatientController : Controller
    {   
        /// <summary>
        /// Returns the Patient profile view.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the Patient profile view.
        /// </remarks>
        /// <returns>The patient profile details</returns>
        [Route("Profile")]
        public ActionResult PatientProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var PatientViewObj = new PatientProfileView((int)Session["userId"]);
            var viewModel = new PatientProfileViewModel
            {
                Patient = PatientViewObj.GetPatientProfile(),
                PatientInfo = PatientViewObj.GetPatientInfo()
            };
            return View(viewModel);
        }
        /// <summary>
        /// This action returns the list of appointments that were booked by the patient.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the appointments that were booked by a particular patient.
        /// </remarks>
        /// <returns>The list of appointments booked by a patient</returns>
        [Route("Appointments")]
        public ActionResult AppointmentList()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentData appointmentData = new AppointmentData();
            List<Appointment> AppointmentList = appointmentData.GetPatientAppointmentList((int)Session["userId"]);
            return View(AppointmentList);
        }
        /// <summary>
        /// Action method that opens the patient profile edit form with the patient details.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the view of patient profile edit form that has prefilled details.
        /// </remarks>
        /// <returns>the present details of the patient</returns>
        [HttpGet]
        [Route("Profile/Edit")]
        public ActionResult EditProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var PatientViewObj = new PatientProfileView((int)Session["userId"]);
            var Patient = PatientViewObj.GetPatientProfile();
            var PatientInfo = PatientViewObj.GetPatientInfo();
            PatientProfileEditViewModel model = new PatientProfileEditViewModel();
            AutoMapper.Mapper.Map(Patient, model);
            AutoMapper.Mapper.Map(PatientInfo, model);
            return View(model);
        }
        /// <summary>
        /// This is the post method that saves the details of the patient onto the database.
        /// </summary>
        /// <remarks>
        /// This is the controller action that posts the edited details of a patient to the database.
        /// </remarks>
        /// <returns>If the profile is edited successfully, the paatient is redirected to the profile details page</returns>
        [HttpPost]
        [Route("Profile/Edit")]
        public ActionResult EditProfile(PatientProfileEditViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            Patients patient = new Patients();
            user.RoleId = (int)Roles.Patient;
            AutoMapper.Mapper.Map(model, user);
            AutoMapper.Mapper.Map(model, patient);
            user.Id = (int)Session["userId"];

            PatientEdit Patient = new PatientEdit();
            try
            {
                if (Patient.UpdatePatient(user, patient))
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    return RedirectToAction("Profile/Edit");
                }
            }
            catch (EmailAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// This is the method that brings up the details of all doctors that are available at any particular time.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the list of doctors with the option to book an appointment at that particular doctor.
        /// </remarks>
        /// <returns>the list of doctors available at any particular time</returns>
        [HttpGet]
        [Route("Doctors")]
        public ActionResult MakeAppointment()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentViewModel appointment = new AppointmentViewModel();
            appointment.PatientId = (int)Session["userId"];
            return View(appointment);
        }
        /// <summary>
        /// This is the get method that provides user with a form to book an appointment at any moment of time.
        /// </summary>
        /// <remarks>
        /// This is the controller action that renders an appointment booking form that users can use to book an appointment.
        /// </remarks>
        /// <returns>the blank form that patients can fill to book an appointment</returns>
        [HttpGet]
        [Route("Doctor/Book-Appointment/{id}")]
        public ActionResult BookAppointment(int id, DateTime date)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var bookAppointmentDetails = new PatientsBookAppointmentViewModel();
            bookAppointmentDetails.DoctorId = id;
            bookAppointmentDetails.Date = date;
            bookAppointmentDetails.PatientId = (int)Session["userId"];
            TempData["DoctorId"] = id;
            TempData["SelectedDateForAppointment"] = date;
            return View(bookAppointmentDetails);
        }
        /// <summary>
        /// This is the post method that submits the appointment details to the backend.
        /// </summary>
        /// <remarks>
        /// This is the controller action that submits the appointment details to be saved into the database.
        /// </remarks>
        /// <returns>If the appointment is booked successfully, the patient is redirected to the appointment list page</returns>
        [HttpPost]
        [Route("Doctor/Book-Appointment/{id}")]
        public ActionResult BookAppointment(PatientsBookAppointmentViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Appointment appointment = model.ToAppointment(model);

            if ((int)Session["roleID"] == (int)Roles.Patient)
            {
                appointment.PatientId = Convert.ToInt32(Session["userId"]);
                appointment.AppointmentStatusId = (byte)ClinicManagementSystemDOL.Enums.AppointmentStatus.Pending;
                appointment.CreatedAt = DateTime.Now;
            }
            try
            {
                new Appointments().AddAppointment(appointment);
                return RedirectToAction("AppointmentList", "Patient");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        /// <summary>
        /// Returns the Medical-History view.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the Medical-History UI.
        /// </remarks>
        /// <returns>the list of medical history for a patient</returns>
        [HttpGet]
        [Route("MedicalHistory")]
        public ActionResult PatientMedicalHistory()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var History = new MedicalHistory();
            List<MedicalHistoryViewModel> model = new List<MedicalHistoryViewModel>();
            List<Diagnosis> DiagnosisHistories = History.GetDiagnosisHistory((int)Session["userId"]);
            var MedicineNames = new List<List<MedicineRecord>>();

            foreach (var item in DiagnosisHistories)
            {
                var MedicalHistory = new MedicalHistoryViewModel();
                MedicalHistory.DiagnosisHistories = item;
                MedicalHistory.MedicineList = History.GetMedicineNames(item.Id);
                MedicalHistory.TestList = History.GetTestNames(item.Id);

                model.Add(MedicalHistory);
            }

            return View(model);
        }
        /// <summary>
        /// This is the get method that provides user with his invoice details of a particular appointment.
        /// </summary>
        /// <remarks>
        /// This is the controller action that renders the invoice generated for any particular appointment.
        /// </remarks>
        /// <returns>the invoice for any particular appointment</returns>
        [HttpGet]
        [Route("Invoice")]
        public ActionResult ShowInvoice(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            InvoiceViewModel model = new InvoiceViewModel(id);

            return View(model);
        }

        /// <summary>
        /// This action is showing the all available doctors to patient according to their availablitity for the particular date
        /// </summary>
        /// <remarks>
        /// This is  controller action which is showing list of all doctors with their specilization after choosing a particular date
        /// </remarks>
        /// <returns>All the doctor list to book appointment for patients</returns>
        public ActionResult DoctorsList(AppointmentViewModel model)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            DoctorsData doctorsData = new DoctorsData();
            ScheduleBasedAppointmentManagement scheduleBasedAppointment = new ScheduleBasedAppointmentManagement();
            var scheduleBasedAvailability = new List<DoctorAvailability>();
            scheduleBasedAvailability = scheduleBasedAppointment.GetAvailability(model.SelectedDate);
            var doctorList = new List<DoctorsListViewModel>();
            var doctorsAvailabilityList = doctorsData.GetDoctorsList();
            doctorsAvailabilityList = (
                    from o in doctorsAvailabilityList
                    orderby o.DoctorId, o.Fee descending
                    group o by o.DoctorId into g
                    select g.First()
                    ).ToList();
            var doctorSpecilizationList = doctorsData.GetDoctorSpecilizationList();
            foreach (var item in doctorsAvailabilityList)
            {
                var doctorData = new DoctorsListViewModel();
                doctorData.FirstName = item.Doctor.FirstName;
                doctorData.LastName = item.Doctor.LastName;
                doctorData.Email = item.Doctor.Email;
                doctorData.DoctorId = item.DoctorId;
                foreach (var scheduleAvailability in scheduleBasedAvailability)
                {
                    if (scheduleAvailability.Users.Id == item.DoctorId)
                    {
                        doctorData.Availability = true;
                        break;
                    }
                }
                foreach (var doctorSpecilization in doctorSpecilizationList)
                {
                    if (item.DoctorId == doctorSpecilization.UserId)
                    {
                        doctorData.Specialization.Add(doctorSpecilization.Specialization.Name.ToString());
                    }
                }
                doctorData.SelectedDate = model.SelectedDate;
                doctorList.Add(doctorData);
            }
            return View(doctorList);
        }
        /// <summary>
        /// Populate the available timeslots of a doctor for a particular date
        /// </summary>
        /// <remarks>
        /// This is controller action to show all time slots which are scheduled by a doctor at a particular date
        /// </remarks>
        /// <returns>All time slots available of a doctor for a date</returns>
        [HttpPost]
        public ActionResult PopulateTimeSlot(TimeSlotViewModel model)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            //model.Date = (DateTime)TempData["SelectedDateForAppointment"];
            ScheduleBasedAppointmentManagement appointmentManagement = new ScheduleBasedAppointmentManagement();
            var doctorAvailability = appointmentManagement.GetAvailability(model.DoctorId, model.Date);
            model.FromSlot = doctorAvailability.FromTime;
            model.ToSlot = doctorAvailability.ToTime;
            model.TimeSlots = new DoctorsData().GetDoctorAvailableTimeSlots(model.DoctorId, model.Date);
            model.RemoveExistingAppointmentSlots((int)Session["userId"], model.Date);
            return View(model);
        }
    }
}