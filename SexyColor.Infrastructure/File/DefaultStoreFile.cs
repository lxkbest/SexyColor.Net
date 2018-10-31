using System;
using System.IO;

namespace SexyColor.Infrastructure
{
    public class DefaultStoreFile : IStoreFile
    {
        private readonly FileInfo fileInfo;
        private string fullLocalPath;
        private string relativePath;

        public DefaultStoreFile(string relativePath, FileInfo fileInfo)
        {
            this.relativePath = relativePath;
            this.fileInfo = fileInfo;
            this.fullLocalPath = fileInfo.FullName;
        }

        public Stream OpenReadStream()
        {
            return new FileStream(this.fileInfo.FullName, FileMode.Open, FileAccess.Read);
        }

        public string Extension
        {
            get
            {
                return this.fileInfo.Extension;
            }
        }

        public string FullLocalPath
        {
            get
            {
                return this.fullLocalPath;
            }
        }

        public DateTime LastModified
        {
            get
            {
                return this.fileInfo.LastWriteTime;
            }
        }

        public string Name
        {
            get
            {
                return this.fileInfo.Name;
            }
        }

        public string RelativePath
        {
            get
            {
                return this.relativePath;
            }
        }

        public long Size
        {
            get
            {
                return this.fileInfo.Length;
            }
        }
    }
}
