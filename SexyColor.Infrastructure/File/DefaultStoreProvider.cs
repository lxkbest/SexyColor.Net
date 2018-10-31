using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SexyColor.Infrastructure
{
    public class DefaultStoreProvider : IStoreProvider
    {
        private string directlyRootUrl;
        private string storeRootPath;
        private static readonly Regex ValidFileNamePattern;
        private static readonly Regex ValidPathPattern;

        public string StoreRootPath { get => storeRootPath; set => storeRootPath = value; }
        public string DirectlyRootUrl { get => directlyRootUrl; set => directlyRootUrl = value; }

        static DefaultStoreProvider()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("^[^");
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                builder.Append(Regex.Escape(new string(ch, 1)));
            }
            builder.Append("]{1,255}$");
            ValidFileNamePattern = new Regex(builder.ToString(), RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            builder = new StringBuilder();
            builder.Append("^[^");
            foreach (char ch2 in Path.GetInvalidPathChars())
            {
                builder.Append(Regex.Escape(new string(ch2, 1)));
            }
            ValidPathPattern = new Regex(builder.ToString() + "]{0,769}$", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public DefaultStoreProvider(string storeRootPath) : this(storeRootPath, WebSiteUtility.ResolveUrl(storeRootPath))
        {
        }

        public DefaultStoreProvider(string storeRootPath, string directlyRootUrl)
        {
            this.storeRootPath = WebSiteUtility.GetPhysicalFilePath(storeRootPath);
            if (!string.IsNullOrEmpty(this.StoreRootPath))
            {
                this.storeRootPath = this.StoreRootPath.TrimEnd(new char[] { '/', '\\' });
            }
            this.directlyRootUrl = directlyRootUrl;
            if (!string.IsNullOrEmpty(this.directlyRootUrl))
            {
                this.directlyRootUrl = WebSiteUtility.ResolveUrl(this.directlyRootUrl.TrimEnd(new char[] { '/', '\\' }));
            }
        }

        public IStoreFile AddOrUpdateFile(string relativePath, string fileName, Stream contentStream)
        {
            if ((contentStream == null) || !contentStream.CanRead)
            {
                return null;
            }
            if (!IsValidPathAndFileName(relativePath, fileName))
            {
                throw new InvalidOperationException("The provided path and/or file name is invalid.");
            }
            string fullLocalPath = this.GetFullLocalPath(relativePath, fileName);
            EnsurePathExists(fullLocalPath, true);
            contentStream.Position = 0L;
            using (FileStream stream = File.OpenWrite(fullLocalPath))
            {
                int num;
                byte[] buffer = new byte[(contentStream.Length > 0x10000L) ? ((int)0x10000L) : ((int)contentStream.Length)];
                while ((num = contentStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, num);
                }
                stream.Flush();
            }
            return new DefaultStoreFile(relativePath, new FileInfo(fullLocalPath));
        }

        public void DeleteFolder(string relativePath)
        {
            if (IsValidPath(relativePath))
            {
                string fullLocalPath = this.GetFullLocalPath(relativePath, string.Empty);
                if (Directory.Exists(fullLocalPath))
                {
                    Directory.Delete(fullLocalPath, true);
                }
            }
        }

        public string GetDirectlyUrl(string relativeFileName)
        {
            return this.GetDirectlyUrl(relativeFileName, DateTime.MinValue);
        }

        public string GetDirectlyUrl(string relativeFileName, DateTime lastModified)
        {
            if (string.IsNullOrEmpty(this.DirectlyRootUrl))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder(this.DirectlyRootUrl);
            relativeFileName = relativeFileName.Replace('\\', '/');
            if (!relativeFileName.StartsWith("/"))
            {
                builder.Append("/");
            }
            builder.Append(relativeFileName);
            if (lastModified > DateTime.MinValue)
            {
                builder.Append("?lm=");
                builder.Append(lastModified.Ticks);
            }
            return builder.ToString();
        }

        public string GetDirectlyUrl(string relativePath, string fileName)
        {
            return this.GetDirectlyUrl(relativePath, fileName, DateTime.MinValue);
        }

        public string GetDirectlyUrl(string relativePath, string fileName, DateTime lastModified)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return this.GetDirectlyUrl(fileName, lastModified);
            }
            if (!relativePath.EndsWith(@"\") && !relativePath.EndsWith("/"))
            {
                return this.GetDirectlyUrl(relativePath + "/" + fileName, lastModified);
            }
            return this.GetDirectlyUrl(relativePath + fileName, lastModified);
        }

        private static void EnsurePathExists(string fullLocalPath, bool pathIncludesFilename)
        {
            string path = pathIncludesFilename ? fullLocalPath.Substring(0, fullLocalPath.LastIndexOf(Path.DirectorySeparatorChar)) : fullLocalPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string GetSizeImageName(string filename, Size size, ResizeMethod resizeMethod)
        {
            return string.Format("{0}-{1}-{2}x{3}{4}", new object[] { filename, (resizeMethod != ResizeMethod.KeepAspectRatio) ? resizeMethod.ToString() : string.Empty, size.Width, size.Height, Path.GetExtension(filename) });
        }

        private static bool IsValidPath(string path)
        {
            return ValidPathPattern.IsMatch(path);
        }

        private static bool IsValidFileName(string fileName)
        {
            if (ValidFileNamePattern != null)
            {
                return ValidFileNamePattern.IsMatch(fileName);
            }
            return true;
        }

        /// <summary>
        /// 连接目录
        /// </summary>
        /// <param name="directoryParts"></param>
        /// <returns></returns>
        public string JoinDirectory(params string[] directoryParts)
        {
            return string.Join(new string(Path.DirectorySeparatorChar, 1), directoryParts);
        }

        private static bool IsValidPathAndFileName(string path, string fileName)
        {
            return ((IsValidPath(path) && IsValidFileName(fileName)) && (Encoding.UTF8.GetBytes(path + "." + fileName).Length <= 0x400));
        }

        public string GetFullLocalPath(string relativeFileName)
        {
            string storeRootPath = this.StoreRootPath;
            if (storeRootPath.EndsWith(":"))
            {
                storeRootPath = storeRootPath + @"\";
            }
            if (!string.IsNullOrEmpty(relativeFileName))
            {
                storeRootPath = Path.Combine(storeRootPath, relativeFileName);
            }
            return storeRootPath;
        }

        public string GetFullLocalPath(string relativePath, string fileName)
        {
            string storeRootPath = this.StoreRootPath;
            if (storeRootPath.EndsWith(":"))
            {
                storeRootPath = storeRootPath + @"\";
            }
            if (!string.IsNullOrEmpty(relativePath))
            {
                relativePath = relativePath.TrimStart(new char[] { Path.DirectorySeparatorChar });
                storeRootPath = Path.Combine(storeRootPath, relativePath);
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                storeRootPath = Path.Combine(storeRootPath, fileName);
            }
            return storeRootPath;
        }

        public string GetRelativePath(string fullLocalPath, bool pathIncludesFilename)
        {
            string str = pathIncludesFilename ? fullLocalPath.Substring(0, fullLocalPath.LastIndexOf(Path.DirectorySeparatorChar)) : fullLocalPath;
            return str.Replace(this.StoreRootPath, string.Empty).Trim(new char[] { Path.DirectorySeparatorChar });
        }

        public IEnumerable<IStoreFile> GetFiles(string relativePath, bool isOnlyCurrentFolder)
        {
            if (!IsValidPath(relativePath))
            {
                throw new ArgumentException("The provided path is invalid", "relativePath");
            }
            List<IStoreFile> list = new List<IStoreFile>();
            string fullLocalPath = this.GetFullLocalPath(relativePath, string.Empty);
            if (Directory.Exists(fullLocalPath))
            {
                SearchOption topDirectoryOnly = SearchOption.TopDirectoryOnly;
                if (!isOnlyCurrentFolder)
                {
                    topDirectoryOnly = SearchOption.AllDirectories;
                }
                foreach (FileInfo info in new DirectoryInfo(fullLocalPath).GetFiles("*.*", topDirectoryOnly))
                {
                    if ((info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        DefaultStoreFile file;
                        if (isOnlyCurrentFolder)
                        {
                            file = new DefaultStoreFile(relativePath, info);
                        }
                        else
                        {
                            file = new DefaultStoreFile(this.GetRelativePath(info.FullName, true), info);
                        }
                        list.Add(file);
                    }
                }
            }
            return list;
        }
    }
}
