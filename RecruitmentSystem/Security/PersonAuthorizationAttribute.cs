using System;
using System.Collections.Generic;
using System.Linq;
using RecruitmentSystem.DAL.Authorization;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Security
{
    /// <summary>
    /// Decorate any methods with this attribute that requires authorized usage.
    /// </summary>
    public class PersonAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly new string[] Roles;

        public PersonAuthorizationAttribute()
        {
            Roles = null;
        }

        public PersonAuthorizationAttribute(params string[] roles)
        {
            Roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string user = httpContext.User.Identity.Name;
            var um = new UserManager();
            if (Roles != null)
            {
                foreach (var role in Roles)
                {
                    role.Trim();
                    if (um.IsUserInRole(user, role))
                        return true;
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