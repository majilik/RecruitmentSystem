using NLog;
using System.Web.Mvc;

namespace RecruitmentSystem.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class LogException : HandleErrorAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                logger.Error(filterContext.Exception);
            }
        }
    }
}