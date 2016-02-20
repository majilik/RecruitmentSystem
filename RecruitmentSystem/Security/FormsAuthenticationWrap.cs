using System.Web.Security;

namespace RecruitmentSystem.Security
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