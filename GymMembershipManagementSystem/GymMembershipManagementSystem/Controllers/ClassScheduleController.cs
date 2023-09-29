using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GymMembershipManagementSystem.Data;
using GymMembershipManagementSystem.Models;

namespace GymMembershipManagementSystem.Controllers
{
    [Authorize(Roles = "Staff")]
    public class ClassScheduleController : Controller
    {
        private GymMembershipManagementSystemContext db = new GymMembershipManagementSystemContext();

        // GET: ClassSchedule
        public ActionResult Index()
        {
            var classSchedules = db.ClassSchedules.Include(c => c.ClassSession).Include(c => c.UserRole);
            return View(classSchedules.ToList());
        }

        // GET: ClassSchedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            return View(classSchedule);
        }

        // GET: ClassSchedule/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.ClassSessions, "ClassID", "ClassName");
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID");
            return View();
        }

        // POST: ClassSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleID,StartDate,EndDate,ClassDuration,Capacity,ClassID,UserRoleID")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                db.ClassSchedules.Add(classSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassSessions, "ClassID", "ClassName", classSchedule.ClassID);
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", classSchedule.UserRoleID);
            return View(classSchedule);
        }

        // GET: ClassSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassSessions, "ClassID", "ClassName", classSchedule.ClassID);
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", classSchedule.UserRoleID);
            return View(classSchedule);
        }

        // POST: ClassSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleID,StartDate,EndDate,ClassDuration,Capacity,ClassID,UserRoleID")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassSessions, "ClassID", "ClassName", classSchedule.ClassID);
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", classSchedule.UserRoleID);
            return View(classSchedule);
        }

        // GET: ClassSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            return View(classSchedule);
        }

        // POST: ClassSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            db.ClassSchedules.Remove(classSchedule);
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
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
