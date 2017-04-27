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
    }
}
