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
    public class ImagesController : Controller
    {
        private LICT_HRMSDbContext db = new LICT_HRMSDbContext();

        // GET: Images
        public ActionResult Index()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                var images = db.Images.Include(i => i.Employee);
                return View(images.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Images images = db.Images.Find(id);
                if (images == null)
                {
                    return HttpNotFound();
                }
                return View(images);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Image,EmployeeId")] Images images)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Images.Add(images);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", images.EmployeeId);
                return View(images);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Images images = db.Images.Find(id);
                if (images == null)
                {
                    return HttpNotFound();
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", images.EmployeeId);
                return View(images);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,EmployeeId")] Images images)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(images).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.EmployeeId = new SelectList(db.Employee, "Id", "Name", images.EmployeeId);
                return View(images);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Images images = db.Images.Find(id);
                if (images == null)
                {
                    return HttpNotFound();
                }
                return View(images);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ADMIN"] != null || Session["ADMIN"].ToString() == "admin")
            {
                Images images = db.Images.Find(id);
                db.Images.Remove(images);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
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
