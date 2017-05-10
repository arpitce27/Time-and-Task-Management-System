using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTMS.Models;

namespace TTMS.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class WorksController : AccountController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Works
        public ActionResult Index()
        {
            var works = db.Works.Include(w => w.WorkType);
            return View(works.ToList());
        }

        // GET: Works/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Include(i => i.Assignedstudents).First(i => i.ID == id);
            string str = string.Empty;

            List<string> ls = new List<string>();
            foreach (var item in work.Assignedstudents)
            {
                ls.Add(item.FirstName);
            }
            foreach (var item in ls)
                str = str + item + ", ";
            ViewBag.assigned = str;
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // GET: Works/Create
        public ActionResult Create()
        {
            ViewBag.WorkTypeID = new SelectList(db.WorkType, "ID", "TypeName");
            return View();
        }

        // POST: Works/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WorkTypeID,Priority,WorkTitle,WorkDescr,Deadline,Status")] Work work)
        {
            work.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Works.Add(work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkTypeID = new SelectList(db.WorkType, "ID", "TypeName", work.WorkTypeID);
            return View(work);
        }

        //Get Editassignments/1
        public ActionResult Editassignments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Work work = db.Works.Find(id);
            var workassignmentviewmodel = new WorkAssignmentViewModel
            {
                Work = db.Works.Include(i => i.Assignedstudents).First(i => i.ID == id)
            };

            if (workassignmentviewmodel == null)
                return HttpNotFound();

            var allstudents = db.Users
                                 .Where(x => x.Roles.Select(y => y.RoleId)
                                 .Contains("33abfbd1-cd4a-473a-ac9c-1924899cc0c8"))
                                 .ToList();
            workassignmentviewmodel.AllStudents = allstudents.Select(i => new SelectListItem
            {
                Text = i.FirstName,
                Value = i.Id.ToString()
            });
            ViewBag.WorkTypeID = new SelectList(db.WorkType, "ID", "TypeName", workassignmentviewmodel.Work.WorkTypeID);
            return View(workassignmentviewmodel);
        }

        // POST: Works/Editassignments/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editassignments(WorkAssignmentViewModel _workassignmentviewmodel)
        {
            if(_workassignmentviewmodel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                var assignmenttoupdate = db.Works.Include(i => i.Assignedstudents).First(i => i.ID == _workassignmentviewmodel.Work.ID);

                if (TryUpdateModel(assignmenttoupdate, "Work", new string[] { "Status" }))
                {
                    var newselectedstudents = new HashSet<string>(_workassignmentviewmodel.SelectedStudents);
                    foreach(User u in db.Users)
                    {
                        if (!newselectedstudents.Contains(u.Id))
                            assignmenttoupdate.Assignedstudents.Remove(u);
                        else
                            assignmenttoupdate.Assignedstudents.Add(u);
                    }
                    db.Entry(assignmenttoupdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(_workassignmentviewmodel);
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
            ViewBag.WorkTypeID = new SelectList(db.WorkType, "ID", "TypeName", work.WorkTypeID);
            return View(work);
        }
        
        // POST: Works/Edit/5
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
            var works = db.Works.Include(w => w.WorkType);
            var tasks = works.Where(t => t.WorkType.TypeName == "task");
            return View(tasks.ToList());
        }
        public ActionResult Events()
        {
            var works = db.Works.Include(w => w.WorkType);
            var tasks = works.Where(t => t.WorkType.TypeName == "event");
            return View(tasks.ToList());
        }
        public ActionResult Projects()
        {
            var works = db.Works.Include(w => w.WorkType);
            var tasks = works.Where(t => t.WorkType.TypeName == "project");
            return View(tasks.ToList());
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
