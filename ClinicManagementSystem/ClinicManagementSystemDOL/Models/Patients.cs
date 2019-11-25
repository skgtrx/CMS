using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemDOL.Models
{
    public class Patients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public Users Users { get; set; }
        [Index(IsUnique = true)]
        [ForeignKey("Users"), Column(Order = 0)]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(80)]
        [Display(Name = "Conservator Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Special character are not allowed")]
        public string ConservatorName { get; set; }

        [Required(ErrorMessage = "Telephone Number Required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string ConservatorPhone { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        [Display(Name = "Conservator Relation")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Special character are not allowed")]
        public string ConservatorRelation { get; set; }
        
    }
}
