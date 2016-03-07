using NLog;
using System.Web.Mvc;

namespace RecruitmentSystem.Attributes
{
    public class LogException : HandleErrorAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                logger.Error(filterContext.Exception);
            }

        }
    }
}