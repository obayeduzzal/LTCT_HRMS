using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class EmployeeLeaveCountHistory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Earn Leave Days cannot be empty")]
        public double EarnLeaveDays { get; set; }

        [Required(ErrorMessage = "Without Pay Leave Days cannot be empty")]
        public double WithoutPayLeaveDays { get; set; }

        public virtual Employee Employee { get; set; }

    }
}