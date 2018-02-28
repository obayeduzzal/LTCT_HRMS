namespace LICT_HRMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 15),
                        Name = c.String(nullable: false, maxLength: 250),
                        FathersName = c.String(nullable: false, maxLength: 250),
                        MothersName = c.String(nullable: false, maxLength: 250),
                        Gender = c.String(nullable: false, maxLength: 250),
                        PresentAddress = c.String(nullable: false, maxLength: 250),
                        PermanentAddress = c.String(nullable: false, maxLength: 250),
                        Mobile = c.String(nullable: false, maxLength: 450),
                        Email = c.String(maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstituteName = c.String(nullable: false, maxLength: 250),
                        InstituteAddress = c.String(nullable: false, maxLength: 250),
                        Website = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 450),
                        Designation = c.String(nullable: false, maxLength: 250),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 15),
                        Name = c.String(nullable: false, maxLength: 250),
                        FathersName = c.String(nullable: false, maxLength: 250),
                        MothersName = c.String(nullable: false, maxLength: 250),
                        Gender = c.String(nullable: false, maxLength: 250),
                        PresentAddress = c.String(nullable: false, maxLength: 250),
                        PermanentAddress = c.String(nullable: false, maxLength: 250),
                        Mobile = c.String(nullable: false, maxLength: 450),
                        Email = c.String(maxLength: 250),
                        NIDorBirthCirtificate = c.String(nullable: false, maxLength: 250),
                        DrivingLicence = c.String(maxLength: 250),
                        PassportNumber = c.String(maxLength: 250),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateOfJoining = c.DateTime(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        EmployeeTypeId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        GrossSalary = c.Double(nullable: false),
                        CreatedBy = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        IsSystemOrSuperAdmin = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        ProbationStatus = c.Boolean(nullable: false),
                        IsSpecialEmployee = c.Boolean(nullable: false),
                        ParmanentDate = c.DateTime(),
                        EmergencyMobile = c.String(maxLength: 450),
                        RelationEmergencyMobile = c.String(maxLength: 450),
                        BloodGroup = c.String(),
                        MedicalHistory = c.String(),
                        Height = c.String(),
                        Weight = c.String(),
                        ExtraCurricularActivities = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.CreatedBy)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeTypeId, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.DesignationId)
                .Index(t => t.EmployeeTypeId)
                .Index(t => t.BranchId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false),
                        OpeningTime = c.DateTime(nullable: false),
                        EndingTime = c.DateTime(nullable: false),
                        IsLateCalculated = c.Boolean(nullable: false),
                        LateConsiderationTime = c.Double(),
                        LateConsiderationDay = c.Double(),
                        LateDeductionPercentage = c.Double(),
                        IsOvertimeCalculated = c.Boolean(nullable: false),
                        OvertimeConsiderationTime = c.Double(),
                        OvertimePaymentPercentage = c.Double(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        DepartmentId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                        CreatedBy = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.CreatedBy)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.UpdatedBy)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        DepartmentGroupId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.CreatedBy)
                .ForeignKey("dbo.DepartmentGroups", t => t.DepartmentGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.UpdatedBy)
                .Index(t => t.DepartmentGroupId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.DepartmentGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.CreatedBy)
                .ForeignKey("dbo.Admins", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstituteName = c.String(nullable: false, maxLength: 250),
                        Program = c.String(nullable: false, maxLength: 250),
                        Board = c.String(maxLength: 250),
                        Result = c.String(nullable: false, maxLength: 250),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.BranchTransfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromBranchId = c.Int(nullable: false),
                        ToBranchId = c.Int(nullable: false),
                        TransferDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Branches", t => t.FromBranchId, cascadeDelete: false)
                .ForeignKey("dbo.Branches", t => t.ToBranchId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.FromBranchId)
                .Index(t => t.ToBranchId);
            
            CreateTable(
                "dbo.DepartmentTransfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromDesignationId = c.Int(nullable: false),
                        ToDesignationId = c.Int(nullable: false),
                        TransferDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.FromDesignationId, cascadeDelete: false)
                .ForeignKey("dbo.Designations", t => t.ToDesignationId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.FromDesignationId)
                .Index(t => t.ToDesignationId);
            
            CreateTable(
                "dbo.DisciplinaryActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DisciplinaryActionTypeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DisciplinaryActionTypes", t => t.DisciplinaryActionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.DisciplinaryActionTypeId);
            
            CreateTable(
                "dbo.DisciplinaryActionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeLeaveCountHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        EarnLeaveDays = c.Double(nullable: false),
                        WithoutPayLeaveDays = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.LeaveCounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeaveTypeId = c.Int(nullable: false),
                        AvailableDay = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.LeaveTypeId);
            
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Day = c.Double(nullable: false),
                        IsEditable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeaveHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeaveTypeId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Day = c.Int(nullable: false),
                        Cause = c.String(nullable: false, maxLength: 250),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        Remarks = c.String(),
                        IsSeen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.UpdatedBy)
                .Index(t => t.EmployeeId)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.PerformanceIssues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PerformanceRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        PerformanceIssueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.PerformanceIssues", t => t.PerformanceIssueId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.PerformanceIssueId);
            
            CreateTable(
                "dbo.PromotionHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromDesignationId = c.Int(nullable: false),
                        ToDesignationId = c.Int(nullable: false),
                        PromotionDate = c.DateTime(nullable: false),
                        FromSalary = c.Double(nullable: false),
                        ToSalary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.FromDesignationId, cascadeDelete: false)
                .ForeignKey("dbo.Designations", t => t.ToDesignationId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.FromDesignationId)
                .Index(t => t.ToDesignationId);
            
            CreateTable(
                "dbo.Resignations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResignDate = c.DateTime(nullable: false),
                        Reason = c.String(nullable: false),
                        Suggestion = c.String(),
                        Status = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        IsSeen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.UpdatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resignations", "UpdatedBy", "dbo.Admins");
            DropForeignKey("dbo.Resignations", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PromotionHistories", "ToDesignationId", "dbo.Designations");
            DropForeignKey("dbo.PromotionHistories", "FromDesignationId", "dbo.Designations");
            DropForeignKey("dbo.PromotionHistories", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PerformanceRatings", "PerformanceIssueId", "dbo.PerformanceIssues");
            DropForeignKey("dbo.PerformanceRatings", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.LeaveHistories", "UpdatedBy", "dbo.Admins");
            DropForeignKey("dbo.LeaveHistories", "LeaveTypeId", "dbo.LeaveTypes");
            DropForeignKey("dbo.LeaveHistories", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.LeaveCounts", "LeaveTypeId", "dbo.LeaveTypes");
            DropForeignKey("dbo.LeaveCounts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeLeaveCountHistories", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DisciplinaryActions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DisciplinaryActions", "DisciplinaryActionTypeId", "dbo.DisciplinaryActionTypes");
            DropForeignKey("dbo.DepartmentTransfers", "ToDesignationId", "dbo.Designations");
            DropForeignKey("dbo.DepartmentTransfers", "FromDesignationId", "dbo.Designations");
            DropForeignKey("dbo.DepartmentTransfers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.BranchTransfers", "ToBranchId", "dbo.Branches");
            DropForeignKey("dbo.BranchTransfers", "FromBranchId", "dbo.Branches");
            DropForeignKey("dbo.BranchTransfers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Experiences", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Images", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropForeignKey("dbo.Educations", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Designations", "UpdatedBy", "dbo.Admins");
            DropForeignKey("dbo.Designations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "UpdatedBy", "dbo.Admins");
            DropForeignKey("dbo.Departments", "DepartmentGroupId", "dbo.DepartmentGroups");
            DropForeignKey("dbo.DepartmentGroups", "UpdatedBy", "dbo.Admins");
            DropForeignKey("dbo.DepartmentGroups", "CreatedBy", "dbo.Admins");
            DropForeignKey("dbo.Departments", "CreatedBy", "dbo.Admins");
            DropForeignKey("dbo.Designations", "CreatedBy", "dbo.Admins");
            DropForeignKey("dbo.Employees", "CreatedBy", "dbo.Admins");
            DropForeignKey("dbo.Employees", "BranchId", "dbo.Branches");
            DropIndex("dbo.Resignations", new[] { "EmployeeId" });
            DropIndex("dbo.Resignations", new[] { "UpdatedBy" });
            DropIndex("dbo.PromotionHistories", new[] { "ToDesignationId" });
            DropIndex("dbo.PromotionHistories", new[] { "FromDesignationId" });
            DropIndex("dbo.PromotionHistories", new[] { "EmployeeId" });
            DropIndex("dbo.PerformanceRatings", new[] { "PerformanceIssueId" });
            DropIndex("dbo.PerformanceRatings", new[] { "EmployeeId" });
            DropIndex("dbo.LeaveHistories", new[] { "UpdatedBy" });
            DropIndex("dbo.LeaveHistories", new[] { "LeaveTypeId" });
            DropIndex("dbo.LeaveHistories", new[] { "EmployeeId" });
            DropIndex("dbo.LeaveCounts", new[] { "LeaveTypeId" });
            DropIndex("dbo.LeaveCounts", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeLeaveCountHistories", new[] { "EmployeeId" });
            DropIndex("dbo.DisciplinaryActions", new[] { "DisciplinaryActionTypeId" });
            DropIndex("dbo.DisciplinaryActions", new[] { "EmployeeId" });
            DropIndex("dbo.DepartmentTransfers", new[] { "ToDesignationId" });
            DropIndex("dbo.DepartmentTransfers", new[] { "FromDesignationId" });
            DropIndex("dbo.DepartmentTransfers", new[] { "EmployeeId" });
            DropIndex("dbo.BranchTransfers", new[] { "ToBranchId" });
            DropIndex("dbo.BranchTransfers", new[] { "FromBranchId" });
            DropIndex("dbo.BranchTransfers", new[] { "EmployeeId" });
            DropIndex("dbo.Images", new[] { "EmployeeId" });
            DropIndex("dbo.Educations", new[] { "EmployeeId" });
            DropIndex("dbo.DepartmentGroups", new[] { "UpdatedBy" });
            DropIndex("dbo.DepartmentGroups", new[] { "CreatedBy" });
            DropIndex("dbo.Departments", new[] { "UpdatedBy" });
            DropIndex("dbo.Departments", new[] { "CreatedBy" });
            DropIndex("dbo.Departments", new[] { "DepartmentGroupId" });
            DropIndex("dbo.Designations", new[] { "UpdatedBy" });
            DropIndex("dbo.Designations", new[] { "CreatedBy" });
            DropIndex("dbo.Designations", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "CreatedBy" });
            DropIndex("dbo.Employees", new[] { "BranchId" });
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            DropIndex("dbo.Employees", new[] { "DesignationId" });
            DropIndex("dbo.Employees", new[] { "Code" });
            DropIndex("dbo.Experiences", new[] { "EmployeeId" });
            DropIndex("dbo.Admins", new[] { "Code" });
            DropTable("dbo.Resignations");
            DropTable("dbo.PromotionHistories");
            DropTable("dbo.PerformanceRatings");
            DropTable("dbo.PerformanceIssues");
            DropTable("dbo.LeaveHistories");
            DropTable("dbo.LeaveTypes");
            DropTable("dbo.LeaveCounts");
            DropTable("dbo.EmployeeLeaveCountHistories");
            DropTable("dbo.DisciplinaryActionTypes");
            DropTable("dbo.DisciplinaryActions");
            DropTable("dbo.DepartmentTransfers");
            DropTable("dbo.BranchTransfers");
            DropTable("dbo.Images");
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.Educations");
            DropTable("dbo.DepartmentGroups");
            DropTable("dbo.Departments");
            DropTable("dbo.Designations");
            DropTable("dbo.Branches");
            DropTable("dbo.Employees");
            DropTable("dbo.Experiences");
            DropTable("dbo.Admins");
        }
    }
}
