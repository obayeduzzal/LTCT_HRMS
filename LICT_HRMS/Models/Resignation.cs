using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{ 
    public class Resignation
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
         ApplyFormatInEditMode = true)]
        public DateTime ResignDate { get; set; }

        [Required(ErrorMessage = "Reason cannot be empty")]
        public string Reason { get; set; }

        public string Suggestion { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [ForeignKey("UpdateEmployee")]
        public int? UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }
        
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [DefaultValue(false)]
        public bool IsSeen { get; set; }

        public virtual Admin UpdateEmployee { get; set; }
        public virtual Employee Employee { get; set; }
    }
}