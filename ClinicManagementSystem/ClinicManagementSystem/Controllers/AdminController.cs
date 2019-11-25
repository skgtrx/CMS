using ClinicManagementSystem.Models;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Exceptions;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// This region consists of all the actions which shows the list of all users for Admin
        /// </summary>
        #region User Lists
        /// <summary>
        /// Shows All Admin List
        /// </summary>
        /// <returns>List of admin view with Datatable</returns>
        [Route("Admins")]
        public ActionResult AdminList()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AdminData adminData = new AdminData();
            var adminsList = adminData.GetAdminsList();
            return View(adminsList);
        }

        /// <summary>
        /// Shows All Assistant List
        /// </summary>
        /// <returns>List of assistant view with Datatable</returns>
        [Route("Assistants")]
        public ActionResult AssistantList()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AssistantData assistantdData = new AssistantData();
            var assistantsList = assistantdData.GetAssistantsList();
            return View(assistantsList);
        }

        /// <summary>
        /// Shows All Doctor List
        /// </summary>
        /// <returns>List of doctor view with Datatable</returns>
        [Route("Doctors")]
        public ActionResult DoctorList()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            DoctorsData doctorsData = new DoctorsData();
            List<DoctorFee> doctorList = doctorsData.GetAllDoctorsList();
            return View(doctorList);
        }

        /// <summary>
        /// Shows All patient List
        /// </summary>
        /// <returns>List of patient view with Datatable</returns>
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
        /// Shows All Appointment List
        /// </summary>
        /// <returns>List of appointment view with Datatable</returns>
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
        #endregion

        /// <summary>
        /// This region consists of all the actions which adds new users (Admins, Doctors, Assistants & Paients) for Admin
        /// </summary>
        #region Add Users
        /// <summary>
        /// This Action calls for the form for Adding Admin
        /// </summary>
        /// <returns>Form View for Adding Admin</returns>
        [Route("Admin/Add")]
        [HttpGet]
        public ActionResult AddAdmin()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var model = new AdminRegisterViewModel
            {
                Password = Default.Password.GetDescription()
            };
            return View(model);
        }
        /// <summary>
        /// This Action Calls BL for adding admin into Database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Redirects to the Admin List</returns>
        [Route("Admin/Add")]
        [HttpPost]
        public ActionResult AddAdmin(AdminRegisterViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            AutoMapper.Mapper.Map(model, user);
            user.RoleId = Convert.ToInt32(Roles.Admin);
            user.CreatedBy = (int)Session["userId"];

            try
            {
                if (new Accounts().AddUser(user))
                {
                    return Redirect("~/Admin/Admins");
                }
                else
                {
                    return Redirect("~/Admin/Admin/Add");
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
                ModelState.AddModelError("", "Unable to add Admin");
                return View(model);
            }
        }

        /// <summary>
        /// This Action calls for the form for Adding Assistant
        /// </summary>
        /// <returns>Form View for Adding Assistant</returns>
        [HttpGet]
        [Route("Assistant/Add")]
        public ActionResult AddAssistant()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var model = new AssistantRegisterViewModel
            {
                Password = Default.Password.GetDescription()
            };
            return View(model);
        }

        /// <summary>
        /// This Action Calls BL for adding assistant into Database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Redirects to the Assistant List</returns>
        [HttpPost]
        [Route("Assistant/Add")]
        public ActionResult AddAssistant(AssistantRegisterViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            user.RoleId = Convert.ToInt32(Roles.Assistant);
            AutoMapper.Mapper.Map(model, user);
            user.CreatedBy = (int)Session["userId"];
            try
            {
                if (new Accounts().AddUser(user))
                {
                    return Redirect("~/Admin/Assistants");
                }
                else
                {
                    return RedirectToAction("~/Admin/Assistant/Add");
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
                ModelState.AddModelError("", "Unable to add Assistant");
                return View(model);
            }
        }

        /// <summary>
        /// This Action calls for the form for Adding Patient
        /// </summary>
        /// <returns>Form View for Adding Patient</returns>
        [HttpGet]
        [Route("Patient/Add")]
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
        [Route("Patient/Add")]
        public ActionResult AddPatient(RegisterViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            user.RoleId = Convert.ToInt32(Roles.Patient);
            AutoMapper.Mapper.Map(model, user);
            user.CreatedBy = (int)Session["userId"];
            Patients patient = new Patients();
            AutoMapper.Mapper.Map(model, patient);

            try
            {
                if (new Accounts().AddUser(user, patient))
                {
                    return Redirect("~/Admin/Patients");
                }
                else
                {
                    return RedirectToAction("~/Admin/Patient/Add");
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
                ModelState.AddModelError("", "Unable to register patient");
                return View(model);
            }
        }

        /// <summary>
        /// This Action calls for the form for Adding Doctor
        /// </summary>
        /// <returns>Form View for Adding Doctor</returns>
        [HttpGet]
        [Route("Doctors/Add")]
        public ActionResult AddDoctor()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var model = new DoctorRegisterViewModel
            {
                Password = Default.Password.GetDescription()
            };
            return View(model);
        }

        /// <summary>
        /// This Action Calls BL for adding doctor into Database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Redirects to the Doctors List</returns>
        [HttpPost]
        [Route("Doctors/Add")]
        public ActionResult AddDoctor(DoctorRegisterViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            user.RoleId = Convert.ToInt32(Roles.Doctor);
            AutoMapper.Mapper.Map(model, user);
            user.CreatedBy = (int)Session["userId"];
            DoctorAvailability availability = new DoctorAvailability();
            DoctorFee doctorFee = new DoctorFee();
            doctorFee.Fee = model.Fee;

            List<DoctorSpecialization> doctorSpecialization = new List<DoctorSpecialization>();
            foreach (var item in model.Specializations)
            {
                DoctorSpecialization DoctorSpecialization = new DoctorSpecialization();
                DoctorSpecialization.SpecializationId = (int)item;
                doctorSpecialization.Add(DoctorSpecialization);
            }
            try
            {
                if (new Accounts().AddUser(user, availability, doctorSpecialization, doctorFee))
                {
                    return Redirect("~/Admin/Doctors");
                }
                else
                {
                    return RedirectToAction("~/Admin/Doctors/Add");
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
                ModelState.AddModelError("", "Unable to add doctor");
                return View(model);
            }
        }
        #endregion

        /// <summary>
        /// This region consists of all the actions which Removes existing users for Admin
        /// </summary>
        #region Remove Users

        /// <summary>
        /// This Action calls for the button which can delete an Admin
        /// </summary>
        /// <returns>The form view which contains list of Admins after deleting a paricular Admin</returns>
        public ActionResult RemoveAdminUser(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AdminRemove Admin = new AdminRemove();
            Admin.DeleteAdmin(id);
            return RedirectToAction("Admins");
        }

        /// <summary>
        /// This Action calls for the button which can delete an Assistant
        /// </summary>
        /// <returns>The form view which contains list of Assistants after deleting a paricular Assistant</returns>
        public ActionResult RemoveAssistantUser(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AssistantRemove Assistant = new AssistantRemove();
            Assistant.DeleteAssistant(id);
            return RedirectToAction("Assistants");
        }

        /// <summary>
        /// This Action calls for the button which can delete a Patient
        /// </summary>
        /// <returns>The form view which contains list of Patients after deleting a paricular Patient</returns>
        public ActionResult RemovePatientUser(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            PatientRemove Patient = new PatientRemove();
            Patient.DeletePatient(id);
            return RedirectToAction("Patients");
        }

        /// <summary>
        /// This Action calls for the button which can delete a Doctor
        /// </summary>
        /// <returns>The form view which contains list of Doctors after deleting a paricular Doctor</returns>
        public ActionResult RemoveDoctorUser(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            DoctorRemove Doctor = new DoctorRemove();
            Doctor.DeleteDoctor(id);
            return RedirectToAction("Doctors");
        }

        /// <summary>
        /// This Action calls for the button which can delete an Appointment
        /// </summary>
        /// <returns>The form view which contains list of Appointments after deleting a paricular Appointment</returns>
        public ActionResult RemoveAppointmentData(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentRemove Appointment = new AppointmentRemove();
            Appointment.DeleteAppointment(id);
            return RedirectToAction("Appointments");
        }
        #endregion

        /// <summary>
        /// This region consists of all the actions by which an Admin can view anyone's profile and update it
        /// </summary>
        #region User Profile

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Admin
        /// </summary>
        /// <param name="id">User Id for Admin whose profile is to be displayed</param>
        /// <returns>To the action which get the details of Admin and show the profile</returns>
        public ActionResult AdminUserDetails(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            TempData["Id"] = id;
            return RedirectToAction("Admin/Profile");
        }

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Admin
        /// </summary>
        /// <returns>The form view which contains the profile of an Admin</returns>
        [Route("Admin/Profile")]
        public ActionResult AdminUserProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            if (TempData["Id"] == null)
            {
                return RedirectToAction("Admins");
            }
            else
            {
                var AdminViewObj = new AdminProfileView((int)TempData["Id"]);
                var viewModel = new AdminProfileViewModel
                {
                    Admin = AdminViewObj.GetAdminProfile()
                };
                return View(viewModel);
            }
        }

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Assistant
        /// </summary>
        /// <param name="id">User Id for Assistant whose profile is to be displayed</param>
        /// <returns>To the action which get the details of Assistant and show the profile</returns>
        public ActionResult AssistantUserDetails(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            TempData["Id"] = id;
            return RedirectToAction("Assistant/Profile");
        }

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Assistant
        /// </summary>
        /// <returns>The form view which contains the profile of an Assistant</returns>
        [Route("Assistant/Profile")]
        public ActionResult AssistantUserProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            if (TempData["Id"] == null)
            {
                return RedirectToAction("Assistants");
            }
            else
            {
                var AssistantViewObj = new AssistantProfileView((int)TempData["Id"]);
                var viewModel = new AssistantProfileViewModel
                {
                    Assistant = AssistantViewObj.GetAssistantProfile()
                };
                return View(viewModel);
            }
        }

        /// <summary>
        /// GET Action for getting the profile details of an Assistant
        /// </summary>
        /// <param name="id">User Id for Assistant whose profile is to be Edited</param>
        /// <returns>To the form view model for updating the Assistant Profile</returns>
        [HttpGet]
        [Route("Assistant/Profile/Edit/{id}")]
        public ActionResult AssistantUserProfileEdit(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var AssistantViewObj = new AssistantProfileView(id);
            Users user= AssistantViewObj.GetAssistantProfile();
            AssistantProfileEditViewModel model = new AssistantProfileEditViewModel();
            AutoMapper.Mapper.Map(user, model);
            return View(model);
        }

        /// <summary>
        /// POST Action for updating Assistant's profile
        /// </summary>
        /// <param name="model">Contains all the values which are to be updated in database</param>
        /// <param name="returnUrl">Contains the return URL to which it needs to be redirected</param>
        /// <returns>The Form view of Assistant's profile</returns>
        [HttpPost]
        [Route("Assistant/Profile/Edit/{id}")]
        public ActionResult AssistantUserProfileEdit(AssistantProfileEditViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            AutoMapper.Mapper.Map(model, user);
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                user.Password = model.Password;
            }
            else
            {
                user.Password = model.NewPassword;
            }
            AssistantEdit Assistant = new AssistantEdit();
            try
            {
                if (Assistant.UpdateAssistant(user))
                {
                    return RedirectToAction($"AssistantUserDetails/{model.Id}");
                }
                else
                {
                    return RedirectToAction($"/Assistant/Profile/Edit/{model.Id}");
                }
            }
            catch (EmailAlreadyExistsEx ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View(model);
            }
            catch (PhoneAlreadyExistsEx ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to update Assistant information");
                return View(model);
            }
            
        }

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Patient
        /// </summary>
        /// <param name="id">User Id for Patient whose profile is to be displayed</param>
        /// <returns>To the action which get the details of Patient and show the profile</returns>
        public ActionResult PatientUserDetails(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            TempData["Id"] = id;
            return RedirectToAction("Patient/Profile");
        }

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Patient
        /// </summary>
        /// <returns>The form view which contains the profile of an Patient</returns>
        [Route("Patient/Profile")]
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
        /// GET Action for getting the profile details of an Patient
        /// </summary>
        /// <param name="id">User Id for Patient whose profile is to be Edited</param>
        /// <returns>To the form view model for updating the Patient Profile</returns>
        [HttpGet]
        [Route("Patient/Profile/Edit/{id}")]
        public ActionResult PatientUserProfileEdit(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var PatientViewObj = new PatientProfileView(id);
            var PatientInfo = PatientViewObj.GetPatientInfo();
            var Patient = PatientViewObj.GetPatientProfile();
            PatientProfileEditViewModel model = new PatientProfileEditViewModel();
            AutoMapper.Mapper.Map(PatientInfo, model);
            AutoMapper.Mapper.Map(Patient, model);
            return View(model);
        }

        /// <summary>
        /// POST Action for updating Patient's profile
        /// </summary>
        /// <param name="model">Contains all the values which are to be updated in database</param>
        /// <param name="returnUrl">Contains the return URL to which it needs to be redirected</param>
        /// <returns>The Form view of Patient's profile</returns>
        [HttpPost]
        [Route("Patient/Profile/Edit/{id}")]
        public ActionResult PatientUserProfileEdit(PatientProfileEditViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            Patients patient = new Patients();
            AutoMapper.Mapper.Map(model, user);
            AutoMapper.Mapper.Map(model, patient);
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                user.Password = model.Password;
            }
            else
            {
                user.Password = model.NewPassword;
            }
            PatientEdit Patient = new PatientEdit();
            try
            {
                if (Patient.UpdatePatient(user, patient))
                {
                    return RedirectToAction($"PatientUserDetails/{model.Id}");
                }
                else
                {
                    return RedirectToAction($"/Patient/Profile/Edit/{model.Id}");
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
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to update Patient information");
                return View(model);
            }
        }

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Patient
        /// </summary>
        /// <param name="id">User Id for Doctor whose profile is to be displayed</param>
        /// <returns>To the action which get the details of Patient and show the profile</returns>
        public ActionResult DoctorUserDetails(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            TempData["Id"] = id;
            return RedirectToAction("Doctor/Profile");
        }

        /// <summary>
        /// This Action calls for the Form View which contains the profile of a particular Doctor
        /// </summary>
        /// <returns>The form view which contains the profile of an Doctor</returns>
        [Route("Doctor/Profile")]
        public ActionResult DoctorUserProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            if (TempData["Id"] == null)
            {
                return RedirectToAction("Doctors");
            }
            else
            {
                var DoctorViewObj = new DoctorProfileView((int)TempData["Id"]);
                var viewModel = new DoctorProfileViewModel
                {
                    Doctor = DoctorViewObj.GetDoctorProfile().Doctor,
                    DoctorFee = DoctorViewObj.GetDoctorProfile()
                };
                return View(viewModel);
            }
        }

        /// <summary>
        /// GET Action for getting the profile details of an Doctor
        /// </summary>
        /// <param name="id">User Id for Doctor whose profile is to be Edited</param>
        /// <returns>To the form view model for updating the Doctor Profile</returns>
        [HttpGet]
        [Route("Doctor/Profile/Edit/{id}")]
        public ActionResult DoctorUserProfileEdit(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var DoctorViewObj = new DoctorProfileView(id);
            var Doctor = DoctorViewObj.GetDoctorProfile();
            DoctorProfileEditViewModel model = new DoctorProfileEditViewModel();
            AutoMapper.Mapper.Map(Doctor, model);
            AutoMapper.Mapper.Map(Doctor.Doctor, model);
            return View(model);
        }

        /// <summary>
        /// POST Action for updating Doctor's profile
        /// </summary>
        /// <param name="model">Contains all the values which are to be updated in database</param>
        /// <param name="returnUrl">Contains the return URL to which it needs to be redirected</param>
        /// <returns>The Form view of Doctor's profile</returns>
        [HttpPost]
        [Route("Doctor/Profile/Edit/{id}")]
        public ActionResult DoctorUserProfileEdit(DoctorProfileEditViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
            DoctorFee doctorFee = new DoctorFee();
            AutoMapper.Mapper.Map(model, user);
            AutoMapper.Mapper.Map(model, doctorFee);
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                user.Password = model.Password;
            }
            else
            {
                user.Password = model.NewPassword;
            }
            DoctorEdit Doctor = new DoctorEdit();
            try
            {
                if (Doctor.UpdateDoctor(user, doctorFee))
                {
                    return RedirectToAction($"DoctorUserDetails/{model.Id}");
                }
                else
                {
                    return RedirectToAction($"/Doctor/Profile/Edit/{model.Id}");
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
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to update Doctor information");
                return View(model);
            }
        }

        #endregion

        /// <summary>
        /// This region consists of the actions which is used to update an Admin's own profile
        /// </summary>
        #region Admin Personal

        /// <summary>
        /// Action returns admin profile view
        /// </summary>
        /// <returns>profile view</returns>
        [Route("Profile")]
        public ActionResult AdminProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var AdminViewObj = new AdminProfileView((int)Session["userId"]);
            var viewModel = new AdminProfileViewModel
            {
                Admin = AdminViewObj.GetAdminProfile()
            };
            return View(viewModel);
        }

        /// <summary>
        /// GET Action for getting the profile details of the logged in Admin
        /// </summary>
        /// <returns>The Form view for updating the Admin's profile</returns>
        [HttpGet]
        [Route("Profile/Edit")]
        public ActionResult EditProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var AdminViewObj = new AdminProfileView((int)Session["userId"]);
            var Admin = AdminViewObj.GetAdminProfile();
            var model = new AdminProfileEditViewModel();
            AutoMapper.Mapper.Map(Admin, model);
            return View(model);
        }

        /// <summary>
        /// POST Action for updating logged in Admin's profile
        /// </summary>
        /// <param name="model">Contains all the values which are to be updated in database</param>
        /// <param name="returnUrl">Contains the return URL to which it needs to be redirected</param>
        /// <returns>The Form view of logged in Admin's profile</returns>
        [HttpPost]
        [Route("Profile/Edit")]
        public ActionResult EditProfile(AdminProfileEditViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Users user = new Users();
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
            AdminEdit Admin = new AdminEdit();
            try
            {
                if (Admin.UpdateAdmin(user))
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
            catch (Exception e)
            {
                ModelState.AddModelError("", "Unable to update information");
                return View(model);
            }
        }

            #endregion
    }
}