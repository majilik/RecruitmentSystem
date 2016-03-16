﻿using System.Web.Security;

namespace RecruitmentSystem.Security
{
    /// <summary>
    /// Represents an implementation of <see cref="IFormsAuthenticationWrap"/>.
    /// </summary>
    public class FormsAuthenticationWrap : IFormsAuthenticationWrap
    {
        /// <summary>
        /// Creates an authentication ticket for the supplied user name and adds it to the
        /// cookies collection of the response, or to the URL if you are using cookieless
        /// authentication.
        /// </summary>
        /// <param name="userName">The name of an authenticated user. This does not have to map to a Windows account.</param>
        /// <param name="createPersistentCookie">true to create a persistent cookie (one that is saved across browser sessions);
        /// otherwise, false.</param>
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        /// <summary>
        /// Removes the forms-authentication ticket from the browser.
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}