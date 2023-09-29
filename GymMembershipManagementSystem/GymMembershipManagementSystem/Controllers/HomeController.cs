using GymMembershipManagementSystem.Data;
using GymMembershipManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Diagnostics;

namespace GymMembershipManagementSystem.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly GymMembershipManagementSystemContext _dbContext = new GymMembershipManagementSystemContext();


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = Models.User.HashPassword(user.Password);


                bool IsValidUser = _dbContext.Users
                    .Any(u => u.UserName.ToLower() == user.UserName.ToLower() && u.Password == hashedPassword);

                bool IsValidStaff = false;

                if(IsValidUser && _dbContext.Users.Any(u => u.IsStaff) == true)
                {
                    IsValidStaff = true;
                }


                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);

                    if (IsValidStaff)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "MembershipRegistration");
                    }
                }
                else
                {
                    // Log or debug the validation errors
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        // Log or debug the error messages
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Username or Password");
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before adding it to the database
                var hashedPassword = Models.User.HashPassword(registerUser.Password);
                registerUser.Password = hashedPassword;

                _dbContext.Users.Add(registerUser);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Class()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}