using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LICT_HRMS.Models
{
    public class LICT_HRMSDbContext : DbContext
    {
        public LICT_HRMSDbContext():base("LICT_HRMS")
        {
            Database.SetInitializer<LICT_HRMSDbContext>(null);
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Branch> Branch { get; set; }
        public DbSet<BranchTransfer> BranchTransfer { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentGroup> DepartmentGroup { get; set; }
        public DbSet<DepartmentTransfer> DepartmentTransfer { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<DisciplinaryAction> DisciplinaryAction { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeLeaveCountHistory> EmployeeLeaveCountHistory { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
        public DbSet<Experience> BrancExperienceh { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<LeaveCount> LeaveCount { get; set; }
        public DbSet<LeaveHistory> LeaveHistory { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<PerformanceIssue> PerformanceIssue { get; set; }
        public DbSet<PerformanceRating> PerformanceRating { get; set; }
        public DbSet<PromotionHistory> PromotionHistory { get; set; }
        public DbSet<Resignation> Resignation { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<DisciplinaryActionType> DisciplinaryActionTypes { get; set; }
    }
}