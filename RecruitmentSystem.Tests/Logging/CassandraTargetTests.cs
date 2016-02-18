using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RecruitmentSystem.Logging.Tests
{
    [TestClass]
    public class CassandraTargetTests
    {
        [TestMethod]
        public void CassandraTargetThrowsArgumentExceptionIfInvalidTest()
        {
            string validString = "pass";
            string[] invalidStrings = { null, "", " " };
            uint validReplication = 1;
            uint invalidReplication = 0;
            uint validTtl = 0;

            foreach (string invalidString in invalidStrings)
            {
                NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                    new CassandraTarget(new string[] { invalidString },
                                        validString,
                                        validString,
                                        validReplication,
                                        validTtl));
            }

            foreach (string invalidString in invalidStrings)
            {
                NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                    new CassandraTarget(new string[] { validString },
                                        invalidString,
                                        validString,
                                        validReplication,
                                        validTtl));
            }

            foreach (string invalidString in invalidStrings)
            {
                NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                    new CassandraTarget(new string[] { validString },
                                        validString,
                                        invalidString,
                                        validReplication,
                                        validTtl));
            }

            NUnit.Framework.Assert.Throws<ArgumentException>(() =>
                new CassandraTarget(new string[] { validString },
                                    validString,
                                    validString,
                                    invalidReplication,
                                    validTtl));
        }

        [TestMethod]
        public void RequiredParameterNodesThrowsArgumentExceptionIfInvalidTest()
        {
        }

        [TestMethod]
        public void DisposeTest()
        {

        }
    }
}