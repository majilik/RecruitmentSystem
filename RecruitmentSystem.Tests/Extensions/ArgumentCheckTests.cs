using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecruitmentSystem.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentSystem.Extensions.Tests
{
    [TestClass()]
    public class ArgumentCheckTests
    {

        [TestMethod()]
        public void ThrowIfNullOrWhiteSpaceThrowsArgumentExceptionTest()
        {
            string validArgs = "logging";
            string[] invalidArgs = new string[] { null, "", " " };

        }

        [TestMethod()]
        public void ThrowIfLessThanZeroThrowsArgumentExceptionTest()
        {

        }

        [TestMethod()]
        public void ThrowIfLessThanOneThrowsArgumentExceptionTest()
        {

        }

        [TestMethod()]
        public void ThrowIfLessThanOneTest1()
        {

        }
    }
}