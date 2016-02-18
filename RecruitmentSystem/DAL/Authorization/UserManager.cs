﻿using RecruitmentSystem.Models;
using RecruitmentSystem.Models.ViewModel;
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
        private const string DefaultRoleOnCreation = "applicant";

        /// <summary>
        /// Adds a User to the system.
        /// </summary>
        public void AddUser(RegisterView registerView)
        {
            using (RecruitmentContext db = new RecruitmentContext())
            {
                Person person = new Person()
                {
                    Username = registerView.Username,
                    Password = SecurityManager.HashPassword(registerView.Password),
                    Email = registerView.Email,
                    Name = registerView.Name,
                    Surname = registerView.Surname,
                    Ssn = registerView.Ssn,
                    Role = db.Roles.Where(role => 
                        role.Name.Equals(DefaultRoleOnCreation)).Single()
                };
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

        public bool LoginCheck(LoginView loginView)
        {
            using(RecruitmentContext db = new RecruitmentContext())
            {
                string hash = db.Persons.Where(person => person.Username.Equals(loginView.Username)).Select(person => person.Password).Single();
                return SecurityManager.checkPassword(loginView.Password, hash);

            }
        }


    }
}