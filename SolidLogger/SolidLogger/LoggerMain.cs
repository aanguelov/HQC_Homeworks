namespace SolidLogger
{
    using SolidLogger.Interfaces;
    using SolidLogger.Items;
    using SolidLogger.Items.Appenders;
    using SolidLogger.Items.Layouts;

    public class LoggerMain
    {
        public static void Main()
        {
            ILayout simpleLayout = new SimpleLayout();
            IAppender consoleAppender = new ConsoleAppender(simpleLayout);
            ILogger logger = new Logger(new [] { consoleAppender });

            logger.Error("Error parsing JSON.");
            logger.Info(string.Format("User {0} successfully registered.", "Pesho"));
        }
    }
}
