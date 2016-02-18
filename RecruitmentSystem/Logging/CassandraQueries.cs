using RecruitmentSystem.Extensions;

namespace RecruitmentSystem.Logging
{
    /// <summary>
    /// Encapsulates the query logic used to store application logs in an
    /// Apache Cassandra <see cref="http://cassandra.apache.org/"/> database.
    /// </summary>
    public class CassandraQueries
    {
        /// <summary>
        /// Returns a CQL (Cassandra Query Language) formatted string that can
        /// be used to query a Cassandra database. When executed, the queried
        /// node will create a column family (a container for a collection of
        /// rows, a table) named <paramref name="columnFamily"/>. The container
        /// for this column family a key space (a namespace that defines data
        /// replication on nodes, similar to a schema in relational databases)
        /// named <paramref name="keySpace"/>. The column family created
        /// represents the data created as a result of an application log.
        /// </summary>
        /// <param name="keySpace">The referenced key space.</param>
        /// <param name="columnFamily">The column family to create if it
        /// doesn't already exist.</param>
        /// <returns>A CQL formatted string that can be executed in a
        /// statement.</returns>
        public static string CreateTable(string keySpace, string columnFamily)
        {
            return string.Format(@"CREATE TABLE IF NOT EXISTS ""{0}"".""{1}""
                                (logger text,
                                id timeuuid,
                                sequenceid int,
                                timestamp timestamp,
                                level text,
                                message text,
                                stacktrace text,
                                PRIMARY KEY(logger, id))
                                WITH CLUSTERING ORDER BY(id ASC);",
                                keySpace.ThrowIfNullOrWhiteSpace(),
                                columnFamily.ThrowIfNullOrWhiteSpace());
        }

        /// <summary>
        /// Returns a CQL (Cassandra Query Language) formatted string that can
        /// be used to query a Cassandra database. When executed, the queried
        /// node will create a row contained in the given column family and key
        /// space. The row created represents the data created as a result of
        /// an application log. It is possible to specify the Time To Live for
        /// the new row. This will result in the node deleting the record after
        /// <paramref name="ttl"/> seconds, or store it indefinitely if the
        /// value is 0. Note that this requires 8 extra bytes of storage per
        /// row.
        /// </summary>
        /// <param name="keySpace">The referenced key space.</param>
        /// <param name="columnFamily">The referenced column family.</param>
        /// <param name="ttl">The time in seconds that the log should be
        /// stored. Will be stored indefinitely if the values is 0.</param>
        /// <returns>A CQL formatted string that can be executed in a
        /// statement.</returns>
        public static string Insert(string keySpace, string columnFamily, uint ttl)
        {
            return string.Format(@"INSERT INTO ""{0}"".""{1}""
                                (logger, id, sequenceid, timestamp, level,
                                    message, stacktrace)
                                VALUES (?, ?, ?, ?, ?, ?, ?) USING TTL {2};",
                                keySpace.ThrowIfNullOrWhiteSpace(),
                                columnFamily.ThrowIfNullOrWhiteSpace(), ttl);
        }
    }
}