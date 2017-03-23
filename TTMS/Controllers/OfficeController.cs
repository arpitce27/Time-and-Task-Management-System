using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTMS.Models;

namespace shanuMVCUserRoles.Controllers
{
    public class OfficeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Office
        public ActionResult Index()
        {
            return View(db.OfficeInfo.ToList());
        }

        // GET: Office/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeInfo officeInfo = db.OfficeInfo.Find(id);
            if (officeInfo == null)
            {
                return HttpNotFound();
            }
            return View(officeInfo);
        }

        // GET: Office/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Office/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Office,OfficeName,OfficeDescr,Address,City,State,Zip,Country,Phone")] OfficeInfo officeInfo)
        {
            if (ModelState.IsValid)
            {
                db.OfficeInfo.Add(officeInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(officeInfo);
        }

        // GET: Office/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeInfo officeInfo = db.OfficeInfo.Find(id);
            if (officeInfo == null)
            {
                return HttpNotFound();
            }
            return View(officeInfo);
        }

        // POST: Office/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Office,OfficeName,OfficeDescr,Address,City,State,Zip,Country,Phone")] OfficeInfo officeInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officeInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(officeInfo);
        }

        // GET: Office/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeInfo officeInfo = db.OfficeInfo.Find(id);
            if (officeInfo == null)
            {
                return HttpNotFound();
            }
            return View(officeInfo);
        }

        // POST: Office/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfficeInfo officeInfo = db.OfficeInfo.Find(id);
            db.OfficeInfo.Remove(officeInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
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
