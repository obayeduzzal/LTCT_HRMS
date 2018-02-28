using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class PerformanceRating
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 5, ErrorMessage = "Can only be between 0 to 5")]
        public double Rating { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Performance Issue cannot be empty")]
        [ForeignKey("PerformanceIssue")]
        public int PerformanceIssueId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual PerformanceIssue PerformanceIssue { get; set; }
    }
}