using RecruitmentSystem.Models.ViewModel;

namespace RecruitmentSystem.DAL.Authorization.Interfaces
{
    public interface IUserManager
    {
        void AddUser(RegisterView registerView);
        bool IsUsernameInUse(string username);
        bool IsUserInRole(string username, string role);
        bool LoginCheck(LoginView loginView);
    }
}
