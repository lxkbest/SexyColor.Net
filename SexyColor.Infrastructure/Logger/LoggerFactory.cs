using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    public static class LoggerFactory
    {
        private static ILoggerFactoryAdapter loggerFactoryAdapter = DIContainer.Resolve<ILoggerFactoryAdapter>();

        public static ILogger GetLogger()
        {
            return GetLogger("SEXYCOLOR");
        }

        public static ILogger GetLogger(string loggerName)
        {
            return loggerFactoryAdapter.GetLogger(loggerName);
        }
    }
}
