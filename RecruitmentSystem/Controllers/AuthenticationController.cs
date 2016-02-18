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
    
    /// <summary>
    /// Controller for Authentication services.
    /// </summary>
    public class AuthenticationController : Controller
    {
        /// <summary>
        /// GET for login
        /// </summary>
        /// <returns>Login View</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST for login
        /// Checks whether username is in use and if password is valid
        /// If so, user is authorized.
        /// </summary>
        /// <param name="loginView">ViewModel for login</param>
        /// <returns>View of either start page or login page</returns>
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wrong password.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User doesn't exist.");
                }
            }
            return View();
        }


        /// <summary>
        /// Removes the users authorization from URL
        /// </summary>
        /// <returns>Home page view</returns>
        [PersonAuthorization]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// GET for register
        /// </summary>
        /// <returns>Register View</returns>
        public ActionResult Register()
        {
            return View();
        }
        
        /// <summary>
        /// POST for register
        /// Adds a new user with data from register view model if the username is availible.
        /// </summary>
        /// <param name="registerView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterView registerView)
        {
            if (ModelState.IsValid)
            {
                var um = new UserManager();
                if(registerView.Password == registerView.PasswordVerify)
                {
                    if (!um.IsUsernameInUse(registerView.Username))
                    {
                        um.AddUser(registerView);
                        return RedirectToAction("Login", "Authentication");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username already in use.");
                    }
                }else ModelState.AddModelError(string.Empty, "Passwords must be identical.");

            }
            return View();
        }

    }
}
