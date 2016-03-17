using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
    /// Controller handles requests that fails, and responds with an Error Page.
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Http GET for 'Page Not Found Error' page.
        /// </summary>
        /// <returns>'Page Not Found' view.</returns>
        public ActionResult PageNotFound()
        {
            return View();
        }

        /// <summary>
        /// Http GET for 'Internal Server Error' page.
        /// </summary>
        /// <returns>'Internal Server Error' view.</returns>
        public ActionResult InternalServerError()
        {
            return View();
        }

        /// <summary>
        /// Http GET for 'Unauthorized' page.
        /// </summary>
        /// <returns>'Unauthorized' view.</returns>
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}