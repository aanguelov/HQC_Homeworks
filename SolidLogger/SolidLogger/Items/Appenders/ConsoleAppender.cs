namespace SolidLogger.Items.Appenders
{
    using System;

    using SolidLogger.Interfaces;

    public class ConsoleAppender : Appender
    {
        public ConsoleAppender(ILayout layout)
            : base(layout)
        {
        }

        public override void Append(string message, ReportLevel level, DateTime date)
        {
            var result = this.Layout.FormatLog(message, level, date);
            Console.WriteLine(result);
        }
    }
}
