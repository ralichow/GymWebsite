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
    public class ReportController : Controller
    {
        private readonly GymMembershipManagementSystemContext _dbContext;

        private GymMembershipManagementSystemContext db = new GymMembershipManagementSystemContext();

        // GET: Report
      
        public ReportController()
        {
            _dbContext = new GymMembershipManagementSystemContext();
        }

        public ActionResult Index()
        {
            var membershipTypeReport = GetMembershipTypeSummary();
            return View(membershipTypeReport);
        }

        private IEnumerable<MembershipTypeSummaryViewModel> GetMembershipTypeSummary()
        {
            var membershipTypeSummary = _dbContext.MembershipTypes
                .Select(mt => new MembershipTypeSummaryViewModel
                {
                    MembershipTypeName = mt.MembershipName,
                    MemberCount = _dbContext.MembershipRegistrations
                        .Count(mr => mr.MembershipTypeID == mt.MembershipId)
                })
                .ToList();

            return membershipTypeSummary;
        }

        // GET: Report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            ViewBag.RegistrationId = new SelectList(db.MembershipRegistrations, "RegistrationID", "Note");
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID");
            return View();
        }

        // POST: Report/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportId,RegistrationId,UserRoleID,ReportName,Description")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegistrationId = new SelectList(db.MembershipRegistrations, "RegistrationID", "Note", report.RegistrationId);
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", report.UserRoleID);
            return View(report);
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegistrationId = new SelectList(db.MembershipRegistrations, "RegistrationID", "Note", report.RegistrationId);
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", report.UserRoleID);
            return View(report);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportId,RegistrationId,UserRoleID,ReportName,Description")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegistrationId = new SelectList(db.MembershipRegistrations, "RegistrationID", "Note", report.RegistrationId);
            ViewBag.UserRoleID = new SelectList(db.UserRoles, "UserRoleID", "UserRoleID", report.UserRoleID);
            return View(report);
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
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
