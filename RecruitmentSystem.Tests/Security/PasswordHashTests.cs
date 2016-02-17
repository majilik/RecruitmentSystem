﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;

namespace RecruitmentSystem.Security.Tests
{
    [TestClass]
    public class PasswordHashTests
    {
        string validArg;
        string[] invalidArgs;

        [TestInitialize]
        public void TestInitialize()
        {
            validArg = "pass";
            invalidArgs = new string[] { null, "", " " };
        }

        [TestCleanup]
        public void TestCleanp()
        {
            validArg = null;
            invalidArgs = null;
        }

        [TestMethod]
        public void IllegalArgumentInCreateHashTest()
        {
            PasswordHash.CreateHash(validArg);

            foreach (string invalidArg in invalidArgs)
            {
                NUnit.Framework.Assert.That(() => PasswordHash.CreateHash(invalidArg),
                    Throws.TypeOf<ArgumentException>());
            }
        }

        [TestMethod]
        public void CreateHashReturnsProperlyFormattedHashTest()
        {
            string hash = PasswordHash.CreateHash(validArg);
            string[] hashParts = hash.Split('$');

            NUnit.Framework.Assert.AreEqual(hashParts.Length, 3);
            NUnit.Framework.Assert.DoesNotThrow(() => int.Parse(hashParts[0]));
            NUnit.Framework.Assert.DoesNotThrow(() => Convert.FromBase64String(hashParts[1]));
            NUnit.Framework.Assert.DoesNotThrow(() => Convert.FromBase64String(hashParts[2]));
        }

        [TestMethod]
        public void IllegalArgumentInValidatePasswordTest()
        {
            foreach (string invalidArg in invalidArgs)
            {
                NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(invalidArg, validArg),
                    Throws.TypeOf<ArgumentException>());

                NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(invalidArg, validArg),
                    Throws.TypeOf<ArgumentException>());
            }
        }

        [TestMethod]
        public void ValidatePasswordReturnsTrueWhenComparingAHashedStringWithTheOriginalStringTest()
        {
            string hash = PasswordHash.CreateHash(validArg);
            bool result = PasswordHash.ValidatePassword(validArg, hash);
            NUnit.Framework.Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatePasswordReturnsFalseWhenComparingAHashedStringWithOtherThanTheOriginalStringTest()
        {
            string otherValidArg = "otherPass";
            string hash = PasswordHash.CreateHash(validArg);
            bool result = PasswordHash.ValidatePassword(otherValidArg, hash);
            NUnit.Framework.Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedHashTest()
        {
            string properHash = PasswordHash.CreateHash(validArg);
            string[] hashParts = properHash.Split('$');

            string improperLength = hashParts[0];
            NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(validArg, improperLength),
                Throws.TypeOf<ArgumentException>());
        }

        [TestMethod]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedIterationsTest()
        {
            string properHash = PasswordHash.CreateHash(validArg);
            string[] hashParts = properHash.Split('$');

            hashParts[0] = "-10000";
            string negativeIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(validArg, negativeIterations),
                Throws.TypeOf<ArgumentException>());

            hashParts[0] = int.MaxValue.ToString() + "0";
            string overflowIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(validArg, overflowIterations),
                Throws.TypeOf<ArgumentException>());

            hashParts[0] = "NaN";
            string nanIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(validArg, nanIterations),
                Throws.TypeOf<ArgumentException>());
        }

        [TestMethod]
        public void ValidatePasswordThrowsArgumentExceptionForImproperlyFormattedSaltAndHashTest()
        {
            string properHash = PasswordHash.CreateHash(validArg);
            string[] hashParts = properHash.Split('$');

            hashParts[1] = "notbase64";
            string negativeIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(validArg, negativeIterations),
                Throws.TypeOf<ArgumentException>());

            hashParts[2] = "notbase64";
            string overflowIterations = string.Join("$", hashParts);
            NUnit.Framework.Assert.That(() => PasswordHash.ValidatePassword(validArg, overflowIterations),
                Throws.TypeOf<ArgumentException>());
        }
    }
}