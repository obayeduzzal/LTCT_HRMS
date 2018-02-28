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
    public class LeaveHistoriesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: LeaveHistories
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                var leaveHistory = db.LeaveHistory.Include(l => l.Employee).Include(l => l.LeaveType).Include(l => l.UpdateEmployee).Where(i => i.Status == true).ToList();
                return View(leaveHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: LeaveHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LeaveHistory leaveHistory = db.LeaveHistory.Find(id);
                if (leaveHistory == null)
                {
                    return HttpNotFound();
                }
                return View(leaveHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: LeaveHistories/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                ViewBag.LeaveTypeId = new SelectList(db.LeaveType, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: LeaveHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,LeaveTypeId,CreateDate,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status,Remarks,IsSeen")] LeaveHistory leaveHistory)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    leaveHistory.CreateDate = DateTime.Now;
                    leaveHistory.Status = true;
                    db.LeaveHistory.Add(leaveHistory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                ViewBag.LeaveTypeId = new SelectList(db.LeaveType, "Id", "Name");
                return View(leaveHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: LeaveHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LeaveHistory leaveHistory = db.LeaveHistory.Find(id);
                if (leaveHistory == null)
                {
                    return HttpNotFound();
                }
                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.LeaveTypeId = new SelectList(db.LeaveType, "Id", "Name");
                ViewBag.UpdatedBy = new SelectList(employee, "Id", "Name");
                return View(leaveHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        // POST: LeaveHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,LeaveTypeId,CreateDate,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status,Remarks,IsSeen")] LeaveHistory leaveHistory)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    leaveHistory.UpdatedBy = Convert.ToInt32(Session["ADMINID"]);
                    leaveHistory.UpdateDate = DateTime.Now;
                    db.Entry(leaveHistory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.LeaveTypeId = new SelectList(db.LeaveType, "Id", "Name");
                ViewBag.UpdatedBy = new SelectList(employee, "Id", "Name");
                return View(leaveHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        #endregion

        #region Delete
        // GET: LeaveHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LeaveHistory leaveHistory = db.LeaveHistory.Find(id);
                if (leaveHistory == null)
                {
                    return HttpNotFound();
                }
                return View(leaveHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
              
        }

        // POST: LeaveHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveHistory leaveHistory = db.LeaveHistory.Find(id);
            leaveHistory.Status = false;
            db.Entry(leaveHistory).State = EntityState.Modified;
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
