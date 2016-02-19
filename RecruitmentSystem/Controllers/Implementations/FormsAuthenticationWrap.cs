using System;
using System.Web.Security;

namespace RecruitmentSystem.Controllers
{
    public class FormsAuthenticationWrap : IFormsAuthenticationWrap
    {
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}