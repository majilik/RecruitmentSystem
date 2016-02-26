using System.Web.Mvc;
using RecruitmentSystem.Resources;
using System.Globalization;
using RecruitmentSystem.Controllers.Base;
using System.Web;
using System;
using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.DAL.Authorization;
using RecruitmentSystem.Security;
using RecruitmentSystem.DAL.Authorization.Interfaces;

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

        public ActionResult Search(RecruiterView rView)
        {
          

            return View();
        }
    }

}