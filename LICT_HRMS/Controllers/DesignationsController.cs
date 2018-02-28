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
    public class DesignationsController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();
        #region List
        // GET: Designations
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                var designation = db.Designation.Include(d => d.CreateEmployee).Include(d => d.Department).Include(d => d.UpdateEmployee).Where(i => i.Status == true);
                return View(designation.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: Designations/Details/5
        public ActionResult Details(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Designation designation = db.Designation.Find(id);
                if (designation == null)
                {
                    return HttpNotFound();
                }
                return View(designation);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: Designations/Create
        public ActionResult Create()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                List<Department> department = new List<Department>();
                department = db.Department.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentId = new SelectList(department, "Id", "Name");
                return View();
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,DepartmentId,RoleName,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    designation.CreatedBy = Convert.ToInt32(Session["ADMINID"]);
                    designation.CreateDate = DateTime.Now;
                    designation.Status = true;
                    db.Designation.Add(designation);
                    db.SaveChanges();
                    TempData["Success"] = "Success";
                    return RedirectToAction("Index");
                }

                List<Department> department = new List<Department>();
                department = db.Department.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentId = new SelectList(department, "Id", "Name");
                return View(designation);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit
        // GET: Designations/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Designation designation = db.Designation.Find(id);
                if (designation == null)
                {
                    return HttpNotFound();
                }
                List<Department> department = new List<Department>();
                department = db.Department.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentId = new SelectList(department, "Id", "Bame", designation.DepartmentId);
                return View(designation);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,DepartmentId,RoleName,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    designation.UpdatedBy = Convert.ToInt32(Session["ADMINID"]);
                    designation.UpdateDate = DateTime.Now;
                    db.Entry(designation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<Department> department = new List<Department>();
                department = db.Department.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentId = new SelectList(department, "Id", "Name", designation.DepartmentId);
                return View(designation);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: Designations/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Designation designation = db.Designation.Find(id);
                if (designation == null)
                {
                    return HttpNotFound();
                }
                return View(designation);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                Designation designation = db.Designation.Find(id);
                designation.Status = false;
                db.Entry(designation).State = EntityState.Modified;
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
