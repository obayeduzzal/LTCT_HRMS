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
    public class PromotionHistoriesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: PromotionHistories
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                var promotionHistory = db.PromotionHistory.Include(p => p.Employee).Include(p => p.FromDesignation).Include(p => p.ToDesignation);
                return View(promotionHistory.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: PromotionHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PromotionHistory promotionHistory = db.PromotionHistory.Find(id);
                if (promotionHistory == null)
                {
                    return HttpNotFound();
                }
                return View(promotionHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: PromotionHistories/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                List<Designation> designation = new List<Designation>();
                designation = db.Designation.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                ViewBag.ToDesignationId = new SelectList(designation, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: PromotionHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,FromDesignationId,ToDesignationId,PromotionDate,FromSalary,ToSalary")] PromotionHistory promotionHistory)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    int fromDesignationId = db.Employee.Where(i => i.Id == promotionHistory.EmployeeId).Select(i => i.DesignationId).FirstOrDefault();
                    int toDesigantionId = Convert.ToInt32(Request["ToDesignationId"]);
                    promotionHistory.FromDesignationId = fromDesignationId;
                    promotionHistory.ToDesignationId = toDesigantionId;
                    promotionHistory.PromotionDate = DateTime.Now;
                    db.PromotionHistory.Add(promotionHistory);
                    db.SaveChanges();

                    Employee emp = db.Employee.Find(promotionHistory.EmployeeId);
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
                return View(promotionHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: PromotionHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PromotionHistory promotionHistory = db.PromotionHistory.Find(id);
                if (promotionHistory == null)
                {
                    return HttpNotFound();
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", promotionHistory.EmployeeId);
                ViewBag.FromDesignationId = new SelectList(db.Designation, "Id", "Name", promotionHistory.FromDesignationId);
                ViewBag.ToDesignationId = new SelectList(db.Designation, "Id", "Name", promotionHistory.ToDesignationId);
                return View(promotionHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: PromotionHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,FromDesignationId,ToDesignationId,PromotionDate,FromSalary,ToSalary")] PromotionHistory promotionHistory)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(promotionHistory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", promotionHistory.EmployeeId);
                ViewBag.FromDesignationId = new SelectList(db.Designation, "Id", "Name", promotionHistory.FromDesignationId);
                ViewBag.ToDesignationId = new SelectList(db.Designation, "Id", "Name", promotionHistory.ToDesignationId);
                return View(promotionHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: PromotionHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PromotionHistory promotionHistory = db.PromotionHistory.Find(id);
                if (promotionHistory == null)
                {
                    return HttpNotFound();
                }
                return View(promotionHistory);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: PromotionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                PromotionHistory promotionHistory = db.PromotionHistory.Find(id);
                db.PromotionHistory.Remove(promotionHistory);
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
