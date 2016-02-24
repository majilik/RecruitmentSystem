using RecruitmentSystem.Resources;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers.Base
{
    /// <summary>
    /// Extends the Controller class with Locales functionality.
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string culture = null;

            HttpCookie cookie = Request.Cookies["_locale"];

            if (cookie != null)
            {
                culture = cookie.Value;
            }
            else
            {
                //Get the first prefered locale that is implemented.
                string[] preferedLocales = Request.UserLanguages;
                foreach(string locale in preferedLocales)
                {
                    if (LocalesExtension.IsLocaleImplemented(locale))
                    {
                        culture = locale;
                        break;
                    }
                }
            }

            //Set Culture to the parsed culture.
            Thread.CurrentThread.CurrentUICulture = 
                Thread.CurrentThread.CurrentCulture = LocalesExtension.ParseCultureInfo(culture);

            //Run base functionality.
            return base.BeginExecuteCore(callback, state);
        }
    }
}