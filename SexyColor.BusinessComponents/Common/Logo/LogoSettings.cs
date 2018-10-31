using SexyColor.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;

namespace SexyColor.BusinessComponents
{
    public class LogoSettings
    {
        private static ConcurrentDictionary<string, LogoSettings> registeredLogoSettings = new ConcurrentDictionary<string, LogoSettings>();

        private string commonTypeKey;
        private ConcurrentDictionary<string, KeyValuePair<Size, ResizeMethod>> imageSizeTypeKey;
        public string CommonTypeKey { get => commonTypeKey; set => commonTypeKey = value; }
        public ConcurrentDictionary<string, KeyValuePair<Size, ResizeMethod>> ImageSizeTypeKey { get => imageSizeTypeKey; set => imageSizeTypeKey = value; }

        private LogoSettings(XElement xElement)
        {
            if (xElement != null)
            {
                XAttribute att = xElement.Attribute("commonTypeKey");
                if (att != null)
                    commonTypeKey = att.Value;
                IEnumerable<XElement> imageSizeTypeElements = xElement.Elements("imageSizeType");
                if (imageSizeTypeElements != null && imageSizeTypeElements.Count() > 0)
                {
                    imageSizeTypeKey = new ConcurrentDictionary<string, KeyValuePair<Size, ResizeMethod>>();
                    foreach (var imageSizeTypeElement in imageSizeTypeElements)
                    {
                        XAttribute keyAttr = imageSizeTypeElement.Attribute("key");
                        if (keyAttr == null)
                            continue;
                        int width = 0;
                        XAttribute widthAttr = imageSizeTypeElement.Attribute("width");
                        if (widthAttr != null)
                            int.TryParse(widthAttr.Value, out width);
                        int height = 0;
                        XAttribute heightAttr = imageSizeTypeElement.Attribute("height");
                        if (heightAttr != null)
                            int.TryParse(heightAttr.Value, out height);
                        ResizeMethod resizeMethod = ResizeMethod.KeepAspectRatio;
                        XAttribute resizeMethodAttr = imageSizeTypeElement.Attribute("resizeMethod");
                        if (resizeMethodAttr != null)
                            Enum.TryParse<ResizeMethod>(resizeMethodAttr.Value, out resizeMethod);
                        imageSizeTypeKey[keyAttr.Value] = new KeyValuePair<Size, ResizeMethod>(new Size(width, height), resizeMethod);
                    }
                }
            }
        }

        /// <summary>
        /// 获取注册的LogoSettings
        /// </summary>
        /// <param name="commonTypeKey"></param>
        /// <returns></returns>
        public static LogoSettings GetRegisteredSettings(string commonTypeKey)
        {
            LogoSettings logoSettings;
            if (registeredLogoSettings.TryGetValue(commonTypeKey, out logoSettings))
                return logoSettings;

            return null;
        }

        /// <summary>
        /// 获取所有注册的LogoSettings
        /// </summary>
        public static IEnumerable<LogoSettings> GetAll()
        {
            return registeredLogoSettings.Values;
        }

        /// <summary>
        /// 注册LogoSettings
        /// </summary>
        /// <param name="xElement"></param>
        public static void RegisterSettings(XElement xElement)
        {
            IEnumerable<LogoSettings> settings = xElement.Elements("add").Select(w => new LogoSettings(w));
            foreach (var setting in settings)
            {
                registeredLogoSettings[setting.CommonTypeKey] = setting;
            }
        }
    }
}
