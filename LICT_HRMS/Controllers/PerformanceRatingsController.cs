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
    public class PerformanceRatingsController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: PerformanceRatings
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                var performanceRating = db.PerformanceRating.Include(p => p.Employee).Include(p => p.PerformanceIssue);
                return View(performanceRating.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: PerformanceRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PerformanceRating performanceRating = db.PerformanceRating.Find(id);
                if (performanceRating == null)
                {
                    return HttpNotFound();
                }
                return View(performanceRating);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: PerformanceRatings/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        

        // POST: PerformanceRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    performanceRating.Date = DateTime.Now;
                    db.PerformanceRating.Add(performanceRating);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Id", "Name");
                return View(performanceRating);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: PerformanceRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PerformanceRating performanceRating = db.PerformanceRating.Find(id);
                if (performanceRating == null)
                {
                    return HttpNotFound();
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", performanceRating.EmployeeId);
                ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Id", "Name", performanceRating.PerformanceIssueId);
                return View(performanceRating);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: PerformanceRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(performanceRating).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", performanceRating.EmployeeId);
                ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Id", "Name", performanceRating.PerformanceIssueId);
                return View(performanceRating);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: PerformanceRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PerformanceRating performanceRating = db.PerformanceRating.Find(id);
                if (performanceRating == null)
                {
                    return HttpNotFound();
                }
                return View(performanceRating);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: PerformanceRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                PerformanceRating performanceRating = db.PerformanceRating.Find(id);
                db.PerformanceRating.Remove(performanceRating);
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
