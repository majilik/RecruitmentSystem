namespace RecruitmentSystem.Security
{
    public interface IFormsAuthenticationWrap
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
