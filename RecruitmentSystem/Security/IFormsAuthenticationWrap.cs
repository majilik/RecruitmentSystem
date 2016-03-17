namespace RecruitmentSystem.Security
{
    /// <summary>
    /// Represents a wrapper of a set or subset of <see cref="FormsAuthentication"/> methods.
    /// </summary>
    public interface IFormsAuthenticationWrap
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
    }
}