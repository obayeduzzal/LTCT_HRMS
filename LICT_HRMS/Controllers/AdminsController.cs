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
    public class AdminsController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: Admins
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                return View(db.Admin.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        #endregion

        #region Details
        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admin.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: Admins/Create
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

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,FathersName,MothersName,Gender,PresentAddress,PermanentAddress,Mobile,Email,Password")] Admin admin)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    db.Admin.Add(admin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(admin);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion

        #region Edit
        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admin.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }


        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,FathersName,MothersName,Gender,PresentAddress,PermanentAddress,Mobile,Email,Password")] Admin admin)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(admin);
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
            }
            

        #endregion

        #region Delete
        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admin.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
           
            
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                Admin admin = db.Admin.Find(id);
                db.Admin.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Login", "Account");
            } 
        }
        #endregion

        #region Logout
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
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
