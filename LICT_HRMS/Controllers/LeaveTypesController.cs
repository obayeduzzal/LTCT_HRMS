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
    public class LeaveTypesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: LeaveTypes
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                return View(db.LeaveType.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: LeaveTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)

                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LeaveType leaveType = db.LeaveType.Find(id);
                if (leaveType == null)
                {
                    return HttpNotFound();
                }
                return View(leaveType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: LeaveTypes/Create
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

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Day,IsEditable")] LeaveType leaveType)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.LeaveType.Add(leaveType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(leaveType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: LeaveTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LeaveType leaveType = db.LeaveType.Find(id);
                if (leaveType == null)
                {
                    return HttpNotFound();
                }
                return View(leaveType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Day,IsEditable")] LeaveType leaveType)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(leaveType).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(leaveType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        #endregion

        #region Delete
        // GET: LeaveTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LeaveType leaveType = db.LeaveType.Find(id);
                if (leaveType == null)
                {
                    return HttpNotFound();
                }
                return View(leaveType);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveType leaveType = db.LeaveType.Find(id);
            db.LeaveType.Remove(leaveType);
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
