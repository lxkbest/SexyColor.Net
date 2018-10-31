using System;
using System.Text;
using System.Security.Cryptography;

namespace SexyColor.Infrastructure
{
    public static class EncryptionUtility
    {
        public static string Base64Decode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// base64编码 默认UTF8
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="charset">编码方式</param>
        /// <returns></returns>
        public static string Base64Encode(string str, string charset)
        {
            return Convert.ToBase64String(Encoding.GetEncoding(charset).GetBytes(str));
        }

        /// <summary>
        /// 菜鸟裹裹MD5方式
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string MD5(string str, string charset)
        {
            byte[] bytes = Encoding.GetEncoding(charset).GetBytes(str);
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] somme = md5.ComputeHash(bytes);
            string str2 = "";
            foreach (byte a in somme)
            {
                if (a < 16)
                    str2 += "0" + a.ToString("X");
                else
                    str2 += a.ToString("X");
            }
            return str2.ToLower();
 
        }

        public static string MD5(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            bytes = md5.ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }

        public static string MD5_16(string str)
        {
            return MD5(str).Substring(8, 0x10);
        }

        public static string SymmetricDncrypt(SymmetricEncryptType encryptType, string str, string ivString, string keyString)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            SymmetricEncrypt encrypt = new SymmetricEncrypt(encryptType)
            {
                IVString = ivString,
                KeyString = keyString
            };
            return encrypt.Decrypt(str);
        }

        public static string SymmetricEncrypt(SymmetricEncryptType encryptType, string str, string ivString, string keyString)
        {
            if ((string.IsNullOrEmpty(str) || string.IsNullOrEmpty(ivString)) || string.IsNullOrEmpty(keyString))
            {
                return str;
            }
            SymmetricEncrypt encrypt = new SymmetricEncrypt(encryptType)
            {
                IVString = ivString,
                KeyString = keyString
            };
            return encrypt.Encrypt(str);
        }
    }
}
