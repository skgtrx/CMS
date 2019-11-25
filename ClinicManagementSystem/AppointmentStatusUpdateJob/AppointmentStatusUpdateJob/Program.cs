using AppointmentStatusUpdateJob.SQL_Access;
using AppointmentStatusUpdateJob.Enum;

namespace AppointmentStatusUpdateJob
{
    class Program
    {
        static void Main(string[] args)
        {
            int CurrentAppointmentstatus = (int)AppointmentStatus.Pending;
            SqlHelper sqlHelper = new SqlHelper();
            string Query = $"Update Appointments set AppointmentStatusId='{(int)AppointmentStatus.Expired}' " +
                $"WHERE AppointmentStatusId='{CurrentAppointmentstatus}' and DATEDIFF( d, Date, GETDATE() ) = 1";
            int RowsAffected = sqlHelper.ExecuteUpdate(Query);
        }
    }
}
