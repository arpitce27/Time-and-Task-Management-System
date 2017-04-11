using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTMS.Models;
using Microsoft.AspNet.Identity;


namespace TTMS.Controllers
{
    public class AssignedTaskController : AccountController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssignedTask
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var works = db.Works.Where(i => i.Assignedstudents.Any(w => id.Contains(w.Id)));
            return View(works.ToList());
        }

        public ActionResult Tasks()
        {
            var id = User.Identity.GetUserId();
            var works = db.Works.Include(w => w.WorkType).Where(i => i.Assignedstudents.Any(w => id.Contains(w.Id)));
            var tasks = works.Where(t => t.WorkType.TypeName == "task");
            return View(tasks.ToList());
        }

        public ActionResult Events()
        {
            var id = User.Identity.GetUserId();
            var works = db.Works.Include(w => w.WorkType).Where(i => i.Assignedstudents.Any(w => id.Contains(w.Id)));
            var tasks = works.Where(t => t.WorkType.TypeName == "event");
            return View(tasks.ToList());
        }

        public ActionResult Projects()
        {
            var id = User.Identity.GetUserId();
            var works = db.Works.Include(w => w.WorkType).Where(i => i.Assignedstudents.Any(w => id.Contains(w.Id)));
            var tasks = works.Where(t => t.WorkType.TypeName == "project");
            return View(tasks.ToList());
        }
        // GET: AssignedTask/Details/5
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

        // GET: AssignedTask/Edit/5
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
            ViewBag.WorkTypeID = new SelectList(db.WorkType, "ID", "TypeName", work.WorkTypeID);
            return View(work);
        }

        // POST: AssignedTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WorkTypeID,Priority,WorkTitle,WorkDescr,CreationDate,Deadline,Status")] Work work)
        {
            if (ModelState.IsValid)
            {
                db.Entry(work).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkTypeID = new SelectList(db.WorkType, "ID", "TypeName", work.WorkTypeID);
            return View(work);
        }

        // POST: AssignedTask/Delete/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitComment(CommentViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    if (!String.IsNullOrEmpty(model.Content))
                    {
                        Comment comment = new Comment()
                        {
                            PostTime = DateTime.Now,
                            Content = model.Content,
                            Work = db.Works.Where(p => p.ID == id).FirstOrDefault(),
                            User = db.Users.FirstOrDefault(u => u.Id == user.Id)
                        };
                        db.Comment.Add(comment);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Details", "Works", new { id = id });
            }
            return RedirectToAction("Details", "Works", new { id = id });
        }
    }
}
