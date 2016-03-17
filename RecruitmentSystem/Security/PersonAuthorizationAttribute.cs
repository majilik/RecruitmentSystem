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

        /// <summary>
        /// Default constructor, creates with a UserManager.
        /// </summary>
        public PersonAuthorizationAttribute() : this(new UserManager())
        {
        }

        /// <summary>
        /// Constructor, specified with a IUserManager implementation
        /// </summary>
        /// <param name="userManager">IUserManager to create Attribute with</param>
        public PersonAuthorizationAttribute(IUserManager userManager)
        {
            _userManager = userManager;
            _roles = new string[0];
        }

        /// <summary>
        /// Constructor, takes roles as a parameterlist.
        /// </summary>
        /// <param name="roles">Parameter list of roles as strings.</param>
        public PersonAuthorizationAttribute(params string[] roles) : this(new UserManager(), roles)
        {
        }

        /// <summary>
        /// Constructor, takes roles as a parameterlist, and a IUserManager implementation.
        /// </summary>
        /// <param name="userManager">IUserManager implementation to use.</param>
        /// <param name="roles"></param>
        public PersonAuthorizationAttribute(IUserManager userManager, params string[] roles)
        {
            _userManager = userManager;
            _roles = roles;
        }

        /// <summary>
        /// Entry point for authorzation check.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>true if the user is authorized; otherwise, false.</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                return false;
            }

            string user = httpContext.User.Identity.Name;

            if (_roles.Any(role => _userManager.IsUserInRole(user, role)))
            {
                return httpContext.User.Identity.IsAuthenticated;
            }

            return false;
        }

        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using AuthorizeAttribute. The filterContext object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                action = "Unauthorized",
                controller = "Error",
                area = ""
            }));
        }
    }
}