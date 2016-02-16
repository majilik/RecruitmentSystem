using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Security.Cryptography;

namespace RecruitmentSystem.Security
{
    /// <summary>
    /// Salted password hashing with PBKDF2-SHA1.
    /// Author: godtopus
    /// Compatibility: TBD
    /// </summary>
    public static class PasswordHash
    {
        // The following constants may be changed without breaking existing hashes.
        private const int SaltByteSize = 32;
        private const int HashByteSize = 32;
        private const int Iterations = 10000;

        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int HashIndex = 2;

        private static RNGCryptoServiceProvider CSPRNG = new RNGCryptoServiceProvider();

        /// <summary>
        /// Creates a salted PBKDF2 hash of the password, together with the
        /// number of iterations used in case this changes in the future.
        /// Generates a new random salt each time.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password.</returns>
        public static string CreateHash(string password)
        {
            byte[] salt = new byte[SaltByteSize];
            CSPRNG.GetBytes(salt);

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
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            string[] split = correctHash.Split('$');
            int iterations = Int32.Parse(split[IterationIndex]);
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[HashIndex]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint) a.Length ^ (uint) b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++) diff |= (uint) (a[i] ^ b[i]);
            Array.Clear(a, 0, a.Length);
            Array.Clear(b, 0, b.Length);
            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA256 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Pkcs5S2ParametersGenerator gen = new Pkcs5S2ParametersGenerator(new Sha256Digest());
            gen.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray()), salt, Iterations);
            KeyParameter key = (KeyParameter) gen.GenerateDerivedMacParameters(HashByteSize * 8);
            return key.GetKey();
        }
    }
}