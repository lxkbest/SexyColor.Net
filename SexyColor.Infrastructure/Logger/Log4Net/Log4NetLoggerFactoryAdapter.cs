using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace SexyColor.Infrastructure
{
    public class Log4NetLoggerFactoryAdapter : ILoggerFactoryAdapter
    {
        private static bool isConfigLoaded;
        private static ILoggerRepository repository;

        public Log4NetLoggerFactoryAdapter() : this("NETCoreSexyColor", "~/Config/log4net.config")
        {
            
        }

        public Log4NetLoggerFactoryAdapter(string repositoryName) : this(repositoryName, "~/Configs/log4net.config")
        {
            
        }

        public Log4NetLoggerFactoryAdapter(string repositoryName, string configFilename)
        {
            if (!isConfigLoaded)
            {
                if (string.IsNullOrWhiteSpace(repositoryName))
                {
                    repositoryName = "NETCoreSexyColor";
                }
                repository = LogManager.GetRepository(repositoryName);
                if (string.IsNullOrEmpty(configFilename))
                {
                    configFilename = "~/Configs/log4net.config";
                }
                FileInfo configFile = new FileInfo(WebSiteUtility.GetPhysicalFilePath(configFilename));
                if (!configFile.Exists)
                {
                    throw new Exception(string.Format("log4net配置文件 {0} 未找到", configFile.FullName));
                }
                if (repository == null)
                {
                    throw new Exception(string.Format("提供的容器名称不存在，log4net无法获取到容器"));
                }
                XmlConfigurator.ConfigureAndWatch(repository, configFile);
                isConfigLoaded = true;
                
            }
        }

        public ILogger GetLogger(string loggerName)
        {
            return new Log4NetLogger(LogManager.GetLogger(repository.Name, loggerName));
        }
    }
}
