using System.ComponentModel;

namespace ClinicManagementSystemDOL.Enums
{
    public enum AppointmentStatus
    {
        [Description("Open")]
        Open = 1,

        [Description("Pending")]
        Pending = 2,

        [Description("Rejected")]
        Rejected = 3,

        [Description("Closed")]
        Closed = 4,

        [Description("Expired")]
        Expired = 5
    }
}
