using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using System.Linq;
using RecruitmentSystem.Security;
using RecruitmentSystem.DAL.Authorization.Interfaces;

namespace RecruitmentSystem.DAL.Authorization
{
    //TODO: Document this class in Architecture Document
    public class UserManager : IUserManager
    {
        public UserManager()
        {
            _personQueryService = new QueryService<Person>();
            _roleQueryService = new QueryService<Role>();
        }
        
        QueryService<Person> _personQueryService;
        QueryService<Role> _roleQueryService;

        /// <summary>
        /// Adds a user to the system.
        /// </summary>
        /// <param name="registerView"></param>
        public void AddUser(RegisterView registerView)
        {
            Role defaultRole = _roleQueryService.GetSingle(role => role.Name.Equals("applicant"));

            Person person = new Person()
            {
                Username = registerView.Username,
                Password = SecurityManager.HashPassword(registerView.Password),
                Email = registerView.Email,
                Name = registerView.Name,
                Surname = registerView.Surname,
                Ssn = registerView.Ssn,
                Role = defaultRole
            };

            _personQueryService.Add(person);
        }

        /// <summary>
        /// Determines whether a given Username is in use.
        /// </summary>
        /// <param name="username">Username to check</param>
        /// <returns>Usage status</returns>
        public bool IsUsernameInUse(string username)
        {
            return _personQueryService.GetSingle(person => person.Username.Equals(username)) != null;
        }

        /// <summary>
        /// Determines whether a given User is in a given Role
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="role">Role to check</param>
        /// <returns>User in Role status</returns>
        public bool IsUserInRole(string username, string role)
        {
            return _personQueryService.GetSingle(person =>
                person.Username.Equals(username) &&
                person.Role.Name.Equals(role)) != null;
        }

        /// <summary>
        /// Checks if the users password is correct.
        /// </summary>
        /// <param name="loginView"></param>
        /// <returns>True or False</returns>
        public bool LoginCheck(LoginView loginView)
        {
            string hash = _personQueryService.GetSingle(person => person.Username.Equals(loginView.Username)).Password;

            return SecurityManager.checkPassword(loginView.Password, hash);
        }
    }
}