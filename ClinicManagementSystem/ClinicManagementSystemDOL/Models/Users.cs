using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystemDOL.Models
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        [Display(Name ="First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Special character should not be entered")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Special character should not be entered")]
        public string LastName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(60)]
        [Required]
        [Index(IsUnique =true)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [MinLength(0,ErrorMessage ="Enter a minimum number of characters")]
        public string Address { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Required(ErrorMessage = "Telephone Number Required")]
        [Index(IsUnique = true)]
        public string Phone { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [Display(Name ="Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int CreatedBy { get; set; }

        public UserRoles UserRoles { get; set; }
        [ForeignKey("UserRoles"), Column(Order = 0)]
        public int RoleId { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeletedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedAt { get; set; }
    }
}
