using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.Security;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Controller for Homepage.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET for Index page
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET for Unauthorized page
        /// </summary>
        /// <returns>Unauthorized View</returns>
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}