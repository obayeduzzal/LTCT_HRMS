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
    public class ResignationsController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: Resignations
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                var resignation = db.Resignation.Include(r => r.Employee).Include(r => r.UpdateEmployee).Where(i => i.IsSeen == false);
                return View(resignation.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: Resignations/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Resignation resignation = db.Resignation.Find(id);
                if (resignation == null)
                {
                    return HttpNotFound();
                }
                return View(resignation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: Resignations/Create
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

        // POST: Resignations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId,IsSeen")] Resignation resignation)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    resignation.ResignDate = DateTime.Now;
                    resignation.CreateDate = DateTime.Now;
                    resignation.IsSeen = false;
                    db.Resignation.Add(resignation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                List<Employee> employee = new List<Employee>();
                employee = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employee, "Id", "Name");
                return View(resignation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: Resignations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Resignation resignation = db.Resignation.Find(id);
                if (resignation == null)
                {
                    return HttpNotFound();
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", resignation.EmployeeId);
                return View(resignation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Resignations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId,IsSeen")] Resignation resignation)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(resignation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", resignation.EmployeeId);
                return View(resignation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: Resignations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Resignation resignation = db.Resignation.Find(id);
                if (resignation == null)
                {
                    return HttpNotFound();
                }
                return View(resignation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Resignations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resignation resignation = db.Resignation.Find(id);
            db.Resignation.Remove(resignation);
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
