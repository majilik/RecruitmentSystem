using RecruitmentSystem.Attributes;
using System.Web.Mvc;

namespace RecruitmentSystem
{
    /// <summary>
    /// Represents the logic for configuring filters for this assembly.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Adds any filters specified in the method body to <paramref name="filters"/>.
        /// This avoids the need to decorate any classes or methods with the filter
        /// since it applies to the entire assembly. It is possible to ignore filters
        /// for specific classes or methods wih another custom attribute.
        /// </summary>
        /// <param name="filters">The global collection of filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleException());
        }
    }
}
