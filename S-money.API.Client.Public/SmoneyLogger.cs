namespace Smoney.API.Client
{
    public static class SmoneyLogger
    {
        private static ILogAdapter logger;

        public static ILogAdapter Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new ConsoleLogger();
                }
                return logger;
            }
            set { logger = value; }
        }
    }
}