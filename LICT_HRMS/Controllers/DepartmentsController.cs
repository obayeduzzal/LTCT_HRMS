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
    public class DepartmentsController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: Departments
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                var department = db.Department.Include(d => d.CreateEmployee).Include(d => d.DepartmentGroup).Include(d => d.UpdateEmployee).Where(i => i.Status == true);
                return View(department.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Details
        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Create
        // GET: Departments/Create
        public ActionResult Create()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                List<DepartmentGroup> groupList = new List<DepartmentGroup>();
                groupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentGroupId = new SelectList(groupList, "Id", "Name");
                return View();
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,DepartmentGroupId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Department department)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (db.Department.Where(i => i.Code == department.Code).ToList().Count < 1)
                {
                    if (db.Department.Where(i => i.Name == department.Name && i.DepartmentGroupId == department.DepartmentGroupId).ToList().Count < 1)
                    {
                        department.CreatedBy = Convert.ToInt32(Session["ADMINID"]);
                        department.CreateDate = DateTime.Now;
                        department.Status = true;
                        db.Department.Add(department);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not Succedd!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Code Exist!");
                }
                List<DepartmentGroup> groupList = new List<DepartmentGroup>();
                groupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();

                ViewBag.DepartmentGroupId = new SelectList(groupList, "Id", "Name");
                return View(department);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
            
            
        }
        #endregion

        #region Edit
        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                List<DepartmentGroup> groupList = new List<DepartmentGroup>();
                groupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
                ViewBag.DepartmentGroupId = new SelectList(groupList, "Id", "Name");
                return View(department);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
            
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,DepartmentGroupId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Department department)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                List<DepartmentGroup> groupList = new List<DepartmentGroup>();
                groupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
                if (db.Department.Where(i => i.Id == department.Id).Select(i => i.Code).FirstOrDefault() != department.Code && department.Code != null)
                {
                    if (db.Department.Where(i => i.Code == department.Code).ToList().Count < 1)
                    {
                        department.UpdatedBy = Convert.ToInt32(Session["ADMINID"]);
                        department.UpdateDate = DateTime.Now;
                        db.Entry(department).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Code Exist!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Enter a Code!");
                }

                ViewBag.DepartmentGroupId = new SelectList(db.DepartmentGroup, "Id", "Name", department.DepartmentGroupId);
                return View(department);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion

        #region Delete
        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                Department department = db.Department.Find(id);
                department.Status = false;
                db.Entry(department).State = EntityState.Modified;
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
