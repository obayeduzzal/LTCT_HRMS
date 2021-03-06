﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class LeaveHistory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Leave Type cannot be empty")]
        [ForeignKey("LeaveType")]
        public int LeaveTypeId { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Day cannot be empty")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Cause cannot be empty")]
        [MaxLength(250)]
        public string Cause { get; set; }

        [ForeignKey("UpdateEmployee")]
        public int? UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        public string Remarks { get; set; }

        [DefaultValue(false)]
        public bool IsSeen { get; set; }

        public virtual Admin UpdateEmployee { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}