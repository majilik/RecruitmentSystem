namespace RecruitmentSystem.Controllers
{
    public interface IFormsAuthenticationWrap
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);

        void SignOut();
    }
}
