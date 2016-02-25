using RecruitmentSystem.DAL.Authorization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.DAL.Authorization.Interfaces;
using System.Web.Routing;

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
            _roles = new string[0];
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
            if (httpContext == null)
                return false;

            string user = httpContext.User.Identity.Name;

            if (_roles.Any(role => _userManager.IsUserInRole(user, role)))
            {
                return true;
            }

            return httpContext.User.Identity.IsAuthenticated;
        }
        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                action = "Unauthorized",
                controller = "Home",
                area = ""
            }));
        }
    }
}