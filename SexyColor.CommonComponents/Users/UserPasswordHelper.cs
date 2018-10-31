using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.CommonComponents
{
    public class UserPasswordHelper
    {
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password">用户输入的密码</param>
        /// <param name="dataPassword">数据库密码</param>
        /// <param name="format">加密方式</param>
        /// <returns></returns>
        public static bool CheckPassword(string password, string dataPassword, UserPasswordFormat format)
        {
            string encodedPassword = EncodePassword(password, format);
            if (encodedPassword != null)
                return encodedPassword.Equals(dataPassword, StringComparison.CurrentCultureIgnoreCase);
            else
                return false;
        }
        /// <summary>
        /// 加密方式
        /// </summary>
        /// <param name="password">用户输入的密码</param>
        /// <param name="format">加密方式</param>
        /// <returns></returns>
        public static string EncodePassword(string password, UserPasswordFormat format)
        {
            if (UserPasswordFormat.MD5 == format)
            {
                return EncryptionUtility.MD5(password);
            }
            return string.Empty;
        }
    }
}
