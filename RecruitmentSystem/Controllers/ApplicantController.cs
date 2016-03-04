﻿using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.DAL;
using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;
using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
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
        /// <param name="view">The current <see cref="ApplicationView"/>.</param>
        /// <returns>Returns a new <see cref="ApplicationView"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplicationView applicationView)
        {
            if (ModelState.IsValid)
            {
                string username = HttpContext.User.Identity.Name;

                using (RecruitmentContext context = new RecruitmentContext())
                {
                    IList<Competence> competences = context.Competences.ToList();
                    Person applicant = context.Persons.Single(p => p.Username == username);

                    Application application =
                        new Application
                        {
                            ApplicationDate = DateTime.Now,
                            Availabilities = applicationView.SelectedAvailabilities
                                .Aggregate(new List<Availability>(), (accumulator, entry) =>
                                {
                                    accumulator.Add(new Availability { FromDate = entry.Key, ToDate = entry.Value });
                                    return accumulator;
                                }),
                            CompetenceProfiles = applicationView.SelectedCompetences
                                .Aggregate(new List<CompetenceProfile>(), (accumulator, entry) =>
                                {
                                    accumulator.Add(new CompetenceProfile
                                    {
                                        Competence = competences.Single(c => c.Id == entry.Key),
                                        YearsOfExperience = entry.Value
                                    }
                                        );
                                    return accumulator;
                                }),
                            Person = applicant
                        };

                    context.Applications.Add(application);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Success", "Applicant");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}