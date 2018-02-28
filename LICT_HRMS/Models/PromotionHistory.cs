using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class PromotionHistory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "From Designation cannot be empty")]
        [ForeignKey("FromDesignation")]
        public int FromDesignationId { get; set; }

        [Required(ErrorMessage = "To Designation cannot be empty")]
        [ForeignKey("ToDesignation")]
        public int ToDesignationId { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
         ApplyFormatInEditMode = true)]
        public DateTime PromotionDate { get; set; }

        public double FromSalary { get; set; }
        public double ToSalary { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Designation FromDesignation { get; set; }
        public virtual Designation ToDesignation { get; set; }

    }
}