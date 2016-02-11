using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecruitmentSystem.DAL;
using RecruitmentSystem.DAL.Authentication;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Controllers
{
    [PersonAuthorization]
    public class AuthenticationController : Controller
    {
        private RecruitmentContext db = new RecruitmentContext();
        
        [HttpPost]
        public ActionResult Login() {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
