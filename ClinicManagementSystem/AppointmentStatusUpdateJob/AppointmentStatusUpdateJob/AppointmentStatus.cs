using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentStatusUpdateJob.Enum
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
