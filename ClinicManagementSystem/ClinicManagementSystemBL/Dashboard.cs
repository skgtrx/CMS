using ClinicManagementSystemDAL;

namespace ClinicManagementSystemBL
{
    public class Dashboard
    {

        /// <summary>
        /// Instance of DashboardData for accessing methods defined in DAL
        /// </summary>
        DashboardData dashboardData = new DashboardData();

        #region Admin and Assistant

        /// <summary>
        /// Retrives the count of Users for Admin and Assistant Dashboard
        /// </summary>
        /// <returns>Count of Users</returns>
        public int GetCountOfUser()
        {
            return dashboardData.GetUserCount();
        }

        /// <summary>
        /// Retrives the count of Doctors for Admin and Assistant Dashboard
        /// </summary>
        /// <returns>Count of Doctors</returns>
        public int GetCountOfDoctor()
        {
            return dashboardData.GetDoctorCount();
        }

        /// <summary>
        /// Retrives the count of Patients for Admin and Assistant Dashboard
        /// </summary>
        /// <returns>Count of Patients</returns>
        public int GetCountOfPatient()
        {
            return dashboardData.GetPatientCount();
        }

        /// <summary>
        /// Retrives the count of Appointments for Admin and Assistant Dashboard
        /// </summary>
        /// <returns>Count of Appointments</returns>
        public int GetCountOfAppointment()
        {
            return dashboardData.GetAppointmentCount();
        }

        /// <summary>
        /// Retrives the count of Todays Appointment for Admin and Assistant Dashboard
        /// </summary>
        /// <returns>Count of Todays Appointment</returns>
        public int GetCountOfTodaysAppointment()
        {
            return dashboardData.GetTodaysAppointmentCount();
        }
        #endregion

        #region Patient

        /// <summary>
        /// Retrives the count of Upcomming Appointments of a particular patient
        /// </summary>
        /// <returns>Count of Upcomming Appointments</returns>
        public int GetUpcomingPatientAppointments(int PatientId)
        {
            return dashboardData.GetUpcomingPatientAppointments(PatientId);
        }

        /// <summary>
        /// Retrives the count of Pending Appointments of a particular patient
        /// </summary>
        /// <returns>Count of Pending Appointments</returns>
        public int GetPendingPatientAppointments(int PatientId)
        {
            return dashboardData.GetPendingPatientAppointments(PatientId);
        }

        /// <summary>
        /// Retrives the count of Total Appointments of a particular patient
        /// </summary>
        /// <returns>Count of Total Appointments</returns>
        public int GetTotalPatientAppointments(int PatientId)
        {
            return dashboardData.GetTotalPatientAppointments(PatientId);
        }
        #endregion


        #region Doctor

        /// <summary>
        /// Retrives the count of Appointments of a particular doctor
        /// </summary>
        /// <returns>Count of Appointments</returns>
        public int GetDoctorAppointments(int DoctorId)
        {
            return dashboardData.GetDoctorAppointments(DoctorId);
        }

        /// <summary>
        /// Retrives the count of Todays Appointments of a particular doctor
        /// </summary>
        /// <returns>Count of Todays Appointments</returns>
        public int GetDoctorTodaysAppointments(int DoctorId)
        {
            return dashboardData.GetDoctorTodaysAppointments(DoctorId);
        }

        /// <summary>
        /// Retrives the count of Patients of a particular doctor
        /// </summary>
        /// <returns>Count of Patients</returns>
        public int GetDoctorsPatient(int DoctorId)
        {
            return dashboardData.GetDoctorsPatient(DoctorId);
        }
        #endregion
    }
}
