using NLog;
using RecruitmentSystem.Attributes;
using System.Threading.Tasks;

namespace RecruitmentSystem.Logging
{
    [TraceLogger(AttributeExclude = true)]
    public class EventLogger
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static async Task AsyncLog(string loggerName, LogLevel logLevel, string eventID, string message)
        {
            await Task.Run(() => Log(loggerName, logLevel, eventID, message));
        }

        public static void Log(string loggerName, LogLevel logLevel, string eventID, string message)
        {
            LogEventInfo logEvent = new LogEventInfo(logLevel, _logger.Name, message);
            logEvent.Properties["EventID"] = eventID;
            logEvent.Properties["LoggerName"] = loggerName;
            _logger.Log(typeof(EventLogger), logEvent);
        }
    }
}