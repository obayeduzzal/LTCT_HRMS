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
    public class EmployeeTypesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: EmployeeTypes
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                return View(db.EmployeeType.Where(i => i.Status == true));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: EmployeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeType employeeType = db.EmployeeType.Find(id);
                if (employeeType == null)
                {
                    return HttpNotFound();
                }
                return View(employeeType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: EmployeeTypes/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        // POST: EmployeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Status")] EmployeeType employeeType)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    employeeType.Status = true;
                    db.EmployeeType.Add(employeeType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(employeeType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: EmployeeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeType employeeType = db.EmployeeType.Find(id);
                if (employeeType == null)
                {
                    return HttpNotFound();
                }
                return View(employeeType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: EmployeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status")] EmployeeType employeeType)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employeeType).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(employeeType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: EmployeeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeType employeeType = db.EmployeeType.Find(id);
                if (employeeType == null)
                {
                    return HttpNotFound();
                }
                return View(employeeType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: EmployeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                EmployeeType employeeType = db.EmployeeType.Find(id);
                employeeType.Status = false;
                db.Entry(employeeType).State = EntityState.Modified;
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
