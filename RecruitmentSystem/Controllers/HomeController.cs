using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class HomeController : Controller
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