using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystemDOL.Models
{
    public class ResetPasswd
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Id { get; set; }

        public Users Users { get; set; }
        [ForeignKey("Users"), Column(Order = 1)]
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ResetRequestDateTime { get; set; }
    }
}
