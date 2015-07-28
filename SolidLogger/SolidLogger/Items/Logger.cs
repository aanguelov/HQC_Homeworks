namespace SolidLogger.Items
{
    using System;
    using System.Collections.Generic;

    using SolidLogger.Interfaces;

    public class Logger : ILogger
    {
        public Logger(IAppender[] appenders)
        {
            this.Appenders = new List<IAppender>(appenders);
        }

        public ICollection<IAppender> Appenders { get; private set; }

        public void Error(string message)
        {
            this.LogMessage(ReportLevel.Error, message);
        }

        public void Info(string message)
        {
            this.LogMessage(ReportLevel.Info, message);
        }

        public void Warn(string message)
        {
            this.LogMessage(ReportLevel.Warn, message);
        }

        public void Critical(string message)
        {
            this.LogMessage(ReportLevel.Critical, message);
        }

        public void Fatal(string message)
        {
            this.LogMessage(ReportLevel.Fatal, message);
        }

        private void LogMessage(ReportLevel level, string message)
        {
            foreach (var appender in this.Appenders)
            {
                appender.Append(message, level, DateTime.Now);
            }
        }
    }
}
