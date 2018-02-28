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
    public class DisciplinaryActionTypesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: DisciplinaryActionTypes
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                return View(db.DisciplinaryActionTypes.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
           
        }
        #endregion

        #region Details
        // GET: DisciplinaryActionTypes/Details/5
        public ActionResult Details(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionTypes.Find(id);
                if (disciplinaryActionType == null)
                {
                    return HttpNotFound();
                }
                return View(disciplinaryActionType);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: DisciplinaryActionTypes/Create
        public ActionResult Create()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                return View();
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DisciplinaryActionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] DisciplinaryActionType disciplinaryActionType)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    db.DisciplinaryActionTypes.Add(disciplinaryActionType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(disciplinaryActionType);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: DisciplinaryActionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionTypes.Find(id);
                if (disciplinaryActionType == null)
                {
                    return HttpNotFound();
                }
                return View(disciplinaryActionType);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DisciplinaryActionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] DisciplinaryActionType disciplinaryActionType)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    db.Entry(disciplinaryActionType).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(disciplinaryActionType);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: DisciplinaryActionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionTypes.Find(id);
                if (disciplinaryActionType == null)
                {
                    return HttpNotFound();
                }
                return View(disciplinaryActionType);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DisciplinaryActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionTypes.Find(id);
                db.DisciplinaryActionTypes.Remove(disciplinaryActionType);
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
