using RecruitmentSystem.Attributes;
using System.Web.Mvc;

namespace RecruitmentSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleException());
        }
    }
}
