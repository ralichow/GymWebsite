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
    public class MembershipRegistrationController : Controller
    {

        private readonly GymMembershipManagementSystemContext _dbContext = new GymMembershipManagementSystemContext();
        private GymMembershipManagementSystemContext db = new GymMembershipManagementSystemContext();

        // GET: MembershipRegistration
        public ActionResult Index()
        {
            // Get the currently logged-in user's username
            var username = User.Identity.Name; // Replace with your authentication mechanism

            // Retrieve the user's ID based on their username
            int? userId = _dbContext.Users
                .Where(u => u.UserName == username)
                .Select(u => u.UserID)
                .FirstOrDefault();

            if (userId == null)
            {
                // Handle the case where the user is not found
                return RedirectToAction("Index");
            }

            // Retrieve memberships for the logged-in user
            var memberships = _dbContext.MembershipRegistrations
                .Where(m => m.UserID == userId)
                .ToList();

            return View(memberships);
        }

        // GET: MembershipRegistration/Details/5
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

        // GET: MembershipRegistration/Create
        public ActionResult Create()
        {
            // Get the currently logged-in user's username
            var username = User.Identity.Name; // Replace with your authentication mechanism

            // Retrieve the user's ID based on their username
            int? userId = _dbContext.Users
                .Where(u => u.UserName == username)
                .Select(u => u.UserID)
                .FirstOrDefault();

            if (userId == null)
            {
                // Handle the case where the user is not found
                return RedirectToAction("Index");
            }

            ViewBag.MembershipTypeID = new SelectList(_dbContext.MembershipTypes, "MembershipId", "MembershipName");
            ViewBag.UserID = userId;
            return View();
        }

        // POST: MembershipRegistration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationID,UserID,MembershipTypeID,StartDate,EndDate,ActiveStatus,Note")] MembershipRegistration membershipRegistration)
        {
            if (ModelState.IsValid)
            {
                // Get the currently logged-in user's username
                var username = User.Identity.Name; // Replace with your authentication mechanism

                // Retrieve the user's ID based on their username
                int? userId = _dbContext.Users
                    .Where(u => u.UserName == username)
                    .Select(u => u.UserID)
                    .FirstOrDefault();

                if (userId == null)
                {
                    // Handle the case where the user is not found
                    return RedirectToAction("Index");
                }

                // Set the user ID for the new registration
                membershipRegistration.UserID = userId;

                db.MembershipRegistrations.Add(membershipRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "MembershipId", "MembershipName", membershipRegistration.MembershipTypeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", membershipRegistration.UserID);
            return View(membershipRegistration);
        }

        // GET: MembershipRegistration/Edit/5
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

        // POST: MembershipRegistration/Edit/5
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

        // GET: MembershipRegistration/Delete/5
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

        // POST: MembershipRegistration/Delete/5
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
