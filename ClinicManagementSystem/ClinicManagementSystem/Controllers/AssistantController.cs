using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicManagementSystem.Models;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Exceptions;
using System.Web.Routing;

namespace ClinicManagementSystem.Controllers
{
    [RoutePrefix("Assistant")]
    public class AssistantController : Controller
    {
        /// <summary>
        /// This action is used to get the record of all appointments from the database.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the appointment list of all patients.
        /// </remarks>
        /// <returns>The list of appointments</returns>
        // GET: Account/Login
        [Route("Appointments")]
        public ActionResult AppointmentList()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentData appointmentData = new AppointmentData();
            List<Appointment> AppointmentList = appointmentData.GetAppointmentList();
            return View(AppointmentList);
        }

        /// <summary>
        /// Returns the assistant profile.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the profile of the assistant who is logged in.
        /// </remarks>
        /// <returns>The profile of the assistant who has logged in</returns>
        // GET: Account/Login
        [Route("Profile")]
        public ActionResult AssistantProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var AssistantViewObj = new AssistantProfileView((int)Session["userId"]);
            var viewModel = new AssistantProfileViewModel
            {
                Assistant = AssistantViewObj.GetAssistantProfile()
            };
            return View(viewModel);
        }

        /// <summary>
        /// This action retrieves the assistant profile data and renders it on the UI to be edited.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the assistant profile for it to be edited.
        /// </remarks>
        /// <returns>Renders the view model to the UI</returns>
        // GET: Account/Login
        [HttpGet]
        [Route("Profile/Edit")]
        public ActionResult EditProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var AssistantViewObj = new AssistantProfileView((int)Session["userId"]);
            var Assistant = AssistantViewObj.GetAssistantProfile();
            var model = new AssistantProfileEditViewModel();
            AutoMapper.Mapper.Map(Assistant, model);
            return View(model);
        }

        /// <summary>
        /// This is the post action that will submit the edited assistant details.
        /// </summary>
        /// <remarks>
        /// This is the controller action that adds the edited changes to the database.
        /// </remarks>
        /// <returns>Once the changes are saved onto the database, the user is redirected to the profile view</returns>
        // GET: Account/Login
        [HttpPost]
        [Route("Profile/Edit")]
        public ActionResult EditProfile(AssistantProfileEditViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();// model.ToUsers();
            AutoMapper.Mapper.Map(model, user);
            user.Id = (int)Session["userId"];
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                user.Password = model.Password;
            }
            else
            {
                user.Password = model.NewPassword;
            }
            try
            {
                if (new AssistantEdit().UpdateAssistant(user))
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
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }



        }

        /// <summary>
        /// This action is used to get the record of all patients from the database.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the patients list.
        /// </remarks>
        /// <returns>The list of patients</returns>
        [Route("Patients")]
        public ActionResult PatientList()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            PatientData patientData = new PatientData();
            List<Patients> PatientList = patientData.GetPatientDetails();
            return View(PatientList);
        }
        /// <summary>
        /// This action is used to fetch particular patient detail from his/her id.
        /// </summary>
        /// <remarks>
        /// This is the controller action that storing the id of patient to pass it to another action.
        /// </remarks>
        /// <returns>Redirects to action patient profile </returns>
        public ActionResult PatientUserDetails(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            TempData["Id"] = id;
            return RedirectToAction("Patients/Profile");
        }

        /// <summary>
        /// This action is used to fetch particular patient detail from his/her id.
        /// </summary>
        /// <remarks>
        /// This is the controller action that redirects to a particular profile of patient by using the id.
        /// </remarks>
        /// <returns>Returns the view of patient detail accordin to id</returns>
        [Route("Patients/Profile")]
        public ActionResult PatientUserProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            if (TempData["Id"] == null)
            {
                return RedirectToAction("Patients");
            }
            else
            {
                var PatientViewObj = new PatientProfileView((int)TempData["Id"]);
                var viewModel = new PatientProfileViewModel
                {
                    Patient = PatientViewObj.GetPatientProfile(),
                    PatientInfo = PatientViewObj.GetPatientInfo()
                };
                return View(viewModel);
            }
        }
        /// <summary>
        /// This Action calls for the form for Adding Patient
        /// </summary>
        /// <returns>Form View for Adding Patient</returns>

        [HttpGet]
        [Route("Patients/Add")]
        public ActionResult AddPatient()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var model = new RegisterViewModel
            {
                Password = Default.Password.GetDescription()
            };
            return View(model);
        }
        /// <summary>
        /// This Action Calls BL for adding patient into Database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Redirects to the Patients List</returns>

        [HttpPost]
        [Route("Patients/Add")]
        public ActionResult AddPatient(RegisterViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            Patients patient = new Patients();
            AutoMapper.Mapper.Map(model, user);
            AutoMapper.Mapper.Map(model, patient);
            user.CreatedBy = (int)Session["userId"];
            user.RoleId = (int)Roles.Patient;
            try
            {
                if (new Accounts().AddUser(user, patient))
                {
                    return Redirect("~/Assistant/Patients");
                }
                else
                {
                    return RedirectToAction("~/Assistant/Patients/Add");
                }
            }
            catch (EmailAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        /// <summary>
        /// Get action to make appointment by assistant
        /// </summary>
        /// <returns>Returns the view to make appointment</returns>
        [HttpGet]
        public ActionResult MakeAppointment()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentViewModel appointment = new AppointmentViewModel();
            return View(appointment);
        }
        /// <summary>
        /// Post action to display the three dates from today
        /// </summary>
        /// <returns>Dispays the three dates from today onwards</returns>
        [HttpPost]
        public ActionResult FetchDates()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
            appointmentViewModel.DatesList.Add(DateTime.Now);
            appointmentViewModel.DatesList.Add(DateTime.Now.AddDays(1));
            appointmentViewModel.DatesList.Add(DateTime.Now.AddDays(2));
            return View(appointmentViewModel);
        }
        /// <summary>
        /// This action is showing the all available doctors to patient according to their availablitity for the particular date
        /// </summary>
        /// <remarks>
        /// This is  controller action which is showing list of all doctors with their specilization after choosing a particular date
        /// </remarks>
        /// <returns>All the doctor list to book appointment for assistant</returns>
        [HttpPost]
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
        /// This is the get method that provides user with a form to book an appointment at any moment of time.
        /// </summary>
        /// <remarks>
        /// This is the controller action that renders an appointment booking form that users can use to book an appointment.
        /// </remarks>
        /// <returns>the blank form that assistant can fill to book an appointment</returns>
        [HttpGet]
        public ActionResult BookAppointment(int id, DateTime date)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            TempData["SelectedDateForAppointment"] = date;
            var bookAppointmentDetails = new AssistantsBookAppointmentViewModel();
            bookAppointmentDetails.Date = date;
            if (id == 0)
            {
                bookAppointmentDetails = (AssistantsBookAppointmentViewModel)TempData["AppointmentData"];
            }
            else
            {
                bookAppointmentDetails.DoctorId = id;
                TempData["DoctorId"] = id;
            }
            return View(bookAppointmentDetails);
        }
        /// <summary>
        /// This is the post method that submits the appointment details to the backend.
        /// </summary>
        /// <remarks>
        /// This is the controller action that submits the appointment details to be saved into the database.
        /// </remarks>
        /// <returns>If the appointment is booked successfully, the assistant is redirected to the appointment list page</returns>
        [HttpPost]
        public ActionResult BookAppointment(AssistantsBookAppointmentViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            if (model.SelectedTimeSlot > 0 && !string.IsNullOrEmpty(model.PatientPhoneNumber) && !string.IsNullOrEmpty(model.Details))
            {
                Appointment appointment = model.ToAppointment(model);
                appointment.AppointmentTimeId = (byte)model.SelectedTimeSlot;
                var patientData = new PatientData();
                int patientId = patientData.GetPatientIdIfExists(model.PatientPhoneNumber); //returns 0 if patient doesn't exist

                if ((int)Session["roleID"] == (int)Roles.Assistant)
                {

                    if (patientId == 0) //returns 0 if patient doesn't exist
                    {
                        TempData["RegisterModel"] = model;
                        return RedirectToAction("RegisterPatient", "Assistant");
                    }
                    else
                    {
                        appointment.PatientId = patientId;
                        appointment.AppointmentStatusId = (byte)ClinicManagementSystemDOL.Enums.AppointmentStatus.Open;
                        appointment.CreatedAt=DateTime.Now;
                        try
                        {
                            if (new Appointments().AddAppointment(appointment))
                                return RedirectToAction("AppointmentList", "Assistant");
                            else
                                return View(model);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex.Message);
                            return View(model);
                        }
                    }
                }
            }
            return View(model);
        }
        /// <summary>
        /// Get action to register a patient when assistant enter a number which is not registered in database
        /// </summary>
        /// <returns>Returns view to register a patient when patient is not registered</returns>
        [HttpGet]
        public ActionResult RegisterPatient()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AssistantsBookAppointmentViewModel registerModel = (AssistantsBookAppointmentViewModel)TempData["RegisterModel"];
            return View(registerModel);
        }
        /// <summary>
        /// Get action to register a patient when assistant enter a number which is not registered in database while booking appointment.
        /// </summary>
        /// <remarks>
        /// This controller action register a patient while booking appointment for them.
        /// </remarks>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Register the patient in the database</returns>
        [HttpPost]
        public ActionResult RegisterPatient(AssistantsBookAppointmentViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            Patients patient = new Patients();
            AutoMapper.Mapper.Map(model, user);
            AutoMapper.Mapper.Map(model, patient);
            user.RoleId = (int)Roles.Patient;
            user.CreatedBy = (int)Session["userId"];
            try
            {
                if (new Accounts().AddUser(user, patient))
                {
                    TempData["AppointmentData"] = model;
                    return RedirectToAction("BookAppointment", "Assistant", new { id = 0, date = model.Date });
                }
                else
                {
                    return RedirectToAction("~/Assistant/Patients/Add");
                }
            }
            catch (EmailAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
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
            DoctorScheduleManagement doctorScheduleManagement = new DoctorScheduleManagement();
            model.TimeSlots = new DoctorsData().GetDoctorAvailableTimeSlots(model.DoctorId, model.Date);
            var patientData = new PatientData();
            int patientId = patientData.GetPatientIdIfExists(model.PatientPhone);
            model.RemoveExistingAppointmentSlots(patientId, model.Date);
            return View(model);
        }

        /// <summary>
        /// This action approve the appointment which is made by the patient.
        /// </summary>
        /// <remarks>
        /// This controller action approves the appointment of patient and make the status to open from pending.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Apporve the appointment of a patient</returns>
        public ActionResult ApproveAppointmentStatus(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentEdit Approve = new AppointmentEdit();
            Approve.EditAppointment(id, (int)ClinicManagementSystemDOL.Enums.AppointmentStatus.Open);
            return RedirectToAction("Appointments");
        }
        /// <summary>
        ///  This action rejects the appointment which is made by the patient.
        /// </summary>
        /// <remarks>
        /// This controller action rejects the appointment of patient and make the status to rejected from pending.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Rejects the appointment of a patient</returns>

        public ActionResult RejectAppointmentStatus(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentEdit Approve = new AppointmentEdit();
            Approve.EditAppointment(id, (int)ClinicManagementSystemDOL.Enums.AppointmentStatus.Rejected);
            return RedirectToAction("Appointments");
        }
        /// <summary>
        /// Get action gives the list of all closed appoitments to generate invoice.
        /// </summary>
        /// <remarks>
        /// This controller action give the list of all the closed appointments to generate the invoice of that appointment.
        /// </remarks>
        /// <returns>List of all closed appointments</returns>
        [HttpGet]
        [Route("Closed-Appointments")]
        public ActionResult ManageInvoice()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentData appointments = new AppointmentData();
            List<Appointment> AppointmentList = appointments.GetClosedAppointmentList();
            return View(AppointmentList);
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
        /// Get action to add more Medicines into database
        /// </summary>
        /// <returns>returns the view to add Medicines</returns>
        [HttpGet]
        [Route("Medicines")]
        public ActionResult AddMedicines()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            return View();
        }

        /// <summary>
        /// Post method to add more Medicines into database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Add the Medicines into database if succesfull else give error message </returns>
        [HttpPost]
        [Route("Medicines")]
        public ActionResult AddMedicines(AmenitiesViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Medicines medcines = model.ToMedicines();

            try
            {
                new Amenities().AddMedicines(medcines);
                return RedirectToAction("Medicines", "Assistant");
            }
            catch (MedicineAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        /// <summary>
        /// Get action to add more test into database
        /// </summary>
        /// <returns>returns the view to add tests</returns>

        [HttpGet]
        [Route("Tests")]
        public ActionResult AddTests()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            return View();
        }
        /// <summary>
        /// Post method to add more test into database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Add the test into database if succesfull else give error message </returns>

        [HttpPost]
        [Route("Tests")]
        public ActionResult AddTests(AmenitiesViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Tests tests = model.ToTests();

            try
            {
                new Amenities().AddTests(tests);
                return RedirectToAction("Tests", "Assistant");
            }
            catch (TestsAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}