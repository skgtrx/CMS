using ClinicManagementSystemDAL;
using ClinicManagementSystemDOL.Models;

namespace ClinicManagementSystemBL
{
    public class AppointmentEdit
    {
        /// <summary>
        /// Updates The Appointment Status based on the Appointment Id and Status Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns>Flag as true if Updated Successfully else false</returns>
        public bool EditAppointment(int id, int status)
        {
            AppointmentEditRemove appointmentEdit = new AppointmentEditRemove();
            if (appointmentEdit.EditAppointment(id, status))
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
