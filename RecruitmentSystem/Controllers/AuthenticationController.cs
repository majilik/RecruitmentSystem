using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using RecruitmentSystem.DAL;
using RecruitmentSystem.DAL.Authorization;
using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;

namespace RecruitmentSystem.Controllers
{
    //TODO: Document this class in Architecture Document
    
    public class AuthenticationController : Controller
    {
        //TODO: Is this needed here?
        private RecruitmentContext db = new RecruitmentContext();

        //TODO: Implement Login GET/POST here?
        
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginView loginView)
        {
            if (ModelState.IsValid)
            {
                var um = new UserManager();

                if (um.IsUsernameInUse(loginView.Username))
                {
                    if (um.LoginCheck(loginView))
                    {
                        FormsAuthentication.SetAuthCookie(loginView.Username, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [PersonAuthorization]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterView registerView)
        {
            if (ModelState.IsValid)
            {
                var um = new UserManager();
                if (!um.IsUsernameInUse(registerView.Username))
                {
                    um.AddUser(registerView);
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username already in use!");
                }
            }
            return View();
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
