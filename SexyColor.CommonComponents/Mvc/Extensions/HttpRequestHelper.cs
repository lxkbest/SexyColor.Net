using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public static class HttpRequestHelper
    {
        const string ImagePath = "~/UpLoad/Images";
        const string FilePath = "~/UpLoad/Files";

        /// <summary>
        /// 初始化路径
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string InitPath(IServiceProvider serviceProvider, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += string.Format("\\{0}\\", DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                var storage = DIContainer.Resolve<IStorageService>();
                if (storage != null)
                {
                    storage.CreateFolder(path);
                }
            }
            return path;
        }

        /// <summary>
        /// 路径转换目录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(this HttpRequest request, string path)
        {
            var environment = request.HttpContext.RequestServices.GetService<IHostingEnvironment>();
            return Path.Combine(environment.WebRootPath, path.Replace("~/", "")).Replace("/", "\\");
        }
        /// <summary>
        /// 保存图片到UpLoad/Images
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string SaveImage(this HttpRequest request)
        {
            if (request.Form.Files.Count > 0 && request.Form.Files[0].Length > 0)
            {
                string path = InitPath(request.HttpContext.RequestServices, request.MapPath(ImagePath));
                string fileName = request.Form.Files[0].FileName;
                string ext = Path.GetExtension(fileName);
                if (CommonHelper.IsImage(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Form.Files[0].SaveAs(path);
                    var storage = DIContainer.Resolve<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~").Replace("\\", "/");
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存图片到UpLoad/Images
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string SaveImage(this HttpRequest request, string name)
        {
            if (request.Form.Files.Count > 0 && request.Form.Files[name].Length > 0)
            {
                string path = InitPath(request.HttpContext.RequestServices, request.MapPath(ImagePath));
                string fileName = request.Form.Files[name].FileName;
                string ext = Path.GetExtension(fileName);
                if (CommonHelper.IsImage(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Form.Files[name].SaveAs(path);
                    var storage = DIContainer.Resolve<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 保存文件到UpLoad/Files
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string SaveFile(this HttpRequest request)
        {
            if (request.Form.Files.Count > 0 && request.Form.Files[0].Length > 0)
            {
                string path = InitPath(request.HttpContext.RequestServices, request.MapPath(FilePath));
                string fileName = request.Form.Files[0].FileName;
                string ext = Path.GetExtension(fileName);
                if (CommonHelper.FileCanUp(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Form.Files[0].SaveAs(path);
                    var storage = DIContainer.Resolve<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }
        public static string SaveFile(this HttpRequest request, string name)
        {
            if (request.Form.Files.Count > 0 && request.Form.Files[0].Length > 0)
            {
                string path = InitPath(request.HttpContext.RequestServices, request.MapPath(FilePath));
                string fileName = request.Form.Files[0].FileName;
                string ext = System.IO.Path.GetExtension(fileName);
                if (CommonHelper.FileCanUp(ext))
                {
                    fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    path += fileName;
                    request.Form.Files[0].SaveAs(path);
                    var storage = DIContainer.Resolve<IStorageService>();
                    if (storage != null)
                    {
                        string filePath = storage.SaveFile(path);
                        if (filePath.IsNotNullAndWhiteSpace())
                        {
                            return filePath;
                        }
                    }
                    return path.Replace(request.MapPath("~/"), "~/").Replace("\\", "/");
                }
            }
            return string.Empty;
        }

        public static void DeleteFile(this HttpRequest request, string filePath)
        {
            string file = request.MapPath(filePath);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            var storage = DIContainer.Resolve<IStorageService>();
            if (storage != null)
            {
                storage.DeleteFile(file);
            }
        }
    }
}
