using System.Web.Mvc;
using RecruitmentSystem.DAL.Authorization;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;
using RecruitmentSystem.DAL.Authorization.Interfaces;

namespace RecruitmentSystem.Controllers
{
    //TODO: Document this class in Architecture Document
    public class AuthenticationController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IFormsAuthenticationWrap _formsAuthentication;

        public AuthenticationController() : this(new UserManager(), new FormsAuthenticationWrap())
        {
        }

        public AuthenticationController(IUserManager userManager, IFormsAuthenticationWrap formsAuthentication)
        {
            _userManager = userManager;
            _formsAuthentication = formsAuthentication;
        }

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
                if (_userManager.IsUsernameInUse(loginView.Username))
                {
                    if (_userManager.LoginCheck(loginView))
                    {
                        _formsAuthentication.SetAuthCookie(loginView.Username, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("wrong_pass", "Wrong password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("non_existent_user", "User doesn't exist.");
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
            _formsAuthentication.SignOut();
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
                if (registerView.Password.Equals(registerView.PasswordVerify))
                {
                    if (!_userManager.IsUsernameInUse(registerView.Username))
                    {
                        _userManager.AddUser(registerView);
                        return RedirectToAction("Login", "Authentication");
                    }
                    else
                    {
                        ModelState.AddModelError("username_in_use", "Username already in use.");
                    }
                }
                else
                {
                    ModelState.AddModelError("verify_pass", "Passwords must be identical.");
                }
            }

            return View();
        }

    }
}
