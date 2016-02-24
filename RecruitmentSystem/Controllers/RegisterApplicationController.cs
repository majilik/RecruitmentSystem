using RecruitmentSystem.DAL;
using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    //[PersonAuthorization(new string[] { "applicant" })]
    public class ApplicantController : Controller
    {
        private QueryService<Competence> _competenceQueryService;

        public ApplicantController()
        {
            _competenceQueryService = new QueryService<Competence>();
        }

        // GET: RegisterApplication
        public ActionResult RegisterApplication()
        {
            return View(new ApplicationView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCompetence(ApplicationView view)
        {
            if (ModelState.IsValid)
            {
                view.AddCompetence();
            }

            return View("RegisterApplication", view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAvailability(ApplicationView view)
        {
            if (ModelState.IsValid)
            {
                view.AddAvailability();
            }

            return View("RegisterApplication", view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplicationView view)
        {
            if (ModelState.IsValid)
            {
                //view.AddAvailability();
            }

            return View(new ApplicationView());
        }
    }
}