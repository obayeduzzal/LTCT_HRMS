using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class EmployeeType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [DefaultValue(true)]
        public bool Status { get; set; }
    }
}