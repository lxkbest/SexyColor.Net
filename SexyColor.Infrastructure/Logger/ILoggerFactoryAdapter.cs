using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    public interface ILoggerFactoryAdapter
    {
        ILogger GetLogger(string loggerName);
    }
}
