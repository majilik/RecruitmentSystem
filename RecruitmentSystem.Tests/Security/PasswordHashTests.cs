using NUnit.Framework;
using System;

namespace RecruitmentSystem.Security.Tests
{
    [TestFixture]
    public class PasswordHashTests
    {
        private string _validArg;
        private string[] _invalidArgs;

        [SetUp]
        public void TestInitialize()
        {
            _validArg = "pass";
            _invalidArgs = new string[] { null, "", " " };
        }

        [TearDown]
        public void TestCleanp()
        {
            _validArg = null;
            _invalidArgs = null;
        }

        [Test]
        public void CreateHashThrowsArgumentExceptionTest()
        {
            PasswordHash.CreateHash(_validArg);

            foreach (string invalidArg in _invalidArgs)
            {
                Assert.Throws<ArgumentException>(() => PasswordHash.CreateHash(invalidArg));
            }
        }

        [Test]
        public void CreateHashReturnsProperlyFormattedHashTest()
        {
            string hash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = hash.Split('$');

            Assert.AreEqual(hashParts.Length, 3);
            Assert.DoesNotThrow(() => int.Parse(hashParts[0]));
            Assert.DoesNotThrow(() => Convert.FromBase64String(hashParts[1]));
            Assert.DoesNotThrow(() => Convert.FromBase64String(hashParts[2]));
        }

        [Test]
        public void ValidatePasswordThrowsArgumentExceptionTest()
        {
            foreach (string invalidArg in _invalidArgs)
            {
                Assert.Throws<ArgumentException>(() =>
                    PasswordHash.ValidatePassword(invalidArg, _validArg));

                Assert.Throws<ArgumentException>(() =>
                    PasswordHash.ValidatePassword(invalidArg, _validArg));
            }
        }

        [Test]
        public void ValidatePasswordReturnsTrueWhenComparingAHashedStringWithTheOriginalStringTest()
        {
            string hash = PasswordHash.CreateHash(_validArg);

            bool result = PasswordHash.ValidatePassword(_validArg, hash);

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidatePasswordReturnsFalseWhenComparingAHashedStringWithOtherThanTheOriginalStringTest()
        {
            string otherValidArg = "otherPass";
            string hash = PasswordHash.CreateHash(_validArg);

            bool result = PasswordHash.ValidatePassword(otherValidArg, hash);

            Assert.IsFalse(result);
        }

        [Test]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedHashTest()
        {
            string properHash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = properHash.Split('$');

            string improperLength = hashParts[0];
            Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, improperLength));
        }

        [Test]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedIterationsTest()
        {
            string properHash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = properHash.Split('$');

            hashParts[0] = "-10000";
            string negativeIterations = string.Join("$", hashParts);
            Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, negativeIterations));

            hashParts[0] = int.MaxValue.ToString() + "0";
            string overflowIterations = string.Join("$", hashParts);
            Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, overflowIterations));

            hashParts[0] = "NaN";
            string nanIterations = string.Join("$", hashParts);
            Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, nanIterations));
        }

        [Test]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedSaltAndHashTest()
        {
            string properHash = PasswordHash.CreateHash(_validArg);
            string[] hashParts = properHash.Split('$');

            hashParts[1] = "notbase64";
            string negativeIterations = string.Join("$", hashParts);
            Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, negativeIterations));

            hashParts[2] = "notbase64";
            string overflowIterations = string.Join("$", hashParts);
            Assert.Throws<ArgumentException>(() =>
                PasswordHash.ValidatePassword(_validArg, overflowIterations));
        }
    }
}