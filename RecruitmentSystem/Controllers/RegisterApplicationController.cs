using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.DAL;
using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
    /// </summary>
    //[PersonAuthorization("applicant")]
    public class ApplicantController : BaseController
    {
        private QueryService<Competence> _competenceQueryService;
        private QueryService<CompetenceProfile> _competenceProfileQueryService;
        private QueryService<Person> _personQueryService;

        /// <summary>
        /// Constructs the ApplicationController and initializes the database access objects
        /// necessary to query the database.
        /// </summary>
        public ApplicantController()
        {
            _competenceQueryService = new QueryService<Competence>();
            _competenceProfileQueryService = new QueryService<CompetenceProfile>();
            _personQueryService = new QueryService<Person>();
        }

        /// <summary>
        /// HTTP Get for the view named RegisterApplication. Initializes an
        /// <see cref="ApplicationView"/> for the returned <see cref="View"/>.
        /// </summary>
        /// <returns>The RegisterApplication <see cref="ViewResult"/> initialized
        /// with the <see cref="ApplicationView"/> model.</returns>
        public ActionResult RegisterApplication()
        {
            return View(new ApplicationView(_competenceQueryService.GetAll()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCompetence([Bind(Include = "SelectedCompetence,SelectedYearsOfExperience,SelectedCompetences,Competences,_competences")] ApplicationView view)
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

        /// <summary>
        /// Takes the current ApplicationView when submitted and processes the data.
        /// The application is then stored for the currently authorized user.
        /// </summary>
        /// <param name="view">The current <see cref="ApplicationView"/>.</param>
        /// <returns>Returns an new <see cref="ApplicationView"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplicationView view)
        {
            if (ModelState.IsValid)
            {
                string username = HttpContext.User.Identity.Name;
                Person applicant = _personQueryService.GetSingle(e => e.Name.Equals(username));
                List<CompetenceProfile> competenceProfiles = new List<CompetenceProfile>();

                foreach (KeyValuePair<Competence, decimal> entry in view.SelectedCompetences)
                {
                    competenceProfiles.Add(new CompetenceProfile
                        {
                            //Person = applicant, Competence = entry.Key, YearsOfExperience = entry.Value
                        });
                }

                _competenceProfileQueryService.Add(competenceProfiles.ToArray());
            }

            return View(new ApplicationView());
        }

        public ActionResult Delete(ApplicationView view)
        {
            if (ModelState.IsValid)
            {
                //view.RemoveCompetence(competence);
            }

            return View("RegisterApplication", new ApplicationView());
        }
    }
}