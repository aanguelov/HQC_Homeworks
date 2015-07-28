namespace SolidLogger.Interfaces
{
    using System.Collections.Generic;

    public interface ILogger
    {
        ICollection<IAppender> Appenders { get; }

        void Error(string message);

        void Info(string message);

        void Warn(string message);

        void Critical(string message);

        void Fatal(string message);
    }
}
