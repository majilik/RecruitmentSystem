using System.Web.Mvc;
using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Models;
using RecruitmentSystem.DAL;
using System.Collections.Generic;
using System.Linq;
using RecruitmentSystem.Security;

namespace RecruitmentSystem.Controllers
{
    [PersonAuthorization("recruiter")]
    public class RecruiterController : BaseController
    {
        private readonly QueryService<Application> _applicationQueryService;

        public RecruiterController()
        {
            _applicationQueryService = new QueryService<Application>();
        }

        // GET: ListApplication
        public ActionResult ListApplications()
        {
            return View(new RecruiterView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListApplications(RecruiterView rView)
        {
            IEnumerable<Application> applications =
                _applicationQueryService.GetAll(a => a.Availabilities, a => a.Person, a => a.CompetenceProfiles);
            rView.Result = (applications.ToList());
            return View("ListApplications", rView);
        }

        public ActionResult Check(long? id)
        {
            return View(new RecruiterView());
        }
    }
}