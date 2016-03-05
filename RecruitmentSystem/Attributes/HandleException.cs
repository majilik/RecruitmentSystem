using NLog;
using System.Web.Mvc;

namespace RecruitmentSystem.Attributes
{
    public class HandleException : HandleErrorAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                logger.Error(filterContext.Exception);

                filterContext.ExceptionHandled = true;
                var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)
                };
            }
        }
    }
}