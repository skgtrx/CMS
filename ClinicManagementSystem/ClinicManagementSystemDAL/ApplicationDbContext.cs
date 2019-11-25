using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystemDOL.Models;

namespace ClinicManagementSystemDAL
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatus { get; set; }
        public DbSet<AppointmentTime> AppointmentTime { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        public DbSet<DoctorSpecialization> DoctorSpecialization { get; set; }
        public DbSet<InvoiceRecord> InvoiceRecord { get; set; }
        public DbSet<MedicineRecord> MedicineRecord { get; set; }
        public DbSet<Medicines> Medicines { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<TestRecord> TestRecord { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<DoctorFee> DoctorFee { get; set; }
        public DbSet<ResetPasswd> ResetPassword { get; set; }
        public ApplicationDbContext()
            : base("name = ClinicManagementSystemCS")
        {
        }

    }
}
