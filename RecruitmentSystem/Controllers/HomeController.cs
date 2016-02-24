using System.Web.Mvc;
using System.Globalization;
using RecruitmentSystem.Controllers.Base;
using System.Web;
using System;

namespace RecruitmentSystem.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

    }
}