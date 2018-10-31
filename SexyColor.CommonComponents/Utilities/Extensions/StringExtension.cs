using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    public static class StringExtension
    {

        private static readonly Regex emailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex webUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex stripHTMLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        /// <summary>
        /// 转Byte类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByte(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// 编码Html
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string value)
        {
            return WebUtility.HtmlEncode(value);
        }

        /// <summary>
        /// 解码Html
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string value)
        {
            return WebUtility.HtmlDecode(value);
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncode(this string value)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = Encoding.UTF8.GetBytes(value);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString());
        }

        /// <summary>
        /// 转Unicode编码字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUnicode(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append("\\u" + ((int)value[i]).ToString("x"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 便捷Format
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(instance, args);
        }

        /// <summary>
        /// 转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string instance, T defaultValue) where T : struct, IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrWhiteSpace(instance) && !Enum.TryParse(instance.Trim(), true, out convertedValue))
            {
                convertedValue = defaultValue;
            }

            return convertedValue;
        }
        
        /// <summary>
        /// 转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int instance, T defaultValue) where T : struct, IComparable, IFormattable
        {
            T convertedValue;

            if (!Enum.TryParse(instance.ToString(), true, out convertedValue))
            {
                convertedValue = defaultValue;
            }

            return convertedValue;
        }

        /// <summary>
        /// 判断是否Email
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsEmail(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance) && emailExpression.IsMatch(instance);
        }

        /// <summary>
        /// 判断是否Url链接
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsWebUrl(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance) && webUrlExpression.IsMatch(instance);
        }

        /// <summary>
        /// 转Bool
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool AsBool(this string instance)
        {
            bool result = false;
            bool.TryParse(instance, out result);
            return result;
        }

        /// <summary>
        /// 转DateTime
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string instance)
        {
            DateTime result = DateTime.MinValue;
            DateTime.TryParse(instance, out result);
            return result;
        }

        /// <summary>
        /// 转Decimal
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Decimal AsDecimal(this string instance)
        {
            var result = (decimal)0.0;
            Decimal.TryParse(instance, out result);
            return result;
        }

        /// <summary>
        /// 转Int
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static int AsInt(this string instance)
        {
            var result = (int)0;
            int.TryParse(instance, out result);
            return result;
        }

        /// <summary>
        /// 转Long
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static long AsLong(this string instance)
        {
            var result = (long)0;
            long.TryParse(instance, out result);
            return result;
        }

        /// <summary>
        /// 判断是否是Int
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsInt(this string instance)
        {
            int result;
            return int.TryParse(instance, out result);
        }

        /// <summary>
        /// 判断是否是DateTime
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string instance)
        {
            DateTime result;
            return DateTime.TryParse(instance, out result);
        }

        /// <summary>
        /// 判断是否是Float
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsFloat(this string instance)
        {
            float result;
            return float.TryParse(instance, out result);
        }

        /// <summary>
        /// 判断是否是Null或是空字符，还是空白字符
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string instance)
        {
            return string.IsNullOrWhiteSpace(instance);
        }

        /// <summary>
        /// 判断是否是Null并且是空字符，而且是空白字符
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsNotNullAndWhiteSpace(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance);
        }

        /// <summary>
        /// 判断是否是Null或是空字符
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string theString)
        {
            return string.IsNullOrEmpty(theString);
        }

        /// <summary>
        /// 字符太长使用...代替
        /// </summary>
        /// <param name="rawString">待截字的字符串</param>
        /// <param name="charLimit">截字的长度，按双字节计数</param>
        /// <returns></returns>
        public static string AsTrim(this string rawString, int charLimit)
        {
            return AsTrim(rawString, charLimit, "...");
        }

        /// <summary>
        /// 字符太长使用其他字符代替
        /// </summary>
        /// <param name="rawString">待截字的字符串</param>
        /// <param name="charLimit">截字的长度，按双字节计数</param>
        /// <param name="appendString">截去字的部分用替代字符串</param>
        /// <returns></returns>
        public static string AsTrim(this string rawString, int charLimit, string appendString)
        {
            if (string.IsNullOrEmpty(rawString) || (rawString.Length <= charLimit))
            {
                return rawString;
            }
            if (Encoding.UTF8.GetBytes(rawString).Length <= (charLimit * 2))
            {
                return rawString;
            }
            charLimit = (charLimit * 2) - Encoding.UTF8.GetBytes(appendString).Length;
            StringBuilder builder = new StringBuilder();
            int num2 = 0;
            for (int i = 0; i < rawString.Length; i++)
            {
                char ch = rawString[i];
                builder.Append(ch);
                num2 += (ch > '\x0080') ? 2 : 1;
                if (num2 >= charLimit)
                {
                    break;
                }
            }
            return builder.Append(appendString).ToString();
        }
    }
}
