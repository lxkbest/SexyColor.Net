using System;
using System.IO;

namespace SexyColor.Infrastructure
{
    public interface IStoreFile
    {
        /// <summary>
        /// 获取用于读取文件的Stream
        /// </summary>
        /// <returns></returns>
        Stream OpenReadStream();

        /// <summary>
        /// 文件扩展名
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime LastModified { get; }

        /// <summary>
        /// 文件名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 相对StoragePath的路径
        /// </summary>
        string RelativePath { get; }

        /// <summary>
        /// 文件大小
        /// </summary>
        long Size { get; }
    }
}
