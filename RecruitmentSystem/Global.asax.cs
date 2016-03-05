using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            logger.Info("Starting application");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Dispose()
        {
            logger.Info("Stopping application");
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception == null)
            {
                exception = new HttpException(500, "Internal Server Error", exception);
            }

            if (exception is HttpException)
            {
                HttpException httpException = exception as HttpException;
                Server.ClearError();
                Response.Clear();

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        Response.Redirect("~/Error/PageNotFound");
                        break;
                    case 500:
                        Response.Redirect("~/Error/InternalServerError");
                        break;
                }
            }
        }
    }
}
