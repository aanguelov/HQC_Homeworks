namespace SolidLogger.Interfaces
{
    using System;

    public interface ILayout
    {
        string FormatLog(string message, ReportLevel level, DateTime date);
    }
}
