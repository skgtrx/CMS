using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystemDOL.Models
{
    public class TestRecord
    {
        public Diagnosis Diagnosis { get; set; }
        [ForeignKey("Diagnosis"), Column(Order = 1)]
        [Key]
        public int DiagnosisId { get; set; }

        public Tests Tests { get; set; }
        [ForeignKey("Tests"), Column(Order = 0)]
        [Key]
        public int TestId { get; set; }
    }
}
