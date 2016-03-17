using RecruitmentSystem.Models.ViewModel;

namespace RecruitmentSystem.DAL.Authorization.Interfaces
{
    /// <summary>
    /// Represents a set of methods for authenticating a users existence and status.
    /// </summary>
    public interface IUserManager
    {
        void AddUser(RegisterView registerView);
        bool IsUsernameInUse(string username);
        bool IsUserInRole(string username, string role);
        bool LoginCheck(LoginView loginView);
    }
}
