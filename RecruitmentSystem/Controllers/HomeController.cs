using System.Web.Mvc;
using RecruitmentSystem.Controllers.Base;

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