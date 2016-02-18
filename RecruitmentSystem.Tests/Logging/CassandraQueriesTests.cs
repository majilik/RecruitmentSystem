using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;

namespace RecruitmentSystem.Logging.Tests
{
    [TestClass]
    public class CassandraQueriesTests
    {
        private string _validKeySpace;
        private string[] _invalidKeySpaces;

        private string _validColumnFamily;
        private string[] _invalidColumnFamilies;

        [TestInitialize]
        public void TestInitialize()
        {
            _validKeySpace = "logging";
            _invalidKeySpaces = new string[] { null, "", " " };

            _validColumnFamily = "log_entires";
            _invalidColumnFamilies = new string[] { null, "", " " };
        }

        [TestCleanup]
        public void TestCleanp()
        {
            _validKeySpace = null;
            _invalidKeySpaces = null;

            _validColumnFamily = null;
            _invalidColumnFamilies = null;
        }

        [TestMethod]
        public void CreateTableThrowsArgumentExceptionTest()
        {
            foreach (string invalidKeySpace in _invalidKeySpaces)
            {
                NUnit.Framework.Assert.That(() => CassandraQueries
                    .CreateTable(invalidKeySpace, _validColumnFamily),
                        Throws.TypeOf<ArgumentException>());
            }

            foreach (string invalidColumnFamily in _invalidColumnFamilies)
            {
                NUnit.Framework.Assert.That(() => CassandraQueries
                    .CreateTable(_validKeySpace, invalidColumnFamily),
                        Throws.TypeOf<ArgumentException>());
            }
        }

        [TestMethod]
        public void CreateTableReturnsProperlyFormattedQueryTest()
        {
            string properQuery =
                string.Format(@"CREATE TABLE IF NOT EXISTS ""{0}"".""{1}""
                                (logger text,
                                id timeuuid,
                                sequenceid int,
                                timestamp timestamp,
                                level text,
                                message text,
                                stacktrace text,
                                PRIMARY KEY(logger, id))
                                WITH CLUSTERING ORDER BY(id ASC);",
                                _validKeySpace,
                                _validColumnFamily);

            string returnedQuery = CassandraQueries.
                CreateTable(_validKeySpace, _validColumnFamily);

            NUnit.Framework.Assert.AreEqual(properQuery, returnedQuery);
        }

        [TestMethod]
        public void InsertThrowsArgumentExceptionTest()
        {
            uint validTtl = 0;

            foreach (string invalidKeySpace in _invalidKeySpaces)
            {
                NUnit.Framework.Assert.That(() => CassandraQueries
                    .Insert(invalidKeySpace, _validColumnFamily, validTtl),
                        Throws.TypeOf<ArgumentException>());
            }

            foreach (string invalidColumnFamily in _invalidColumnFamilies)
            {
                NUnit.Framework.Assert.That(() => CassandraQueries
                    .Insert(_validKeySpace, invalidColumnFamily, validTtl),
                        Throws.TypeOf<ArgumentException>());
            }
        }

        [TestMethod]
        public void InsertReturnsProperlyFormattedQueryTest()
        {
            uint validTtl = 0;

            string properQuery =
                string.Format(@"INSERT INTO ""{0}"".""{1}""
                                (logger, id, sequenceid, timestamp, level,
                                    message, stacktrace)
                                VALUES (?, ?, ?, ?, ?, ?, ?) USING TTL {2};",
                                _validKeySpace, _validColumnFamily, validTtl);

            string returnedQuery = CassandraQueries.
                Insert(_validKeySpace, _validColumnFamily, validTtl);

            NUnit.Framework.Assert.AreEqual(properQuery, returnedQuery);
        }
    }
}