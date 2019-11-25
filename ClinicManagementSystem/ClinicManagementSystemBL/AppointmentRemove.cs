using ClinicManagementSystemDAL;

namespace ClinicManagementSystemBL
{
    public class AppointmentRemove
    {
        /// <summary>
        /// Deletes appointment if not closed based in Appointment Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Flag as true if Deleted Successfully else false</returns>
        public bool DeleteAppointment(int id)
        {
            AppointmentEditRemove appointmentRemove = new AppointmentEditRemove();
            if (appointmentRemove.RemoveAppointment(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
