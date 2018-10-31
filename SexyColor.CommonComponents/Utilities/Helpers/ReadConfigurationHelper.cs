using Microsoft.Extensions.Configuration;
using System;

namespace SexyColor.CommonComponents
{
    public  class ReadConfigurationHelper
    {
        private static volatile ReadConfigurationHelper _instance = null;
        private static readonly object lockObject = new object();

        public static IConfigurationRoot Configuration { get; set; }


        public static ReadConfigurationHelper Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new ReadConfigurationHelper();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 获取原始Url
        /// </summary>
        /// <returns></returns>
        public string GetOriginalUrl(string url)
        {
            var urlSuffix = string.Empty;
            if (Configuration != null)
            {
                var setting = Configuration["UseSuffix"];
                if (setting.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    urlSuffix = ".aspx";
                }
            }
            return url + urlSuffix;
        }
    }
}
