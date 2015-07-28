namespace SolidLogger.Items.Layouts
{
    using System;
    using System.Text;

    using SolidLogger.Interfaces;

    public class XmlLayout : ILayout
    {
        public string FormatLog(string message, ReportLevel level, DateTime date)
        {
            var result = new StringBuilder();
            result.AppendLine("<log>")
                .AppendLine(string.Format("   <date>{0}</date>", date))
                .AppendLine(string.Format("   <level>{0}</level>", level))
                .AppendLine(string.Format("   <message>{0}</message>", message))
                .AppendLine("</log>");

            return result.ToString();
        }
    }
}
