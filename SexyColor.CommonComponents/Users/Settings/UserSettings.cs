using System;

namespace SexyColor.CommonComponents
{
    [Serializable]
    public class UserSettings
    {
        private string resetPassword = "888888";
        /// <summary>
        /// 后台重置密码默认使用值
        /// </summary>
        public string ResetPassword { get => resetPassword; set => resetPassword = value; }
    }
}
