using Cassandra;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using RecruitmentSystem.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentSystem.Logging
{
    /// <summary>
    /// Represents a custom extension of the NLog framework. When NLog is
    /// configured to use this target it will intercept any logs and handle the
    /// process of writing thme to a backing Cassandra node. There are two ways
    /// to configure NLog, see
    /// <see cref="https://github.com/NLog/NLog/wiki/Extending-NLog"/> for more
    /// information.
    /// </summary>
    [Target("Cassandra")]
    public sealed class CassandraTarget : TargetWithLayout, IDisposable
    {
        private string[] _nodes;
        private string _keySpace;
        private string _columnFamily;
        private uint _replication;
        private uint _ttl;

        private readonly Lazy<Cluster> _cluster;
        private readonly Lazy<ISession> _session;
        private readonly Lazy<PreparedStatement> _logStatement;

        private bool _initialized = false;

        /// <summary>
        /// A comma separated list of connection points. Not optional.
        /// </summary>
        [RequiredParameter]
        public string Node
        {
            get { return string.Join(",", _nodes); }
            set
            {   _nodes = value.Split(',')
                    .Select(s => s.ThrowIfNullOrWhiteSpace()).ToArray();
            }
        }

        /// <summary>
        /// The name of a Cassandra key space. Not optional.
        /// </summary>
        [RequiredParameter]
        public string KeySpace
        {
            get { return _keySpace; }
            set { _keySpace = value.ThrowIfNullOrWhiteSpace().Trim(); }
        }

        /// <summary>
        /// The name of a Cassandra column family. Not optional.
        /// </summary>
        [RequiredParameter]
        public string ColumnFamily
        {
            get { return _columnFamily; }
            set { _columnFamily = value.ThrowIfNullOrWhiteSpace().Trim(); }
        }

        /// <summary>
        /// Specifies how many copies of each record should be stored. Each
        /// record is stored on a separate node. Not optional.
        /// </summary>
        [RequiredParameter]
        public uint Replication
        {
            get { return _replication; }
            set { _replication = value.ThrowIfLessThanOne(); }
        }

        /// <summary>
        /// Represents the time in seconds that a log entry should be
        /// persisted. A value of 0 will store the record indefinitely.
        /// Not optional.
        /// </summary>
        [RequiredParameter]
        public uint Ttl
        {
            get { return _ttl; }
            set { _ttl = value; }
        }

        /// <summary>
        /// Constructs an NLog Target, see
        /// <see cref="https://github.com/NLog/NLog/wiki/Targets"/>. A
        /// connection will be established to a Cassandra cluster specified by
        /// <paramref name="nodes"/>. A session will be constructed to hold
        /// the  connections to the cluster nodes.
        /// </summary>
        /// <param name="nodes">A list of connection points.</param>
        /// <param name="keySpace">The Cassandra key space name.</param>
        /// <param name="columnFamily">The Cassandra column family name.
        /// </param>
        /// <param name="replication">The number of nodes to replicate data on.
        /// </param>
        /// <param name="ttl">The time in seconds that a log entry should be
        /// persisted. A value of 0 stores it indefinitely.</param>
        /// <exception cref="NoHostAvailableException"></exception>
        public CassandraTarget(string[] nodes, string keySpace,
            string columnFamily, uint replication, uint ttl)
        {
            _nodes = Array.FindAll(nodes, s => !string.IsNullOrWhiteSpace(s));
            _keySpace = keySpace.ThrowIfNullOrWhiteSpace().Trim();
            _columnFamily = columnFamily.ThrowIfNullOrWhiteSpace().Trim();
            _replication = replication.ThrowIfLessThanOne();
            _ttl = ttl;

            _cluster = new Lazy<Cluster>(
                () => Cluster.Builder().WithDefaultKeyspace(KeySpace)
                .AddContactPoints(_nodes).Build());

            Dictionary<string, string> clusterDef =
                new Dictionary<string, string>()
                {
                    { "class", "SimpleStrategy" },
                    { "replication_factor", Replication.ToString() }
                };

            _session = new Lazy<ISession>(
                () => _cluster.Value
                .ConnectAndCreateDefaultKeyspaceIfNotExists(
                    new Dictionary<string, string>(clusterDef)));

            _logStatement = new Lazy<PreparedStatement>(() => _session.Value
                .Prepare(CassandraQueries
                    .Insert(_keySpace, _columnFamily, _ttl)));
        }

        public void Connect() { }

        protected override void Write(LogEventInfo logEvent)
        {
            if (!_initialized) Init();

            _session.Value.Execute(_logStatement.Value.Bind(
                new { logger = logEvent.LoggerName,
                      id = TimeUuid.NewId(logEvent.TimeStamp),
                      sequenceid = logEvent.SequenceID,
                      timestamp = logEvent.TimeStamp,
                      level = logEvent.Level.ToString(),
                      message = Layout.Render(logEvent),
                      stacktrace = logEvent.Exception == null ?
                        "" : logEvent.Exception.StackTrace
                    }
            ));
        }

        private void Init()
        {
            IStatement createColumnFamilyStatement = (new SimpleStatement(
                CassandraQueries.CreateTable(KeySpace, ColumnFamily)))
                .SetConsistencyLevel(new ConsistencyLevel?
                    (ConsistencyLevel.All));

            _session.Value.Execute(createColumnFamilyStatement);
            _cluster.Value.RefreshSchema();

            _initialized = true;
        }

        #region IDisposable Support
        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_cluster != null) _cluster.Value.Shutdown();
                    if (_session != null) _session.Value.Dispose();
                }

                _disposed = true;
            }
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}