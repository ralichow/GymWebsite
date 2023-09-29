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
    [Authorize(Roles =  "Staff")]
    public class ClassSessionController : Controller
    {
        private GymMembershipManagementSystemContext db = new GymMembershipManagementSystemContext();

        // GET: ClassSession
        public ActionResult Index()
        {
            return View(db.ClassSessions.ToList());
        }

        // GET: ClassSession/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSession classSession = db.ClassSessions.Find(id);
            if (classSession == null)
            {
                return HttpNotFound();
            }
            return View(classSession);
        }

        // GET: ClassSession/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassSession/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,ClassName,Description")] ClassSession classSession)
        {
            if (ModelState.IsValid)
            {
                db.ClassSessions.Add(classSession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classSession);
        }

        // GET: ClassSession/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSession classSession = db.ClassSessions.Find(id);
            if (classSession == null)
            {
                return HttpNotFound();
            }
            return View(classSession);
        }

        // POST: ClassSession/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,ClassName,Description")] ClassSession classSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classSession);
        }

        // GET: ClassSession/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSession classSession = db.ClassSessions.Find(id);
            if (classSession == null)
            {
                return HttpNotFound();
            }
            return View(classSession);
        }

        // POST: ClassSession/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSession classSession = db.ClassSessions.Find(id);
            db.ClassSessions.Remove(classSession);
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
