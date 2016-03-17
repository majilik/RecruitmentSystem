using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.DAL;
using RecruitmentSystem.DAL.Query;
using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
    /// Controller handles requests for 'applicant' users.
    /// </summary>
    [PersonAuthorization("applicant")]
    public class ApplicantController : BaseController
    {
        private readonly QueryService<Application> _applicationQueryService;
        private readonly QueryService<Competence> _competenceQueryService;
        private readonly QueryService<Person> _personQueryService;

        /// <summary>
        /// Constructs the ApplicationController and initializes the database access objects
        /// necessary to query the database.
        /// </summary>
        public ApplicantController()
        {
            _applicationQueryService = new QueryService<Application>();
            _competenceQueryService = new QueryService<Competence>();
            _personQueryService = new QueryService<Person>();
        }

        /// <summary>
        /// HTTP Get for the view named RegisterApplication. Initializes an
        /// <see cref="ApplicationView"/> for the returned View.
        /// </summary>
        /// <returns>The RegisterApplication <see cref="ViewResult"/> initialized
        /// with the <see cref="ApplicationView"/> model.</returns>
        public ActionResult RegisterApplication()
        {
            string username = HttpContext.User.Identity.Name;
            Person applicant = _personQueryService.GetSingle(p => p.Username == username);
            Application application = _applicationQueryService.GetSingle(a => a.Person.Id == applicant.Id, a => a.Person);
            if (application != null)
            {
                return RedirectToAction("Success", "Applicant");
            }

            return View(new ApplicationView(_competenceQueryService.GetAll()));
        }

        /// <summary>
        /// Takes the current ApplicationView when submitted and processes the data.
        /// The application is then stored for the currently authorized user.
        /// </summary>
        /// <param name="applicationView">The current <see cref="ApplicationView"/>.</param>
        /// <returns>Returns a new <see cref="ApplicationView"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplicationView applicationView)
        {
            if (ModelState.IsValid && HttpContext != null)
            {
                string username = HttpContext.User.Identity.Name;
                CreateApplication.Invoke(username, applicationView.SelectedCompetences, applicationView.SelectedAvailabilities);
            }

            return RedirectToAction("Success", "Applicant");
        }

        /// <summary>
        /// Http GET for the Success view shown after a successful application has been created.
        /// </summary>
        /// <returns>The success view.</returns>
        public ActionResult Success()
        {
            return View();
        }
    }
}