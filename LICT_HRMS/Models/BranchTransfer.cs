using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LICT_HRMS.Models
{
    public class BranchTransfer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "From Branch cannot be empty")]
        [ForeignKey("FromBranch")]
        public int FromBranchId { get; set; }

        [Required(ErrorMessage = "To Branch cannot be empty")]
        [ForeignKey("ToBranch")]
        public int ToBranchId { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}",
         ApplyFormatInEditMode = true)]
        public DateTime TransferDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Branch FromBranch { get; set; }
        public virtual Branch ToBranch { get; set; }
    }
}