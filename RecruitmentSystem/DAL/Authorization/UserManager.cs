using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;
using RecruitmentSystem.DAL.Authorization.Interfaces;
using RecruitmentSystem.Extensions;
using System;

namespace RecruitmentSystem.DAL.Authorization
{
    /// <summary>
    /// Represents an implementation of <see cref="IUserManager"/>.
    /// </summary>
    public class UserManager : IUserManager
    {
        private static readonly QueryService<Person> _personQueryService = new QueryService<Person>();
        private static readonly QueryService<Role> _roleQueryService = new QueryService<Role>();

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public UserManager()
        {
        }

        /// <summary>
        /// Adds a user to the system.
        /// </summary>
        /// <param name="registerView"></param>
        public void AddUser(RegisterView registerView)
        {
            Person person = new Person()
            {
                Username = registerView.Username,
                Password = SecurityManager.HashPassword(registerView.Password),
                Email = registerView.Email,
                Name = registerView.Name,
                Surname = registerView.Surname,
                Ssn = registerView.Ssn,
                Role = _roleQueryService.GetSingle(role => role.Name.Equals("applicant"))
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
            try
            {
                username.ThrowIfNullOrWhiteSpace();
            }
            catch (ArgumentException)
            {
                return false;
            }

            return _personQueryService.GetSingle(p => p.Username == username) != null;
        }

        /// <summary>
        /// Determines whether a given User is in a given Role
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="role">Role to check</param>
        /// <returns>User in Role status</returns>
        public bool IsUserInRole(string username, string role)
        {
            try
            {
                username.ThrowIfNullOrWhiteSpace();
                role.ThrowIfNullOrWhiteSpace();
            }
            catch (ArgumentException)
            {
                return false;
            }

            return _personQueryService.GetSingle(
                p => p.Username == username && p.Role.Name.Equals(role),
                p => p.Role) != null;
        }

        /// <summary>
        /// Checks if the users password is correct.
        /// </summary>
        /// <param name="loginView"></param>
        /// <returns>True or False</returns>
        public bool LoginCheck(LoginView loginView)
        {
            try
            {
                loginView.Username.ThrowIfNullOrWhiteSpace();
                loginView.Password.ThrowIfNullOrWhiteSpace();
            }
            catch (ArgumentException)
            {
                return false;
            }
            string hash = _personQueryService.GetSingle(person => person.Username == loginView.Username).Password;

            return SecurityManager.checkPassword(loginView.Password, hash);
        }
    }
}