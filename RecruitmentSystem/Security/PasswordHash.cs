using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using RecruitmentSystem.Extensions;
using System;
using System.Security.Cryptography;

namespace RecruitmentSystem.Security
{
    /// <summary>
    /// Salted password hashing with PBKDF2-SHA56.
    /// </summary>
    public static class PasswordHash
    {
        private const int SaltByteSize = 32;
        private const int HashByteSize = 32;
        private const int Iterations = 10000;

        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int HashIndex = 2;

        private static RNGCryptoServiceProvider _csprng = 
            new RNGCryptoServiceProvider();

        /// <summary>
        /// Creates a salted PBKDF2 hash of the password, together with the
        /// number of iterations used in case this changes in the future.
        /// Generates a new random salt each time.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="CryptographicException">
        /// The system might not support this operation.</exception>
        public static string CreateHash(string password)
        {
            password.ThrowIfNullOrWhiteSpace();

            byte[] salt = new byte[SaltByteSize];
            _csprng.GetBytes(salt);

            byte[] hash = PBKDF2(password, salt, Iterations, HashByteSize);

            return Iterations + "$" +
                Convert.ToBase64String(salt) + "$" +
                Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <returns>True if the password is correct, else false.</returns>
        /// <exception cref="ArgumentException">Thrown if either argument is
        /// null, empty or whitespace. Also thrown if
        /// <paramref name="correctHash"/> is not on the form
        /// iterations$Base64.salt$Base64.hash.</exception>
        public static bool ValidatePassword(string password, string correctHash)
        {
            password.ThrowIfNullOrWhiteSpace();
            correctHash.ThrowIfNullOrWhiteSpace();

            string[] split = correctHash.Split('$');
            if (split.Length != 3)
                throw new ArgumentException("The hash was not properly"
                    + " formatted.");

            uint iterations;
            byte[] salt, hash;
            try {
                iterations = uint.Parse(split[IterationIndex]);
                salt = Convert.FromBase64String(split[SaltIndex]);
                hash = Convert.FromBase64String(split[HashIndex]);
            } catch (Exception ex) when (ex is ArgumentException ||
                ex is FormatException || ex is OverflowException)
            {
                throw new ArgumentException("Hash is not on the form"
                    +" iterations$Base64.salt$Base64.hash.", ex);
            }

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first hash.</param>
        /// <param name="b">The second hash.</param>
        /// <returns>True if both byte arrays are equal, else false</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint) a.Length ^ (uint) b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint) (a[i] ^ b[i]);

            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA256 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length in bytes of the hash to
        /// generate.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt,
            uint iterations, int outputBytes)
        {
            Pkcs5S2ParametersGenerator gen =
                new Pkcs5S2ParametersGenerator(new Sha256Digest());

            gen.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(
                password.ToCharArray()), salt, Iterations);

            KeyParameter key = (KeyParameter) gen
                .GenerateDerivedMacParameters(HashByteSize * 8);

            return key.GetKey();
        }
    }
}