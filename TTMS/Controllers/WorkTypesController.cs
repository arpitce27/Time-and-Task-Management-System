using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTMS.Models;

namespace TTMS.Controllers
{
    public class WorkTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkTypes
        public ActionResult Index()
        {
            return View(db.WorkType.ToList());
        }

        // GET: WorkTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = db.WorkType.Find(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // GET: WorkTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TypeName")] WorkType workType)
        {
            if (ModelState.IsValid)
            {
                db.WorkType.Add(workType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workType);
        }

        // GET: WorkTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = db.WorkType.Find(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // POST: WorkTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TypeName")] WorkType workType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workType);
        }

        // GET: WorkTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = db.WorkType.Find(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // POST: WorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkType workType = db.WorkType.Find(id);
            db.WorkType.Remove(workType);
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
