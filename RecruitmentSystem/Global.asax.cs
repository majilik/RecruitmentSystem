using Cassandra;
using NLog;
using NLog.Config;
using RecruitmentSystem.Logging;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RecruitmentSystem
{
    /// <summary>
    /// Represents the starting point where application setup oocurs.
    /// Defines the methods, properties and events common to all objects in
    /// this ASP.NET application. A list of the supported event handlers can be
    /// viewed here <see cref="https://msdn.microsoft.com/library/2027ewzw%28v=vs.100%29.aspx"/>.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private CassandraTarget _cassandraTarget =
            new CassandraTarget(new string[] { "localhost" }, "logging", "log_entries", 1, null);

        protected void Application_Start()
        {
            //CassandraLoggerSetup();

            logger.Debug("Starting application");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Dispose()
        {
            _cassandraTarget.Dispose();
        }

        private void CassandraLoggerSetup()
        {
            try {
                ConfigurationItemFactory.Default.Targets
                    .RegisterDefinition("Cassandra", typeof(CassandraTarget));

                LogManager.Configuration.LoggingRules
                    .Add(new LoggingRule("*", LogLevel.Trace, _cassandraTarget));

                LogManager.ReconfigExistingLoggers();
            } catch (NotSupportedException ex)
            {
                logger.Error(ex, "No Cassandra nodes available, proceeding with local file backup.");
            }
        }
    }
}
