using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.CommonComponents
{
    public static class DateTimeExtension
    {

        /// <summary>
        /// 转社交时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDisplayString(this DateTime? value)
        {
            if (value.HasValue) return
                    value.Value.ToDisplayString();
            else return string.Empty;
        }
        /// <summary>
        /// 转社交时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDisplayString(this DateTime value)
        {
            DateTime now = DateTime.Now;
            if (now < value)
                return value.ToString("yyyy.MM.dd HH:mm:ss");
            TimeSpan dep = now - value;
            if (dep.TotalMinutes < 10)
                return "刚刚";
            else if (dep.TotalMinutes >= 10 && dep.TotalMinutes < 60)
                return (int)dep.TotalMinutes + " 分钟前";
            else if (dep.TotalHours < 24)
                return (int)dep.TotalHours + " 小时前";
            else if (dep.TotalDays < 5)
                return (int)dep.TotalDays + " 天前";
            else
                return value.ToString("yyyy.MM.dd HH:mm:ss");
        }

        public static string ToDateString(this DateTime dateTime)
        {
            return ToDateString(dateTime, false);
        }

        public static string ToDateString(this DateTime dateTime, bool displayTime)
        {
            if (dateTime == DateTime.MinValue)
                return "-";
            DateTime userDate = ConvertToUserDate(dateTime);
            string dateFormat = "yyyy-MM-dd";
            if (displayTime)
                return userDate.ToString(dateFormat + " HH:mm");
            else
                return userDate.ToString(dateFormat);
        }

        public static DateTime ConvertToUserDate(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Local)
                return dateTime;
            else
                return dateTime.AddHours(8);
        }
    }
}
