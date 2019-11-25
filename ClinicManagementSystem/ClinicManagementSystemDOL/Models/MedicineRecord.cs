using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Models
{
    public class MedicineRecord
    {

        public Diagnosis Diagnosis { get; set; }
        [ForeignKey("Diagnosis"), Column(Order = 0)]
        [Key]
        public int DiagnosisId { get; set; }

        public Medicines Medicines { get; set; }
        [ForeignKey("Medicines"), Column(Order = 1)]
        [Key]
        public int MedicineId { get; set; }

        [Required]
        [DefaultValue(typeof(int),"0")]
        public int Quantity { get; set; }
    }
}
