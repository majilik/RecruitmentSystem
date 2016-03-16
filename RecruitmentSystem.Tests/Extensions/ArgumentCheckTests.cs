using NUnit.Framework;
using System;

namespace RecruitmentSystem.Extensions.Tests
{
    [TestFixture]
    public class ArgumentCheckTests
    {
        [Test]
        public void ThrowIfNullOrWhiteSpaceDoesNotThrowArgumentExceptionIfValidTest()
        {
            string validArg = "pass";

            Assert.DoesNotThrow(() => validArg.ThrowIfNullOrWhiteSpace());
        }

        [Test]
        public void ThrowIfNullOrWhiteSpaceThrowsArgumentExceptionIfInvalidTest()
        {
            string[] invalidArgs = new string[] { null, "", " " };

            foreach (string invalidArg in invalidArgs)
            {
                Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfNullOrWhiteSpace());
            }
        }

        [Test]
        public void ThrowIfNullOrWhiteSpaceReturnsArgumentIfPassTest()
        {
            string validArg = "pass";

            string returnedValue = validArg.ThrowIfNullOrWhiteSpace();

            Assert.AreEqual(validArg, returnedValue);
        }

        [Test]
        public void ThrowIfLessThanZeroDoesNotThrowArgumentExceptionIfValidTest()
        {
            int validArg = 0;

            Assert.DoesNotThrow(() => validArg.ThrowIfLessThanZero());
        }

        [Test]
        public void ThrowIfLessThanZeroThrowsArgumentExceptionIfInvalidTest()
        {
            int invalidArg = -1;

            Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfLessThanZero());
        }

        [Test]
        public void ThrowIfLessThanZeroReturnsArgumentIfPassTest()
        {
            int validArg = 0;

            int returnedValue = validArg.ThrowIfLessThanZero();

            Assert.AreEqual(validArg, returnedValue);
        }

        [Test]
        public void ThrowIfLessThanOneIntDoesNotThrowArgumentExceptionIfValidTest()
        {
            int validArg = 1;

            Assert.DoesNotThrow(() => validArg.ThrowIfLessThanOne());
        }

        [Test]
        public void ThrowIfLessThanOneIntThrowsArgumentExceptionIfInvalidTest()
        {
            int invalidArg = 0;

            Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfLessThanOne());
        }

        [Test]
        public void ThrowIfLessThanOneIntReturnsArgumentIfPassTest()
        {
            int validArg = 1;

            int returnedValue = validArg.ThrowIfLessThanOne();

            Assert.AreEqual(validArg, returnedValue);
        }

        [Test]
        public void ThrowIfLessThanOneUintDoesNotThrowArgumentExceptionIfValidTest()
        {
            uint validArg = 1;

            Assert.DoesNotThrow(() => validArg.ThrowIfLessThanOne());
        }

        [Test]
        public void ThrowIfLessThanOneUintThrowsArgumentExceptionIfInvalidTest()
        {
            uint invalidArg = 0;

            Assert.Throws<ArgumentException>(() => invalidArg.ThrowIfLessThanOne());
        }

        [Test]
        public void ThrowIfLessThanOneUintReturnsArgumentIfPassTest()
        {
            uint validArg = 1;

            uint returnedValue = validArg.ThrowIfLessThanOne();

            Assert.AreEqual(validArg, returnedValue);
        }
    }
}