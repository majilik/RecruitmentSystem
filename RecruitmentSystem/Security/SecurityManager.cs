using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace RecruitmentSystem.Security
{
    //TODO: Document this class in Architecture Document
    public class SecurityManager
    {
        internal static string salt = "34Sd6hB_/cx#";

        /// <summary>
        /// Hashes a password string.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string PasswordHash(string password)
        {
            SHA256Managed hash = new SHA256Managed();
            byte[] passwordData = Encoding.Unicode.GetBytes(password + salt);
            byte[] hashedPasswordData = hash.TransformFinalBlock(passwordData, 0, passwordData.Length);
            return Encoding.Unicode.GetString(hashedPasswordData);
        }
    }
}