namespace SolidLogger.Interfaces
{
    using System;
    using System.Dynamic;

    public interface IAppender
    {
        ILayout Layout { get; }

        void Append(string message, ReportLevel level, DateTime date);
    }
}
