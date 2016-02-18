using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;

namespace RecruitmentSystem.Security.Tests
{
    [TestClass]
    public class PasswordHashTests
    {
        private string _validArg;
        private string[] _invalidArgs;

        [TestInitialize]
        public void TestInitialize()
        {
            _validArg = "pass";
            _invalidArgs = new string[] { null, "", " " };
        }

        [TestCleanup]
        public void TestCleanp()
        {
            _validArg = null;
            _invalidArgs = null;
        }

        [TestMethod]
        public void CreateHashThrowsArgumentExceptionTest()
        {
            PasswordHash.CreateHash(_validArg);

            foreach (string invalidArg in _invalidArgs)
            {
                NUnit.Framework.Assert.Throws<ArgumentException>(() => PasswordHash.CreateHash(invalidArg));
            }
        }

        [TestMethod]
        public void CreateHashReturnsProperlyFormattedHashTest()
        {
            string hash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = hash.Split('$');

            NUnit.Framework.Assert.AreEqual(hashParts.Length, 3);
            NUnit.Framework.Assert.DoesNotThrow(() => int.Parse(hashParts[0]));
            NUnit.Framework.Assert.DoesNotThrow(() => Convert.FromBase64String(hashParts[1]));
            NUnit.Framework.Assert.DoesNotThrow(() => Convert.FromBase64String(hashParts[2]));
        }

        [TestMethod]
        public void ValidatePasswordThrowsArgumentExceptionTest()
        {
            foreach (string invalidArg in _invalidArgs)
            {
                NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                    PasswordHash.ValidatePassword(invalidArg, _validArg));

                NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                    PasswordHash.ValidatePassword(invalidArg, _validArg));
            }
        }

        [TestMethod]
        public void ValidatePasswordReturnsTrueWhenComparingAHashedStringWithTheOriginalStringTest()
        {
            string hash = PasswordHash.CreateHash(_validArg);

            bool result = PasswordHash.ValidatePassword(_validArg, hash);

            NUnit.Framework.Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatePasswordReturnsFalseWhenComparingAHashedStringWithOtherThanTheOriginalStringTest()
        {
            string otherValidArg = "otherPass";
            string hash = PasswordHash.CreateHash(_validArg);

            bool result = PasswordHash.ValidatePassword(otherValidArg, hash);

            NUnit.Framework.Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedHashTest()
        {
            string properHash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = properHash.Split('$');

            string improperLength = hashParts[0];
            NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, improperLength));
        }

        [TestMethod]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedIterationsTest()
        {
            string properHash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = properHash.Split('$');

            hashParts[0] = "-10000";
            string negativeIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, negativeIterations));

            hashParts[0] = int.MaxValue.ToString() + "0";
            string overflowIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, overflowIterations));

            hashParts[0] = "NaN";
            string nanIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, nanIterations));
        }

        [TestMethod]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedSaltAndHashTest()
        {
            string properHash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = properHash.Split('$');

            hashParts[1] = "notbase64";
            string negativeIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, negativeIterations));

            hashParts[2] = "notbase64";
            string overflowIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, overflowIterations));
        }
    }
}