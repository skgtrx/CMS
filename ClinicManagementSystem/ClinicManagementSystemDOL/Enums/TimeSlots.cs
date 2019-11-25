using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Enums
{
    public enum TimeSlots
    {
        [Display(Name="09:00Am")]
        slot1=1,
        [Display(Name ="09:30Am")]
        slot2 = 2,
        [Display(Name ="10:00Am")]
        slot3 = 3,
        [Display(Name = "10:30Am")]
        slot4 = 4,
        [Display(Name = "11:00Am")]
        slot5 = 5,
        [Display(Name = "11:30Am")]
        slot6 = 6,
        [Display(Name = "12:00Pm")]
        slot7 = 7,
        [Display(Name = "12:30Pm")]
        slot8 = 8,
        [Display(Name = "01:00Pm")]
        slot9 = 9,
        [Display(Name = "01:30Pm")]
        slot10 = 10,
        [Display(Name = "02:00Pm")]
        slot11 = 11,
        [Display(Name = "02:30Pm")]
        slot12 = 12,
        [Display(Name = "03:00Pm")]
        slot13 = 13,
        [Display(Name = "03:30Pm")]
        slot14 = 14,
        [Display(Name = "04:00Pm")]
        slot15 = 15,
        [Display(Name = "05:00Pm")]
        slot16 = 16,
        [Display(Name = "05:30Pm")]
        slot17 = 17,
        [Display(Name = "06:00Pm")]
        slot18 = 18,
    }
}
