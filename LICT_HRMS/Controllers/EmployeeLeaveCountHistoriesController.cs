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
    public class EmployeeLeaveCountHistoriesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: EmployeeLeaveCountHistories
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                var employeeLeaveCountHistory = db.EmployeeLeaveCountHistory.Include(e => e.Employee);
                return View(employeeLeaveCountHistory.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: EmployeeLeaveCountHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeLeaveCountHistory employeeLeaveCountHistory = db.EmployeeLeaveCountHistory.Find(id);
                if (employeeLeaveCountHistory == null)
                {
                    return HttpNotFound();
                }
                return View(employeeLeaveCountHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: EmployeeLeaveCountHistories/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: EmployeeLeaveCountHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,EarnLeaveDays,WithoutPayLeaveDays")] EmployeeLeaveCountHistory employeeLeaveCountHistory)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.EmployeeLeaveCountHistory.Add(employeeLeaveCountHistory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                return View(employeeLeaveCountHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: EmployeeLeaveCountHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeLeaveCountHistory employeeLeaveCountHistory = db.EmployeeLeaveCountHistory.Find(id);
                if (employeeLeaveCountHistory == null)
                {
                    return HttpNotFound();
                }
                List<Employee> employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                return View(employeeLeaveCountHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        // POST: EmployeeLeaveCountHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,EarnLeaveDays,WithoutPayLeaveDays")] EmployeeLeaveCountHistory employeeLeaveCountHistory)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employeeLeaveCountHistory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<Employee> employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                return View(employeeLeaveCountHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: EmployeeLeaveCountHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeLeaveCountHistory employeeLeaveCountHistory = db.EmployeeLeaveCountHistory.Find(id);
                if (employeeLeaveCountHistory == null)
                {
                    return HttpNotFound();
                }
                return View(employeeLeaveCountHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: EmployeeLeaveCountHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                EmployeeLeaveCountHistory employeeLeaveCountHistory = db.EmployeeLeaveCountHistory.Find(id);
                db.EmployeeLeaveCountHistory.Remove(employeeLeaveCountHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
