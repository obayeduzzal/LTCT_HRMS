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
    public class BranchTransfersController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        #region List
        // GET: BranchTransfers
        public ActionResult Index()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                var branchTransfer = db.BranchTransfer.Include(b => b.Employee).Include(b => b.FromBranch).Include(b => b.ToBranch);
                return View(branchTransfer.ToList());
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        #endregion

        #region Details
        // GET: BranchTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
                if (branchTransfer == null)
                {
                    return HttpNotFound();
                }
                return View(branchTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        #endregion


        #region Create Employee
        // GET: BranchTransfers/Create
        public ActionResult Create()
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                List<Branch> branchList = new List<Branch>();
                branchList = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.ToBranchId = new SelectList(branchList, "Id", "Name");
                List<Employee> employeelist = new List<Employee>();
                employeelist = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employeelist, "Id", "Name");
                return View();
            }
            else{
                return RedirectToAction("Login", "Account");
            }

            
        }

        // POST: BranchTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,FromBranchId,ToBranchId,TransferDate")] BranchTransfer branchTransfer)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    int fromBranchId = db.Employee.Where(i => i.BranchId == branchTransfer.EmployeeId).Select(x => x.BranchId).FirstOrDefault();
                    int toBranchId = Convert.ToInt32(Request["ToBranchId"]);
                    branchTransfer.FromBranchId = fromBranchId;
                    branchTransfer.ToBranchId = toBranchId;
                    db.BranchTransfer.Add(branchTransfer);
                    db.SaveChanges();

                    Employee employee = db.Employee.Find(branchTransfer.EmployeeId);
                    employee.BranchId = toBranchId;
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Creation Success!";
                    return RedirectToAction("Index");
                }

                List<Branch> branchList = new List<Branch>();
                branchList = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.ToBranchId = new SelectList(branchList, "Id", "Name");
                List<Employee> employeelist = new List<Employee>();
                employeelist = db.Employee.Where(i => i.Status == true).ToList();
                ViewBag.EmployeeId = new SelectList(employeelist, "Id", "Name");
                return View(branchTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        #endregion

        #region Edit
        // GET: BranchTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
                ViewBag.Branch = db.Branch.Where(i => i.Id == branchTransfer.ToBranchId).Select(i => i.Name).FirstOrDefault();
                List<Branch> branch = new List<Branch>();
                branch = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(branch, "Id", "Name");
                if (branchTransfer == null)
                {
                    return HttpNotFound();
                }
                List<Branch> branchList = new List<Branch>();
                branchList = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(branchList, "Id", "Name");
                return View(branchTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: BranchTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,FromBranchId,ToBranchId,TransferDate")] BranchTransfer branchTransfer)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (ModelState.IsValid)
                {
                    int toBranchId = Convert.ToInt32(Request["BranchId"]);
                    branchTransfer.ToBranchId = toBranchId;
                    int fromBranchId = db.Employee.Where(i => i.BranchId == branchTransfer.EmployeeId).Select(i => i.BranchId).FirstOrDefault();
                    db.Entry(branchTransfer).State = EntityState.Modified;
                    db.SaveChanges();

                    Employee employee = db.Employee.Find(branchTransfer.EmployeeId);
                    employee.BranchId = toBranchId;
                    db.Entry(employee).State = EntityState.Modified;

                    return RedirectToAction("Index");
                }

                BranchTransfer BranchTransfer = db.BranchTransfer.Find(branchTransfer.Id);
                ViewBag.Branch = db.Branch.Where(i => i.Id == branchTransfer.ToBranchId).Select(i => i.Name).FirstOrDefault();
                List<Branch> branch = new List<Branch>();
                branch = db.Branch.Where(i => i.Status == true).ToList();
                ViewBag.BranchId = new SelectList(branch, "Id", "Name");
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            


            return View(branchTransfer);
        }

        #endregion

        #region Delete

        // GET: BranchTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
                if (branchTransfer == null)
                {
                    return HttpNotFound();
                }
                return View(branchTransfer);
            }
            else{
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: BranchTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
                {
                BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
                db.BranchTransfer.Remove(branchTransfer);
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
