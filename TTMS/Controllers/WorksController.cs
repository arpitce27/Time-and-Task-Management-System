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
    [Authorize(Roles = "Supervisor")]
    public class WorksController : Controller
    {
        private TTMSEntities db = new TTMSEntities();

        // GET: Works
        public ActionResult Index()
        {
            var works = db.Works.Include(w => w.WorkPriority).Include(w => w.WorkType);
            works = works.OrderBy(s => s.Deadline);
            return View(works.ToList());
        }

        // GET: Works/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // GET: Works/Create
        public ActionResult Create()
        {
            ViewBag.FK_WorkPriority = new SelectList(db.WorkPriorities, "PK_WorkPriority", "Priority");
            ViewBag.FK_WorkType = new SelectList(db.WorkTypes, "PK_WorkType", "WorkName");
            return View();
        }

        // POST: Works/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Work,FK_WorkType,FK_WorkPriority,WorkTitle,WorkDescr,Deadline")] Work work)
        {
            work.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Works.Add(work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_WorkPriority = new SelectList(db.WorkPriorities, "PK_WorkPriority", "Priority", work.FK_WorkPriority);
            ViewBag.FK_WorkType = new SelectList(db.WorkTypes, "PK_WorkType", "WorkName", work.FK_WorkType);
            return View(work);
        }

        // GET: Works/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_WorkPriority = new SelectList(db.WorkPriorities, "PK_WorkPriority", "Priority", work.FK_WorkPriority);
            ViewBag.FK_WorkType = new SelectList(db.WorkTypes, "PK_WorkType", "WorkName", work.FK_WorkType);
            return View(work);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Work,FK_WorkType,FK_WorkPriority,WorkTitle,WorkDescr,CreationDate,Deadline")] Work work)
        {
            if (ModelState.IsValid)
            {
                db.Entry(work).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_WorkPriority = new SelectList(db.WorkPriorities, "PK_WorkPriority", "Priority", work.FK_WorkPriority);
            ViewBag.FK_WorkType = new SelectList(db.WorkTypes, "PK_WorkType", "WorkName", work.FK_WorkType);
            return View(work);
        }

        // GET: Works/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Work work = db.Works.Find(id);
            db.Works.Remove(work);
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

        public ActionResult Tasks()
        {
            var works = db.Works.Include(w => w.WorkPriority).Include(w => w.WorkType);
            var tasks = works.Where(t => t.WorkType.WorkName == "task");
            return View(tasks.ToList());
        }
        public ActionResult Events()
        {
            var works = db.Works.Include(w => w.WorkPriority).Include(w => w.WorkType);
            var tasks = works.Where(t => t.WorkType.WorkName == "event");
            return View(tasks.ToList());
            return View(tasks.ToList());
        }
        public ActionResult Projects()
        {
            var works = db.Works.Include(w => w.WorkPriority).Include(w => w.WorkType);
            var tasks = works.Where(t => t.WorkType.WorkName == "project");
            return View(tasks.ToList());
            return View(tasks.ToList());
        }
    }
}
