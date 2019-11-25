using ClinicManagementSystem.Models;
using ClinicManagementSystemDOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTutorial.Infrastructure
{
    public class AutomapperWebProfile : AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<RegisterViewModel,Users>();
            CreateMap<RegisterViewModel, Patients>();
            CreateMap<AdminRegisterViewModel, Users >();
            CreateMap<AssistantRegisterViewModel, Users>();
            CreateMap<DoctorRegisterViewModel, Users>();
            CreateMap<DoctorRegisterViewModel, DoctorSpecialization>();
            CreateMap<Users, AssistantProfileEditViewModel>();
            CreateMap<AssistantProfileEditViewModel, Users>();
            CreateMap<Users, PatientProfileEditViewModel>();
            CreateMap<Patients, PatientProfileEditViewModel>();
            CreateMap<PatientProfileEditViewModel, Users>();
            CreateMap<PatientProfileEditViewModel, Patients>();
            CreateMap<Users, DoctorProfileEditViewModel>();
            CreateMap<DoctorProfileEditViewModel, Users>();
            CreateMap<Users, AdminProfileEditViewModel>();
            CreateMap<AdminProfileEditViewModel, Users>();
            CreateMap<RemarkViewModel, Diagnosis>();
            CreateMap<DoctorProfileEditViewModel, DoctorFee>();
            CreateMap<DoctorFee, DoctorProfileEditViewModel>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutomapperWebProfile>();


            });
        }



    }
}