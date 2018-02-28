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
    public class BranchesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        // GET: Branches
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                return View(db.Branch.Where(i => i.Status == true).ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Branch branch = db.Branch.Find(id);
                if (branch == null)
                {
                    return HttpNotFound();
                }
                return View(branch);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Branches/Create
        public ActionResult Create()
        {

            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                return View();
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            ;
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,OpeningTime,EndingTime,IsLateCalculated,LateConsiderationTime,LateConsiderationDay,LateDeductionPercentage,IsOvertimeCalculated,OvertimeConsiderationTime,OvertimePaymentPercentage,Status")] Branch branch)
        {
            if(Session["ADMIN"] == null || Session["ADMIN"].ToString() == "admin")
                {
                branch.Status = true;
                if (ModelState.IsValid)
                {
                    db.Branch.Add(branch);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(branch);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Branch branch = db.Branch.Find(id);
                if (branch == null)
                {
                    return HttpNotFound();
                }
                return View(branch);
            }
            else{
                return RedirectToAction("Login", "Account");
            }

            
            
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,OpeningTime,EndingTime,IsLateCalculated,LateConsiderationTime,LateConsiderationDay,LateDeductionPercentage,IsOvertimeCalculated,OvertimeConsiderationTime,OvertimePaymentPercentage,Status")] Branch branch)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                branch.Status = true;
                if (ModelState.IsValid)
                {
                    db.Entry(branch).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(branch);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"]!= null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Branch branch = db.Branch.Find(id);
                if (branch == null)
                {
                    return HttpNotFound();
                }
                return View(branch);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (db.Employee.Where(i => i.BranchId == id && i.Status == true).ToList().Count < 1)
                {
                    Branch branch = db.Branch.Find(id);
                    branch.Status = false;
                    db.Entry(branch).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Delete Success";
                }
                else
                {
                    TempData["Message"] = "Failed to Delete";
                }
                return RedirectToAction("Index");
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
