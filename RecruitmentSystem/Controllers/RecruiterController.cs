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
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
    /// Controller handles requests for 'recruiter' users.
    /// </summary>
    [PersonAuthorization("recruiter")]
    public class RecruiterController : BaseController
    {
        private readonly QueryService<Application> _applicationQueryService;

        /// <summary>
        /// Default constructor, initializes query service objects for database access.
        /// </summary>
        public RecruiterController()
        {
            _applicationQueryService = new QueryService<Application>();
        }

        /// <summary>
        /// Http GET for List Applications view.
        /// </summary>
        /// <returns>List Applications view.</returns>
        public ActionResult ListApplications()
        {
            return View(new RecruiterView());
        }

        /// <summary>
        /// Http POST for List Applications view.
        /// Gets application data from database,
        /// filtered with selected filters.
        /// </summary>
        /// <param name="rView">RecruiterView containing filters.</param>
        /// <returns>List Applications view, updated with applications data.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListApplications(RecruiterView rView)
        {
            IEnumerable<Application> applications =
                _applicationQueryService.GetAll(a => a.Availabilities, a => a.Person, a => a.CompetenceProfiles);
            rView.Result = (applications.ToList());
            return View("ListApplications", rView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Check(long? id)
        {
            return View(new RecruiterView());
        }
    }
}