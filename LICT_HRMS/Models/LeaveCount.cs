﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class LeaveCount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Leave Type cannot be empty")]
        [ForeignKey("LeaveType")]
        public int LeaveTypeId { get; set; }

        [Required(ErrorMessage = "Day cannot be empty")]
        public double AvailableDay { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}