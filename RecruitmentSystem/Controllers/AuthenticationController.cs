using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.DAL;
using RecruitmentSystem.DAL.Authorization;
using RecruitmentSystem.Models;
using RecruitmentSystem.Security;

namespace RecruitmentSystem.Controllers
{
    //TODO: Document this class in Architecture Document
    [PersonAuthorization]
    public class AuthenticationController : Controller
    {
        private RecruitmentContext db = new RecruitmentContext();

        //TODO: Implement Login GET/POST here?

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(Person person)
        {
            if (ModelState.IsValid)
            {
                var um = new UserManager();
                if (!um.IsUsernameInUse(person.Username))
                {
                    um.AddUser(person);
                    //TODO: When Login is implemented, redirect to login view here!
                    return RedirectToAction("Index", "Home");
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
