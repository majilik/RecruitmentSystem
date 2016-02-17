namespace RecruitmentSystem.Security
{
    //TODO: Document this class in Architecture Document
    public class SecurityManager
    {
        /// <summary>
        /// Hashes a password string.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            return PasswordHash.CreateHash(password);
        }

        public static bool checkPassword(string password, string hash)
        {
            return PasswordHash.ValidatePassword(password, hash);
        }
    }
}