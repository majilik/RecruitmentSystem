using NLog;
using System.Web.Mvc;

namespace RecruitmentSystem.Attributes
{
    /// <summary>
    /// Represents the logic used to handle application level errors and provide
    /// users with reasonable responses.
    /// </summary>
    [TraceLogger(AttributeExclude = true)]
    public class HandleException : HandleErrorAttribute
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Invoked when an exception occurs in an application controller.
        /// Exceptions are handled, logged and the user is redirected to
        /// the appropriate error view.
        /// </summary>
        /// <param name="filterContext">The exception context.</param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                _logger.Error(filterContext.Exception);

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