using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.CommonComponents
{

    public interface IStorageService
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        string CreateFolder(string folder);
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        string DeleteFolder(string folder);
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string SaveFile(string file);
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string DeleteFile(string file);
    }
}
