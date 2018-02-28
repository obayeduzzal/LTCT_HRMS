using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LICT_HRMS.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        [Required(ErrorMessage = "Code can not be empty")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "only alphabets and digits")]
        [Index(IsUnique = true)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Father's Name cannot be empty")]
        [MaxLength(250)]
        public string FathersName { get; set; }

        [Required(ErrorMessage = "Mother's Name cannot be empty")]
        [MaxLength(250)]
        public string MothersName { get; set; }

        [Required(ErrorMessage = "Gender cannot be empty")]
        [MaxLength(250)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Present Address cannot be empty")]
        [MaxLength(250)]
        public string PresentAddress { get; set; }

        [Required(ErrorMessage = "Permanent Address cannot be empty")]
        [MaxLength(250)]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Mobile cannot be empty")]
        [StringLength(450)]
        public string Mobile { get; set; }

        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Invalid Email")]
        [StringLength(450)]
        [MaxLength(250)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}