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
    public class DepartmentTransfersController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: DepartmentTransfers
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                var departmentTransfer = db.DepartmentTransfer.Include(d => d.Employee).Include(d => d.FromDesignation).Include(d => d.ToDesignation);
                return View(departmentTransfer.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: DepartmentTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
                if (departmentTransfer == null)
                {
                    return HttpNotFound();
                }
                return View(departmentTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: DepartmentTransfers/Create
        public ActionResult Create()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                List<Designation> designation = new List<Designation>();
                designation = db.Designation.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                ViewBag.ToDesignationId = new SelectList(designation, "Id", "Name");
                return View();
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DepartmentTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,FromDesignationId,ToDesignationId,TransferDate")] DepartmentTransfer departmentTransfer)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    int fromDesignationId = db.Employee.Where(i => i.Id == departmentTransfer.EmployeeId).Select(i => i.DesignationId).FirstOrDefault();
                    int toDesigantionId = Convert.ToInt32(Request["ToDesignationId"]);
                    departmentTransfer.FromDesignationId = fromDesignationId;
                    departmentTransfer.ToDesignationId = toDesigantionId;
                    departmentTransfer.TransferDate = DateTime.Now;
                    db.DepartmentTransfer.Add(departmentTransfer);
                    db.SaveChanges();

                    Employee emp = db.Employee.Find(departmentTransfer.EmployeeId);
                    emp.DesignationId = toDesigantionId;
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                List<Designation> designation = new List<Designation>();
                designation = db.Designation.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                ViewBag.ToDesignationId = new SelectList(designation, "Id", "Name");
                return View(departmentTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit

        // GET: DepartmentTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
                ViewBag.Designation = db.Designation.Where(x => x.Id == departmentTransfer.ToDesignationId).Select(t => t.Name).FirstOrDefault();
                int departmentId = db.Designation.Where(x => x.Id == departmentTransfer.ToDesignationId).Select(t => t.DepartmentId).FirstOrDefault();
                ViewBag.Department = db.Department.Where(x => x.Id == departmentId).Select(t => t.Name).FirstOrDefault();
                int departmentGroupId = db.Department.Where(x => x.Id == departmentId).Select(t => t.DepartmentGroupId).FirstOrDefault();
                ViewBag.DepartmentGroup = db.DepartmentGroup.Where(x => x.Id == departmentGroupId).Select(t => t.Name).FirstOrDefault();

                List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
                departmentGroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Id", "Name");
                if (departmentTransfer == null)
                {
                    return HttpNotFound();
                }
                return View(departmentTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            

        }

        // POST: DepartmentTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,FromDesignationId,ToDesignationId,TransferDate")] DepartmentTransfer departmentTransfer)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    int fromDesignationId = db.Employee.Where(i => i.Id == departmentTransfer.Id).Select(i => i.DesignationId).FirstOrDefault();
                    int toDesignationId = Convert.ToInt32(Request["ToDesignationId"]);
                    departmentTransfer.FromDesignationId = fromDesignationId;
                    departmentTransfer.ToDesignationId = toDesignationId;
                    departmentTransfer.TransferDate = DateTime.Now;
                    db.Entry(departmentTransfer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Code", departmentTransfer.EmployeeId); ;
                ViewBag.ToDesignationId = new SelectList(db.Designation, "Id", "Code", departmentTransfer.ToDesignationId);
                return View(departmentTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        // GET: DepartmentTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
                if (departmentTransfer == null)
                {
                    return HttpNotFound();
                }
                return View(departmentTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DepartmentTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
                db.DepartmentTransfer.Remove(departmentTransfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
