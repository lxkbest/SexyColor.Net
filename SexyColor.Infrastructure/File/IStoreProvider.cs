using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;


namespace SexyColor.Infrastructure
{
    public interface IStoreProvider
    {
        IStoreFile AddOrUpdateFile(string relativePath, string fileName, Stream contentStream);
        void DeleteFolder(string relativePath);
        string GetDirectlyUrl(string relativeFileName);
        string GetDirectlyUrl(string relativeFileName, DateTime lastModified);
        string GetDirectlyUrl(string relativePath, string fileName);
        string GetDirectlyUrl(string relativePath, string fileName, DateTime lastModified);
        string GetSizeImageName(string filename, Size size, ResizeMethod resizeMethod);
        string JoinDirectory(params string[] directoryParts);
        string GetFullLocalPath(string relativeFileName);
        string GetFullLocalPath(string relativePath, string fileName);
        string GetRelativePath(string fullLocalPath, bool pathIncludesFilename);
        IEnumerable<IStoreFile> GetFiles(string relativePath, bool isOnlyCurrentFolder);
    }
}
