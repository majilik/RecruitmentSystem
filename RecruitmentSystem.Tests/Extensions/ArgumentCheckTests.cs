using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RecruitmentSystem.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentSystem.Extensions.Tests
{
    [TestClass]
    public class ArgumentCheckTests
    {
        [TestMethod]
        public void ThrowIfNullOrWhiteSpaceDoesNotThroArgumentExceptionIfValidTest()
        {
            string validArg = "pass";

            NUnit.Framework.Assert.DoesNotThrow(() => validArg
                .ThrowIfNullOrWhiteSpace());
        }

        [TestMethod]
        public void ThrowIfNullOrWhiteSpaceThrowsArgumentExceptionIfInvalidTest()
        {
            string validArg = "pass";
            string[] invalidArgs = new string[] { null, "", " " };

            NUnit.Framework.Assert.DoesNotThrow(() => validArg
                .ThrowIfNullOrWhiteSpace());

            foreach (string invalidArg in invalidArgs)
            {
                NUnit.Framework.Assert.That(() => invalidArg
                    .ThrowIfNullOrWhiteSpace(),
                        Throws.TypeOf<ArgumentException>());
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
        public void ThrowIfLessThanZeroThrowsArgumentExceptionIfValidTest()
        {
            int validArg = 0;

            NUnit.Framework.Assert.DoesNotThrow(() => validArg
                .ThrowIfLessThanZero());
        }

        [TestMethod]
        public void ThrowIfLessThanZeroThrowsArgumentExceptionIfInvalidTest()
        {
            int invalidArg = -1;

            NUnit.Framework.Assert.That(() => invalidArg.ThrowIfLessThanZero(),
                Throws.TypeOf<ArgumentException>());
        }

        [TestMethod]
        public void ThrowIfLessThanZeroReturnsArgumentIfPassTest()
        {
            int validArg = 0;
            int returnedValue = validArg.ThrowIfLessThanZero();

            NUnit.Framework.Assert.AreEqual(validArg, returnedValue);
        }

        [TestMethod]
        public void ThrowIfLessThanOneThrowsArgumentExceptionIfValidTest()
        {
            uint validArg = 1;

            NUnit.Framework.Assert.DoesNotThrow(() => validArg
                .ThrowIfLessThanOne());
        }

        [TestMethod]
        public void ThrowIfLessThanOneThrowsArgumentExceptionIfInvalidTest()
        {
            uint invalidArg = 0;

            NUnit.Framework.Assert.That(() => invalidArg.ThrowIfLessThanOne(),
                Throws.TypeOf<ArgumentException>());
        }

        [TestMethod]
        public void ThrowIfLessThanOneReturnsArgumentIfPassTest()
        {
            uint validArg = 1;
            uint returnedValue = validArg.ThrowIfLessThanOne();

            NUnit.Framework.Assert.AreEqual(validArg, returnedValue);
        }
    }
}