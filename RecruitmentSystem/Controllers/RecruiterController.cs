﻿using System.Web.Mvc;
using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.DAL.Applications;
using RecruitmentSystem.Security;

namespace RecruitmentSystem.Controllers
{
    [PersonAuthorization("recruiter")]
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
            rView.Result = (am.FindApplications());
            return View("ListApplications", rView);

        }

    }
}