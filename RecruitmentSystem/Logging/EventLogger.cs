using NLog;
using RecruitmentSystem.Attributes;
using System.Threading.Tasks;

namespace RecruitmentSystem.Logging
{
    [TraceLogger(AttributeExclude = true)]
    public class EventLogger
    {
        private Logger _logger;

        public EventLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public async Task AsyncLog(LogLevel logLevel, string eventID, string message)
        {
            await Task.Run(() => Log(logLevel, eventID, message));
        }

        public void Log(LogLevel logLevel, string eventID, string message)
        {
            LogEventInfo logEvent = new LogEventInfo(logLevel, _logger.Name, message);
            logEvent.Properties["EventID"] = eventID;
            _logger.Log(typeof(EventLogger), logEvent);
        }
    }
}