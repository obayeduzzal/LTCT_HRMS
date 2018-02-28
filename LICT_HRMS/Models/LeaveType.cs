using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Day cannot be empty")]
        public double Day { get; set; }

        [DefaultValue(true)]
        public bool IsEditable { get; set; }
    }
}