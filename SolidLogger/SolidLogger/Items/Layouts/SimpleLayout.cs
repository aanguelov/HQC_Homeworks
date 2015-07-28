namespace SolidLogger.Items.Layouts
{
    using System;

    using SolidLogger.Interfaces;

    public class SimpleLayout : ILayout
    {
        public string FormatLog(string message, ReportLevel level, DateTime date)
        {
            string formatted = string.Format("{0} - {1} - {2}", date, level, message);

            return formatted;
        }
    }
}
