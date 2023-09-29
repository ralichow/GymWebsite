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
    public class StaffMembershipRegistrationController : Controller
    {
        private GymMembershipManagementSystemContext db = new GymMembershipManagementSystemContext();

        // GET: StaffMembershipRegistration
        public ActionResult Index()
        {
            var membershipRegistrations = db.MembershipRegistrations.Include(m => m.MembershipType).Include(m => m.User);
            return View(membershipRegistrations.ToList());
        }

        // GET: StaffMembershipRegistration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipRegistration membershipRegistration = db.MembershipRegistrations.Find(id);
            if (membershipRegistration == null)
            {
                return HttpNotFound();
            }
            return View(membershipRegistration);
        }

        // GET: StaffMembershipRegistration/Create
        public ActionResult Create()
        {
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipId", "MembershipName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: StaffMembershipRegistration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationID,UserID,MembershipTypeID,StartDate,EndDate,ActiveStatus,Note")] MembershipRegistration membershipRegistration)
        {
            if (ModelState.IsValid)
            {
                db.MembershipRegistrations.Add(membershipRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipId", "MembershipName", membershipRegistration.MembershipTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", membershipRegistration.UserID);
            return View(membershipRegistration);
        }

        // GET: StaffMembershipRegistration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipRegistration membershipRegistration = db.MembershipRegistrations.Find(id);
            if (membershipRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipId", "MembershipName", membershipRegistration.MembershipTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", membershipRegistration.UserID);
            return View(membershipRegistration);
        }

        // POST: StaffMembershipRegistration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationID,UserID,MembershipTypeID,StartDate,EndDate,ActiveStatus,Note")] MembershipRegistration membershipRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membershipRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipId", "MembershipName", membershipRegistration.MembershipTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", membershipRegistration.UserID);
            return View(membershipRegistration);
        }

        // GET: StaffMembershipRegistration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipRegistration membershipRegistration = db.MembershipRegistrations.Find(id);
            if (membershipRegistration == null)
            {
                return HttpNotFound();
            }
            return View(membershipRegistration);
        }

        // POST: StaffMembershipRegistration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipRegistration membershipRegistration = db.MembershipRegistrations.Find(id);
            db.MembershipRegistrations.Remove(membershipRegistration);
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
