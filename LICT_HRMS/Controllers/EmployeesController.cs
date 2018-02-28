using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LICT_HRMS.Models;

namespace LICT_HRMS.Controllers
{
    public class EmployeesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: Employees
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                var employee = db.Employee.Include(e => e.Branch).Include(e => e.CreateEmployee).Include(e => e.Designation).Include(e => e.EmployeeType).Where(i => i.Status == true);
                return View(employee.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        #endregion

        #region Details
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employee.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: Employees/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                List<Branch> branch = new List<Branch>();
                branch = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(branch, "Id", "Name");
                List<Designation> designation = new List<Designation>();
                designation = db.Designation.Where(i => i.Status == true).ToList();
                ViewBag.DesignationId = new SelectList(designation, "Id", "Name");
                List<EmployeeType> employeeType = new List<EmployeeType>();
                employeeType = db.EmployeeType.Where(i => i.Status).ToList();
                ViewBag.EmployeeTypeId = new SelectList(employeeType, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,FathersName,MothersName,Gender,PresentAddress,PermanentAddress,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,IsSystemOrSuperAdmin,Status,ProbationStatus,IsSpecialEmployee,ParmanentDate,EmergencyMobile,RelationEmergencyMobile,BloodGroup,MedicalHistory,Height,Weight,ExtraCurricularActivities")] Employee employee)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                employee.CreateDate = DateTime.Now;
                employee.CreatedBy = Convert.ToInt32(Session["ADMINID"]);
                employee.Status = true;
                if (ModelState.IsValid)
                {
                    db.Employee.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                List<Branch> branch = new List<Branch>();
                branch = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(branch, "Id", "Name");
                List<Designation> designation = new List<Designation>();
                designation = db.Designation.Where(i => i.Status == true).ToList();
                ViewBag.DesignationId = new SelectList(designation, "Id", "Name");
                List<EmployeeType> employeeType = new List<EmployeeType>();
                employeeType = db.EmployeeType.Where(i => i.Status).ToList();
                ViewBag.EmployeeTypeId = new SelectList(employeeType, "Id", "Name");
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employee.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                List<Branch> branch = new List<Branch>();
                branch = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(branch, "Id", "Name", employee.BranchId);
                List<Designation> designation = new List<Designation>();
                designation = db.Designation.Where(i => i.Status == true).ToList();
                ViewBag.DesignationId = new SelectList(designation, "Id", "Name");
                List<EmployeeType> employeeType = new List<EmployeeType>();
                employeeType = db.EmployeeType.Where(i => i.Status).ToList();
                ViewBag.EmployeeTypeId = new SelectList(employeeType, "Id", "Name");
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,FathersName,MothersName,Gender,PresentAddress,PermanentAddress,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,IsSystemOrSuperAdmin,Status,ProbationStatus,IsSpecialEmployee,ParmanentDate,EmergencyMobile,RelationEmergencyMobile,BloodGroup,MedicalHistory,Height,Weight,ExtraCurricularActivities")] Employee employee)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                employee.UpdateDate = DateTime.Now;
                employee.UpdatedBy = Convert.ToInt32(Session["ADMINID"]);
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<Branch> branch = new List<Branch>();
                branch = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(branch, "Id", "Name", employee.BranchId);
                List<Designation> designation = new List<Designation>();
                designation = db.Designation.Where(i => i.Status == true).ToList();
                ViewBag.DesignationId = new SelectList(designation, "Id", "Name");
                List<EmployeeType> employeeType = new List<EmployeeType>();
                employeeType = db.EmployeeType.Where(i => i.Status).ToList();
                ViewBag.EmployeeTypeId = new SelectList(employeeType, "Id", "Name");
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employee.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            employee.Status = false;
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
