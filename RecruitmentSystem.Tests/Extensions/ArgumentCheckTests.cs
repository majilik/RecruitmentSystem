using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RecruitmentSystem.Extensions.Tests
{
    [TestClass]
    public class ArgumentCheckTests
    {
        [TestMethod]
        public void ThrowIfNullOrWhiteSpaceDoesNotThrowArgumentExceptionIfValidTest()
        {
            string validArg = "pass";

            NUnit.Framework.Assert.DoesNotThrow(() => validArg.ThrowIfNullOrWhiteSpace());
        }

        [TestMethod]
        public void ThrowIfNullOrWhiteSpaceThrowsArgumentExceptionIfInvalidTest()
        {
            string[] invalidArgs = new string[] { null, "", " " };

            foreach (string invalidArg in invalidArgs)
            {
                NUnit.Framework.Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfNullOrWhiteSpace());
            }
        }

        [TestMethod]
        public void ThrowIfNullOrWhiteSpaceReturnsArgumentIfPassTest()
        {
            string validArg = "pass";

            string returnedValue = validArg.ThrowIfNullOrWhiteSpace();

            NUnit.Framework.Assert.AreEqual(validArg, returnedValue);
        }

        [TestMethod]
        public void ThrowIfLessThanZeroDoesNotThrowArgumentExceptionIfValidTest()
        {
            int validArg = 0;

            NUnit.Framework.Assert.DoesNotThrow(() => validArg.ThrowIfLessThanZero());
        }

        [TestMethod]
        public void ThrowIfLessThanZeroThrowsArgumentExceptionIfInvalidTest()
        {
            int invalidArg = -1;


            NUnit.Framework.Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfLessThanZero());
        }

        [TestMethod]
        public void ThrowIfLessThanZeroReturnsArgumentIfPassTest()
        {
            int validArg = 0;

            int returnedValue = validArg.ThrowIfLessThanZero();

            NUnit.Framework.Assert.AreEqual(validArg, returnedValue);
        }

        [TestMethod]
        public void ThrowIfLessThanOneIntDoesNotThrowArgumentExceptionIfValidTest()
        {
            int validArg = 1;

            NUnit.Framework.Assert.DoesNotThrow(() => validArg.ThrowIfLessThanOne());
        }

        [TestMethod]
        public void ThrowIfLessThanOneIntThrowsArgumentExceptionIfInvalidTest()
        {
            int invalidArg = 0;

            NUnit.Framework.Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfLessThanOne());
        }

        [TestMethod]
        public void ThrowIfLessThanOneIntReturnsArgumentIfPassTest()
        {
            int validArg = 1;

            int returnedValue = validArg.ThrowIfLessThanOne();

            NUnit.Framework.Assert.AreEqual(validArg, returnedValue);
        }

        [TestMethod]
        public void ThrowIfLessThanOneUintDoesNotThrowArgumentExceptionIfValidTest()
        {
            uint validArg = 1;

            NUnit.Framework.Assert.DoesNotThrow(() => validArg.ThrowIfLessThanOne());
        }

        [TestMethod]
        public void ThrowIfLessThanOneUintThrowsArgumentExceptionIfInvalidTest()
        {
            uint invalidArg = 0;

            NUnit.Framework.Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfLessThanOne());
        }

        [TestMethod]
        public void ThrowIfLessThanOneUintReturnsArgumentIfPassTest()
        {
            uint validArg = 1;

            uint returnedValue = validArg.ThrowIfLessThanOne();

            NUnit.Framework.Assert.AreEqual(validArg, returnedValue);
        }
    }
}