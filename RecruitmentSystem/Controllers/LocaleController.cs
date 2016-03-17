using System;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.Resources;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
    /// Controller handles requests about Localization.
    /// </summary>
    public class LocaleController : BaseController
    {
        /// <summary>
        /// Http GET to change the locale. 
        /// Defaults to EN_US.
        /// </summary>
        /// <param name="locale">Locales enum.</param>
        /// <returns>Redirects to Home.</returns>
        public ActionResult ChangeLocale(Locales? locale)
        {
            string localeName = LocalesExtension.ParseCultureInfo(locale).Name;

            HttpCookie cookie = Request.Cookies["_locale"];
            if (cookie != null)
            {
                cookie.Value = localeName;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie("_locale");
                cookie.Value = localeName;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}