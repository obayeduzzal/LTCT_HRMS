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
    public class DisciplinaryActionsController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: DisciplinaryActions
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                var disciplinaryAction = db.DisciplinaryAction.Include(d => d.DisciplinaryActionType).Include(d => d.Employee);
                return View(disciplinaryAction.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: DisciplinaryActions/Details/5
        public ActionResult Details(int? id)
        {
           if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
                if (disciplinaryAction == null)
                {
                    return HttpNotFound();
                }
                return View(disciplinaryAction);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: DisciplinaryActions/Create
        public ActionResult Create()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionTypes, "Id", "Name");
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name");
                return View();
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DisciplinaryActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
           if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    disciplinaryAction.Date = DateTime.Now;
                    db.DisciplinaryAction.Add(disciplinaryAction);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionTypes, "Id", "Name", disciplinaryAction.DisciplinaryActionTypeId);
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", disciplinaryAction.EmployeeId);
                return View(disciplinaryAction);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: DisciplinaryActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
                if (disciplinaryAction == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionTypes, "Id", "Name", disciplinaryAction.DisciplinaryActionTypeId);
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", disciplinaryAction.EmployeeId);
                return View(disciplinaryAction);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
           
            
        }

        // POST: DisciplinaryActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
           if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    disciplinaryAction.Date = DateTime.Now;
                    db.Entry(disciplinaryAction).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionTypes, "Id", "Name", disciplinaryAction.DisciplinaryActionTypeId);
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", disciplinaryAction.EmployeeId);
                return View(disciplinaryAction);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: DisciplinaryActions/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
                if (disciplinaryAction == null)
                {
                    return HttpNotFound();
                }
                return View(disciplinaryAction);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DisciplinaryActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
                db.DisciplinaryAction.Remove(disciplinaryAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else{
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
