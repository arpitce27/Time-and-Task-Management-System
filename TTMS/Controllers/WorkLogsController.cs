using Microsoft.AspNet.Identity;
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
    [Authorize(Roles = "Student")]
    public class WorkLogsController : AccountController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkLogs
        public ActionResult Index()
        {
            var workLog = db.WorkLog.Include(w => w.work);
            return View(workLog.ToList());
        }

        // GET: WorkLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkLog workLog = db.WorkLog.Find(id);
            if (workLog == null)
            {
                return HttpNotFound();
            }
            return View(workLog);
        }

        // GET: WorkLogs/Create
        public ActionResult Create()
        {
            var id = User.Identity.GetUserId();
            ViewBag.WorkId = new SelectList(db.Works.Where(i => i.Assignedstudents.Any(w => id.Contains(w.Id))), "ID", "WorkTitle");
            return View();
        }

        // POST: WorkLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WorkId,StartTime,EndTime,user")] WorkLog workLog)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            workLog.user = db.Users.FirstOrDefault(u => u.Id == user.Id);
            workLog.TimeSpend = (workLog.EndTime - workLog.StartTime).TotalHours;
            workLog.CretionDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.WorkLog.Add(workLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkId = new SelectList(db.Works, "ID", "WorkTitle", workLog.WorkId);
            return View(workLog);
        }

        // GET: WorkLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkLog workLog = db.WorkLog.Find(id);
            if (workLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkId = new SelectList(db.Works, "ID", "WorkTitle", workLog.WorkId);
            return View(workLog);
        }

        // POST: WorkLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WorkId,CretionDate,StartTime,EndTime,TimeSpend")] WorkLog workLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkId = new SelectList(db.Works, "ID", "WorkTitle", workLog.WorkId);
            return View(workLog);
        }

        // GET: WorkLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkLog workLog = db.WorkLog.Find(id);
            if (workLog == null)
            {
                return HttpNotFound();
            }
            return View(workLog);
        }

        // POST: WorkLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkLog workLog = db.WorkLog.Find(id);
            db.WorkLog.Remove(workLog);
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
