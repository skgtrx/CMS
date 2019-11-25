using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ClinicManagementSystemBL;
using System.Web.Mvc;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Exceptions;
using ClinicManagementSystemDOL.Enums;

namespace ClinicManagementSystem.Controllers
{
    [RoutePrefix("Doctor")]
    public class DoctorController : Controller
    {
        /// <summary>
        /// This action is used to get the Doctor profile view.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the profile details of the doctor.
        /// </remarks>
        /// <returns>The view of doctor profile</returns>
        [Route("Profile")]
        public ActionResult DoctorProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var DoctorViewObj = new DoctorProfileView((int)Session["userId"]);
            var viewModel = new DoctorProfileViewModel
            {
                Doctor = DoctorViewObj.GetDoctorProfile().Doctor,
                DoctorFee = DoctorViewObj.GetDoctorProfile()
            };
            
            return View(viewModel);
        }

        /// <summary>
        /// This action is used to get the record of all appointments for a particular doctor from the database.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the appointment list of all patients that have booked an appointment at that doctor.
        /// </remarks>
        /// <returns>The list of appointments of a particular doctor</returns>
        [Route("Appointments")]
        public ActionResult AppointmentList()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            AppointmentData appointments = new AppointmentData();
            List<Appointment> AppointmentList = appointments.GetDoctorAppointmentList((int)Session["userId"]);
            var DoctorViewObj = new DoctorProfileView((int)Session["userId"]);
            
