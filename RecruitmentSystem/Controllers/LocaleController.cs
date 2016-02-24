using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.Resources;

namespace RecruitmentSystem.Controllers
{
    public class LocaleController : BaseController
    {
        /// <summary>
        /// Changes the locale. Defaults to EN_US.
        /// Assumes route data 'locale' as a Locales enum.
        /// </summary>
        /// <returns>Redirects to Home.</returns>
        public ActionResult ChangeLocale(Locales? locale)
        {
            //TODO: Reference from http://afana.me/post/aspnet-mvc-internationalization.aspx in Architecture Document
            //NOTE: Locale data is saved in cookie '_locale'.
            string localeName = LocalesExtension.ParseCultureInfo(locale).Name;

            HttpCookie cookie = Request.Cookies["_locale"];
            if (cookie != null)
                cookie.Value = localeName;   // update cookie value
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