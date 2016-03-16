using System.Web.Mvc;
using System.Web.Routing;

namespace RecruitmentSystem
{
    /// <summary>
    /// Represents the logic for configuring routes for this assembly.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Adds any routes specified in the method body to <paramref name="routes"/>.
        /// </summary>
        /// <param name="routes">The global collection of routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
