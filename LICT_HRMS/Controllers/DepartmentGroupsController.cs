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
    public class DepartmentGroupsController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();
        #region List
        // GET: DepartmentGroups
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                var departmentGroup = db.DepartmentGroup.Include(d => d.CreateEmployee).Include(d => d.UpdateEmployee).Where(i => i.Status == true);
                return View(departmentGroup.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: DepartmentGroups/Details/5
        public ActionResult Details(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
                if (departmentGroup == null)
                {
                    return HttpNotFound();
                }
                return View(departmentGroup);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: DepartmentGroups/Create
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

        // POST: DepartmentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] DepartmentGroup departmentGroup)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (db.DepartmentGroup.Where(i => i.Code == departmentGroup.Code).ToList().Count < 1)
                {
                    if (db.DepartmentGroup.Where(i => i.Name == departmentGroup.Name).ToList().Count < 1)
                    {
                        if (ModelState.IsValid)
                        {
                            departmentGroup.CreatedBy = Convert.ToInt32(Session["ADMINID"]);
                            departmentGroup.CreateDate = DateTime.Now;
                            departmentGroup.Status = true;
                            db.DepartmentGroup.Add(departmentGroup);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["Messsage"] = "Name Exist!";
                    }
                }
                else
                {
                    TempData["Message"] = "Code Exist!";
                }
                return View(departmentGroup);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Edit

        // GET: DepartmentGroups/Edit/5
        public ActionResult Edit(int? id)
        {
           if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
                if (departmentGroup == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CreatedBy = new SelectList(db.Employee, "Id", "Code", departmentGroup.CreatedBy);
                ViewBag.UpdatedBy = new SelectList(db.Employee, "Id", "Code", departmentGroup.UpdatedBy);
                return View(departmentGroup);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DepartmentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] DepartmentGroup departmentGroup)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                departmentGroup.UpdatedBy = Convert.ToInt32(Session["ADMINID"]);
                departmentGroup.UpdateDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Entry(departmentGroup).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CreatedBy = new SelectList(db.Employee, "Id", "Code", departmentGroup.CreatedBy);
                ViewBag.UpdatedBy = new SelectList(db.Employee, "Id", "Code", departmentGroup.UpdatedBy);
                return View(departmentGroup);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        #endregion

        #region Delete


        // GET: DepartmentGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
                if (departmentGroup == null)
                {
                    return HttpNotFound();
                }
                return View(departmentGroup);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: DepartmentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
                departmentGroup.Status = false;
                db.Entry(departmentGroup).State = EntityState.Modified;
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
