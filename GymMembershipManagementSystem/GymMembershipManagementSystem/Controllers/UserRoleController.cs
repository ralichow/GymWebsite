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
    public class UserRoleController : Controller
    {
        private GymMembershipManagementSystemContext db = new GymMembershipManagementSystemContext();

        // GET: UserRole
        public ActionResult Index()
        {
            var userRoles = db.UserRoles.Include(u => u.Role).Include(u => u.User);
            return View(userRoles.ToList());
        }

        // GET: UserRole/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRole/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: UserRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserRoleID,RoleID,UserID")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", userRole.RoleID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", userRole.UserID);
            return View(userRole);
        }

        // GET: UserRole/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", userRole.RoleID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", userRole.UserID);
            return View(userRole);
        }

        // POST: UserRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRoleID,RoleID,UserID")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", userRole.RoleID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", userRole.UserID);
            return View(userRole);
        }

        // GET: UserRole/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRole userRole = db.UserRoles.Find(id);
            db.UserRoles.Remove(userRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
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
       
    }
}
