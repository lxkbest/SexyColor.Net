﻿namespace SexyColor.CommonComponents
{
    public static class IntExtension
    {
        /// <summary>
        /// 转文件大小
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDisplayFileSize(this int value)
        {
            if (value < 1000)
                return string.Format("{0} Byte", value);
            else if (value >= 1000 && value < 1000000)
                return string.Format("{0:F2} Kb", ((double)value) / 1024);
            else if (value >= 1000 && value < 1000000000)
                return string.Format("{0:F2} M", ((double)value) / 1048576);
            else
                return string.Format("{0:F2} G", ((double)value) / 1073741824);
        }
    }
}
