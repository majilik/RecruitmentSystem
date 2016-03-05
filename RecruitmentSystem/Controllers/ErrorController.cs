using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageNotFound()
        {
            return View();
        }

        // GET: Error
        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}