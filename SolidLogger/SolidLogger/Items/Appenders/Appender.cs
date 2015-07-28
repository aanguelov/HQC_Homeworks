namespace SolidLogger.Items.Appenders
{
    using System;

    using SolidLogger.Interfaces;

    public abstract class Appender : IAppender
    {
        protected Appender(ILayout layout)
        {
            this.Layout = layout;
        }

        public ILayout Layout { get; private set; }

        public abstract void Append(string message, ReportLevel level, DateTime date);
    }
}
