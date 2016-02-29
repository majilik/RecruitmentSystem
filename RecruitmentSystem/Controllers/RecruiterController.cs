using System.Web.Mvc;
using RecruitmentSystem.Resources;
using System.Globalization;
using RecruitmentSystem.Controllers.Base;
using System.Collections.Generic;
using System.Web;
using System;
using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.DAL.Authorization;
using RecruitmentSystem.Security;
using RecruitmentSystem.DAL.Authorization.Interfaces;
using RecruitmentSystem.DAL.Applications;

namespace RecruitmentSystem.Controllers
{
    public class RecruiterController : BaseController
    {

        
      //  [PersonAuthorization]
        // GET: ListApplication
        public ActionResult ListApplications()
        {

            return View(new RecruiterView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListApplications(RecruiterView rView)
        {


            ApplicationManager am = new ApplicationManager();
            rView.Result = (am.FindApplications(rView.SelectedCompetence));
            return View("ListApplications", rView);

        }

        public ActionResult View(long? id)
        {
            return View();
        }
    }

}