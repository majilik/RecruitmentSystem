using RecruitmentSystem.DAL.Authorization;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.DAL.Authorization.Interfaces;

namespace RecruitmentSystem.Security
{
    /// <summary>
    /// Decorate any methods with this attribute that requires authorized usage.
    /// </summary>
    public class PersonAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly IUserManager _userManager;
        private readonly string[] _roles;

        public PersonAuthorizationAttribute() : this(new UserManager())
        {
        }

        public PersonAuthorizationAttribute(UserManager userManager)
        {
            _userManager = userManager;
            _roles = null;
        }

        public PersonAuthorizationAttribute(params string[] roles) : this(new UserManager(), roles)
        {
        }

        public PersonAuthorizationAttribute(UserManager userManager, params string[] roles)
        {
            _userManager = userManager;
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string user = httpContext.User.Identity.Name;

            if (_roles != null)
            {
                foreach (var role in _roles)
                {
                    if (_userManager.IsUserInRole(user, role))
                    {
                        return true;
                    }
                }

                return false;
            }

            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/Unauthorized");
        }
    }
}