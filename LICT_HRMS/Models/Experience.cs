﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Institute Name cannot be empty")]
        [MaxLength(250)]
        public string InstituteName { get; set; }

        [Required(ErrorMessage = "Institute Address cannot be empty")]
        [MaxLength(250)]
        public string InstituteAddress { get; set; }

        [MaxLength(250)]
        public string Website { get; set; }

        [MaxLength(450)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Designation cannot be empty")]
        [MaxLength(250)]
        public string Designation { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
        ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}