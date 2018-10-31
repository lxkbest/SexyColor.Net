using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SexyColor.Infrastructure
{
    public class WebSiteUtility
    {
        private static string directorySeparatorChar = Path.DirectorySeparatorChar.ToString();

        private static IHostingEnvironment host = GlobalHostingEnvironment.HostingEnvironment;

        /// <summary>
        /// ~/images 转换物理路径 C:\\images
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            var rootPath = host.ContentRootPath;
            return IsAbsolute(path) ? path : Path.Combine(rootPath, path.TrimStart('~', '/').Replace("/", directorySeparatorChar));
        }

        /// <summary>
        /// 把~/images 转 /images
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapUrl(string path)
        {
            return path.TrimStart('~');
        }

        public static string MapPhysicsToUrl(string path)
        {
            var hostPath = path;
            return hostPath;
        }

        /// <summary>
        /// 判断是否是绝对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }

        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetPhysicalFilePath(string filePath)
        {
            if ((filePath.IndexOf(@":\") != -1) || (filePath.IndexOf(@"\\") != -1))
            {
                return filePath;
            }
            return MapPath(filePath);
        }

        /// <summary>
        /// 将URL转换为在请求客户端可用的 URL绝对路径
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public static string ResolveUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                return relativeUrl;
            }
            if (!relativeUrl.StartsWith("~/"))
            {
                return relativeUrl;
            }
            string[] strArray = relativeUrl.Split(new char[] { '?' });
            string str = MapUrl(strArray[0]);
            if (strArray.Length > 1)
            {
                str = str + "?" + strArray[1];
            }
            return str;
        }
    }
}
