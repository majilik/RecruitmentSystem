using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecruitmentSystem.Security;


namespace RecruitmentSystem.DAL.Authorization
{
    //TODO: Document this class in Architecture Document
    public class UserManager
    {
        //TODO: Put DEFAULT_ROLE_ON_CREATION in global settings?
        private const string DEFAULT_ROLE_ON_CREATION = "applicant";

        /// <summary>
        /// Adds a User to the system.
        /// </summary>
        public void AddUser(Person person)
        {
            using (RecruitmentContext db = new RecruitmentContext())
            {
                person.Role = db.Roles.Where(role => role.Name.Equals(DEFAULT_ROLE_ON_CREATION)).Single();
                person.Password = SecurityManager.PasswordHash(person.Password);
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Determines whether a given Username is in use.
        /// </summary>
        /// <param name="username">Username to check</param>
        /// <returns>Usage status</returns>
        public bool IsUsernameInUse(string username)
        {
            using (RecruitmentContext db = new RecruitmentContext())
            {
                return db.Persons.Where(person =>
                    person.Username.Equals(username)).Any();
            }
        }

        /// <summary>
        /// Determines whether a given User is in a given Role
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="role">Role to check</param>
        /// <returns>User in Role status</returns>
        public bool IsUserInRole(string username, string role)
        {
            using (RecruitmentContext db = new RecruitmentContext())
            {
                return db.Persons.Where(person =>
                    person.Username.Equals(username) &&
                    person.Role.Name.Equals(role)).Any();
            }
        }


    }
}