using NLog;
using NLog.Config;
using RecruitmentSystem.Logging;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RecruitmentSystem
{
    /// <summary>
    /// Defines the methods, properties and events common to all objects in
    /// this ASP.NET application. A list of the supported event handlers can be
    /// viewed here <see cref="https://msdn.microsoft.com/library/2027ewzw%28v=vs.100%29.aspx"/>.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
            ConfigurationItemFactory.Default.Targets
                .RegisterDefinition("Cassandra", typeof(CassandraTarget));

            LogManager.Configuration.LoggingRules
                .Add(new LoggingRule("*", LogLevel.Trace, _cassandraTarget));

            LogManager.ReconfigExistingLoggers();
        }
    }
}