            return View(AppointmentList);
        }

        /// <summary>
        /// This action is used to get the doctor edit profile view.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the UI of edit doctor profile.
        /// </remarks>
        /// <returns>This controller action returns the edit profile view</returns>
        [HttpGet]
        [Route("Profile/Edit")]
        public ActionResult EditProfile()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var DoctorViewObj = new DoctorProfileView((int)Session["userId"]);
            var Doctor = DoctorViewObj.GetDoctorProfile().Doctor;
            var model = new DoctorProfileEditViewModel();
            AutoMapper.Mapper.Map(Doctor, model);
            
            return View(model);
        }

        /// <summary>
        /// This is a post action that updates the doctor profile.
        /// </summary>
        /// <remarks>
        /// This is the controller post action that updates the changes made by the doctor.
        /// </remarks>
        /// <returns>This provides the action to update a doctor profile</returns>
        [HttpPost]
        [Route("Profile/Edit")]
        public ActionResult EditProfile(DoctorProfileEditViewModel model, string returnUrl)
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
            try
            {
                if (new DoctorEdit().UpdateDoctor(user))
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
        /// This action is used to get the view of add remark.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the add remark view.
        /// </remarks>
        /// <returns>The add remark view</returns>
        [HttpGet]
        public ActionResult AddRemark(int id)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var remark = new RemarkViewModel();
            remark.AppointmentId = id;
            var DoctorViewObj = new DoctorProfileView((int)Session["userId"]);
            
            return View(remark);
        }

        /// <summary>
        /// This action is used to add a remark into the database.
        /// </summary>
        /// <remarks>
        /// This is the controller action that adds a remark and returns the updated appointment list.
        /// </remarks>
        /// <param name="model">Contains the value Remark</param>
        /// <returns>The list of appointments</returns>
        [HttpPost]
        public ActionResult AddRemark(RemarkViewModel model)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Diagnosis diagnosis = new Diagnosis();
            AutoMapper.Mapper.Map(model, diagnosis);
            AddDiagnosisAndRecord addDiagnoseAndRecord = new AddDiagnosisAndRecord();
            try
            {
                addDiagnoseAndRecord.CreateDiagnosisAndRecord(diagnosis);
                return RedirectToAction("AppointmentList");
            }
            catch (Exception)
            {
                return RedirectToAction("AppointmentList");
            }
        }

        /// <summary>
        /// An intermediatory Action which pass the value from AddRemark View to AddMedicinesAndTests View
        /// </summary>
        /// <param name="model">Contains Remark value passed from AddRemark View</param>
        /// <returns>To the Form View for adding Medicines and Tests after the disgnosis of doctor</returns>
        [HttpPost]
        public ActionResult Intermediatory(AddMedicinesAndTestViewModel model)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            return Json(Url.Action("AddMedicinesAndTests", "Doctor", model));
        }

        /// <summary>
        /// GET Action which calls the view for adding medicines and tests after the disgnosis of doctor
        /// </summary>
        /// <param name="model">Contains the Remark value passed from AddRemark View</param>
        /// <returns>The Form View for adding tests and medicines after the disgnosis of doctor</returns>
        [HttpGet]
        public ActionResult AddMedicinesAndTests(AddMedicinesAndTestViewModel model)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            MedicineTableData medicineData = new MedicineTableData();
            TestsTableData testData = new TestsTableData();
            AddMedicinesAndTestViewModel medicineAndTestNames = new AddMedicinesAndTestViewModel();
            medicineAndTestNames.MedicinesList = medicineData.MedicinesInformation();
            medicineAndTestNames.TestsList = testData.TestsInformation();
            medicineAndTestNames.AppointmentId = model.AppointmentId;
            medicineAndTestNames.Remark = model.Remark;
            var DoctorViewObj = new DoctorProfileView((int)Session["userId"]);
            
            return View(medicineAndTestNames);
        }

        /// <summary>
        /// POST Action which calls for Adding Medicines and Tests after the disgnosis of doctor
        /// </summary>
        /// <param name="model">Contains the name of all medicines and tests pescribed by doctor</param>
        /// <param name="returnUrl">Contains the return URL to which it needs to be redirected</param>
        /// <returns>To the Form view for list of Appointments</returns>
        [HttpPost]
        public ActionResult AddMedicinesAndTests(AddMedicinesAndTestViewModel model, string returnUrl)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            Diagnosis diagnosis = new Diagnosis();
            diagnosis.AppointmentId = model.AppointmentId;
            diagnosis.Remark = model.Remark;
            List<MedicineRecord> medicinesList = model.ToMedicineList();
            var testsList = model.ToTestsList();
            AddDiagnosisAndRecord addDiagnoseAndRecord = new AddDiagnosisAndRecord();
            addDiagnoseAndRecord.CreateDiagnosisAndRecord(diagnosis, medicinesList, testsList);

            return Json("Appointments");
        }

        /// <summary>
        /// Returns the Medical-History view.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the Medical-History UI.
        /// </remarks>
        /// <param name="UserId">Contains the User Id of patient whose medical history to be viewed</param>
        /// <returns>the list of medical history for a patient</returns>
        [HttpGet]
        [Route("MedicalHistory/{UserId}")]
        public ActionResult PatientMedicalHistory(int UserId)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            var History = new MedicalHistory();
            List<MedicalHistoryViewModel> model = new List<MedicalHistoryViewModel>();
            List<Diagnosis> DiagnosisHistories = History.GetDiagnosisHistory(UserId);
            var MedicineNames = new List<List<MedicineRecord>>();

            foreach (var item in DiagnosisHistories)
            {
                var MedicalHistory = new MedicalHistoryViewModel();
                MedicalHistory.DiagnosisHistories = item;
                MedicalHistory.MedicineList = History.GetMedicineNames(item.Id);
                MedicalHistory.TestList = History.GetTestNames(item.Id);

                model.Add(MedicalHistory);
            }
            var DoctorViewObj = new DoctorProfileView((int)Session["userId"]);
            
            return View(model);
        }

        /// <summary>
        /// GET Action to show the view for adding a Doctor Schedule
        /// </summary>
        /// <returns>The Form view for Adding/Updating the Doctor Schedule</returns>
        [HttpGet]
        [Route("Schedule")]
        public ActionResult DoctorSchedule()
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            DoctorScheduleViewModel doctorSchedule = new DoctorScheduleViewModel();
            AppointmentTime time = new AppointmentTime();
            time.Id = 0;
            time.Time = "";
            doctorSchedule.From.Add(time);
            return View(doctorSchedule);
        }

        /// <summary>
        /// POST Action for Adding/Updating doctor schedule
        /// </summary>
        /// <param name="model">Contains the value for Schedule of a doctor</param>
        /// <returns>To the Home view of Doctor</returns>
        [HttpPost]
        public ActionResult DoctorScheduleSet(DoctorScheduleViewModel model)
        {
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            model.DoctorId = (int)Session["userId"];
            model.ToAvailabilityList();
            DoctorScheduleManagement doctorSchedule = new DoctorScheduleManagement();
            if (doctorSchedule.MarkAvailability(model.AvailabilityList))
            {
                return Json(Url.Action("Index", "Home"));
            }
            return Json(Url.Action("DoctorSchedule", "Home"));
        }

        /// <summary>
        /// POST Action for populating the available time slots
        /// </summary>
        /// <param name="id">Contains the id of the time slot selected</param>
        /// <returns>The available schedule for doctor</returns>
        [HttpPost]
        public ActionResult PopulateTimeSlots(TimeSlotViewModel model)//int id)
        {
            DateTime date = DateTime.Now;
            if (Session["userId"] == null)
            {
                return Redirect("~");
            }
            DoctorScheduleViewModel doctorScheduleViewModel = new DoctorScheduleViewModel();
            DoctorScheduleManagement doctorScheduleManagement = new DoctorScheduleManagement();
            doctorScheduleViewModel.From = doctorScheduleManagement.GetTimeSlots();
            doctorScheduleViewModel.SelectedFromId =model.SelectedSlot;
            if (model.SelectedSlot==0)
            {
                doctorScheduleViewModel.PreviousFromScheduleTime = doctorScheduleManagement.GetExistingFromSchedule((int)Session["userId"],model.Date);
                doctorScheduleViewModel.PreviousToScheduleTime = null;
            }
            else
            {
                doctorScheduleViewModel.PreviousFromScheduleTime = null;
                doctorScheduleViewModel.PreviousToScheduleTime = doctorScheduleManagement.GetExistingToSchedule((int)Session["userId"],model.Date);
            }
            return View(doctorScheduleViewModel);
        }

        [HttpPost]
        public ActionResult PopulateToTimeSlots(TimeSlotViewModel model)
        {
            DoctorScheduleViewModel doctorScheduleViewModel = new DoctorScheduleViewModel();
            DoctorScheduleManagement doctorScheduleManagement = new DoctorScheduleManagement();
            doctorScheduleViewModel.From = doctorScheduleManagement.GetTimeSlots();
            doctorScheduleViewModel.PreviousToScheduleTime = doctorScheduleManagement.GetExistingToSchedule((int)Session["userId"], model.Date);
            doctorScheduleViewModel.PreviousFromScheduleTime = doctorScheduleManagement.GetExistingFromSchedule((int)Session["userId"], model.Date);
            if (doctorScheduleViewModel.PreviousToScheduleTime != null)
            {
                
                return View(doctorScheduleViewModel);
            }
            else
            {
                return null;
            }
        }
    }
}