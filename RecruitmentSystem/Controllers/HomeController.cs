using System.Web.Mvc;
using RecruitmentSystem.Controllers.Base;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
    /// Controller handles requests to the Homepage.
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Http GET for Index page. 
        /// This page is the default page of the application.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}