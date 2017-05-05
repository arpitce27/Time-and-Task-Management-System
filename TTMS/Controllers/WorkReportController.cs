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
    public class WorkReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkReport
        public ActionResult Index()
        {
            var works = db.Works.Include(w => w.WorkType);
            return View(works.ToList());
        }

        // GET: WorkReport/Details/5
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult reportbywork(int? id)
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
            List<reportbywork> students = new List<reportbywork>();
            var assigedstud = work.Assignedstudents.ToList();

            foreach (var student in assigedstud)
            {
                var wl = db.WorkLog.Include(i => i.work).Include(i => i.user)
                    .Where(i => i.WorkId == id && i.user.UserName == student.UserName).ToList();
                double hr = 0;
                foreach (var worklog in wl)
                {
                    hr += worklog.TimeSpend;
                }
                reportbywork _studreport = new reportbywork();
                _studreport.worktitle = work.WorkTitle;
                _studreport.username = student.FirstName;
                _studreport.hourworked = Math.Round(hr, 2);
                students.Add(_studreport);
            }
            return View(students.ToList());
        }
    }
}
