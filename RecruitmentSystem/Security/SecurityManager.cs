namespace RecruitmentSystem.Security
{
    /// <summary>
    /// Represents a manager that handles password security and validation
    /// and abstracts the implementation details.
    /// </summary>
    public class SecurityManager
    {
        /// <summary>
        /// Hashes a password string.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>hash</returns>
        public static string HashPassword(string password)
        {
            return PasswordHash.CreateHash(password);
        }


        /// <summary>
        /// Validates a password against a hash.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns>True or False</returns>
        public static bool checkPassword(string password, string hash)
        {
            return PasswordHash.ValidatePassword(password, hash);
        }
    }
}