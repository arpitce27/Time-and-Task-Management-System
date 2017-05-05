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
using System.Net.Mail;
using System.IO;
using System.Reflection;

namespace TTMS.Controllers
{
    [Authorize(Roles = "Student")]
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
            string userid = User.Identity.GetUserId();
            var un = db.Users.Where(i => i.Id == userid).FirstOrDefault();
            string workname = work.WorkTitle;
            string st = work.Status.ToString();
            if (ModelState.IsValid)
            {
                db.Entry(work).State = EntityState.Modified;
                db.SaveChanges();
                if (st == "Completed")
                {
                    sendMail(workname, un.FirstName);
                }
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
                return RedirectToAction("Details", "AssignedTask", new { id = id });
            }
            return RedirectToAction("Details", "AssignedTask", new { id = id });
        }

        public void sendMail(string workname, string username)
        {
            try
            {
                string from = "arpitkumar27@gmail.com";
                string to = "arpit.ce27@gmail.com"; // All supervisor mail
                MailMessage objMsg = new MailMessage(from, to);

                //string cc = "arpit.ce27@gmail.com"; //Add all supervisor email as a cc
                var sup_list = db.Users
                                 .Where(x => x.Roles.Select(y => y.RoleId)
                                 .Contains("8d26f441-eb00-4c93-80a2-8789705b5ea0"))
                                 .ToList();
                //foreach (var s in sup_list)
                //{
                //    objMsg.To.Add(s.Email);
                //}
                //objMsg.CC.Add(cc);

                string body = GetBodyText(workname, username);
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                // Add the alternate views instead of using MailMessage.Body
                objMsg.AlternateViews.Add(avHtml);

                objMsg.Subject = "Assigned task has been completed";

                objMsg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("arpitkumar27@gmail.com", "Rightdirections_91010");
                client.Send(objMsg);
                //SmtpClient objMailClient = GetSMTPClientObject();
                //objMailClient.Credentials = new NetworkCredential(from, "Rightdirections_91010");
                //objMailClient.Send(objMsg);
                //return true;
            }
            catch (Exception e)
            {}
        }
        public string GetBodyText(string WorkName, string UserName)
        {
            string strBody = ReadTemplateFile();
            //var UserData = order.GetBillingAddress();
            strBody = strBody.Replace("##Work##", WorkName);
            strBody = strBody.Replace("##By##", UserName);
            return strBody;
        }
        public string ReadTemplateFile()
        {
            String contents = string.Empty;
            try
            {     //using (StreamReader objStreamReader = System.IO.File.OpenText(Server.MapPath("../") + Path))
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                using (StreamReader objStreamReader = System.IO.File.OpenText("A:/Study Line/SampleProject/ASP.NET MVC 5 Security And Creating User Role/Time and Task Management System/TTMS/Template/Template.html"))
                {
                    contents = objStreamReader.ReadToEnd();
                    objStreamReader.Close();
                }
            }
            catch (Exception e)
            {
            }
            return contents;

        }
    }
}
