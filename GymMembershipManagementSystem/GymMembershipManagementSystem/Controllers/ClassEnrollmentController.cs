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
    [Authorize]
    public class ClassEnrollmentController : Controller
    {
        private GymMembershipManagementSystemContext db = new GymMembershipManagementSystemContext();

        // GET: ClassEnrollment
        public ActionResult Index()
        {
            var classEnrollments = db.ClassEnrollments.Include(c => c.ClassSchedule).Include(c => c.User);
            return View(classEnrollments.ToList());
        }

        // GET: ClassEnrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassEnrollment classEnrollment = db.ClassEnrollments.Find(id);
            if (classEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(classEnrollment);
        }

        // GET: ClassEnrollment/Create
        public ActionResult Create()
        {
            ViewBag.ScheduleID = new SelectList(db.ClassSchedules, "ScheduleID", "ScheduleID");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: ClassEnrollment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentId,ScheduleID,UserID")] ClassEnrollment classEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.ClassEnrollments.Add(classEnrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScheduleID = new SelectList(db.ClassSchedules, "ScheduleID", "ScheduleID", classEnrollment.ScheduleID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", classEnrollment.UserID);
            return View(classEnrollment);
        }

        // GET: ClassEnrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassEnrollment classEnrollment = db.ClassEnrollments.Find(id);
            if (classEnrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScheduleID = new SelectList(db.ClassSchedules, "ScheduleID", "ScheduleID", classEnrollment.ScheduleID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", classEnrollment.UserID);
            return View(classEnrollment);
        }

        // POST: ClassEnrollment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentId,ScheduleID,UserID")] ClassEnrollment classEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classEnrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScheduleID = new SelectList(db.ClassSchedules, "ScheduleID", "ScheduleID", classEnrollment.ScheduleID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", classEnrollment.UserID);
            return View(classEnrollment);
        }

        // GET: ClassEnrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassEnrollment classEnrollment = db.ClassEnrollments.Find(id);
            if (classEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(classEnrollment);
        }

        // POST: ClassEnrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassEnrollment classEnrollment = db.ClassEnrollments.Find(id);
            db.ClassEnrollments.Remove(classEnrollment);
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
