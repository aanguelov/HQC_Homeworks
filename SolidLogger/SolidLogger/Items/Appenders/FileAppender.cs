namespace SolidLogger.Items.Appenders
{
    using System;
    using System.IO;

    using SolidLogger.Interfaces;

    public class FileAppender : Appender
    {
        private readonly StreamWriter writer;

        public FileAppender(ILayout layout, string path)
            : base(layout)
        {
            this.Path = path;
            this.writer = new StreamWriter(this.Path);
        }

        public string Path { get; private set; }

        public override void Append(string message, ReportLevel level, DateTime date)
        {
            var result = this.Layout.FormatLog(message, level, date);

            this.writer.WriteLine(result);
            this.writer.Flush();
        }

        public void Close()
        {
            this.writer.Close();
        }
    }
}
