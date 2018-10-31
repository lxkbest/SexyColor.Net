using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.CommonComponents
{
    public class CaptchaSettings
    {

        private static volatile CaptchaSettings _instance = null;
        private static readonly object lockObject = new object();

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static CaptchaSettings Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new CaptchaSettings();
                    }
                }
            }
            return _instance;
        }
        private CaptchaSettings() { }

        private string enableCaptcha = "UnEnable";//string.Empty;
        private string captchaCookieName = "VerifySession";
        private bool enableLineNoise = false;
        private int minCharacterCount = 4;
        private int maxCharacterCount = 5;
        private CaptchaCharacterSet characterSet = CaptchaCharacterSet.LowercaseLetters;
        private int captchaPostCount = 10;
        private int captchaLoginCount = 3;

        public bool EnableCaptcha { get { return enableCaptcha.Equals("Enable", StringComparison.CurrentCultureIgnoreCase); } }
        public string CaptchaCookieName { get { return captchaCookieName; } }
        public bool EnableLineNoise { get { return enableLineNoise; } }
        public int MinCharacterCount { get { return minCharacterCount; } }
        public CaptchaCharacterSet CharacterSet { get { return characterSet; } }

        public int CaptchaPostCount { get => captchaPostCount;  }
        public int CaptchaLoginCount { get => captchaLoginCount;  }
        public int MaxCharacterCount { get => maxCharacterCount;  }
    }
}
