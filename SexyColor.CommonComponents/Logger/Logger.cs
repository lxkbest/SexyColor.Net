using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SexyColor.CommonComponents
{
    public class Logger : ILogger
    {
        public const string Path = "Logs";
        public const string TitleTemplate = "----------------------------------------------------------------\r\n时间：{0}\r\n详细信息:\r\n";
        public const string Split = "\r\n----------------------------------------------------------------\r\n";
        public const string FileTemplate = "LOG_{0}.txt";
        public const string DateNameTemplate = "yyyy-MM-dd";
        private readonly IHostingEnvironment _hostingEnvironment;
        public Logger(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteInfo(string msg)
        {
            lock (Path)
            {
                string logPath = GetPath(DateTime.Now);
                FileStream fs = new FileStream(logPath, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine(string.Format(TitleTemplate, DateTime.Now) + msg + Split);
                writer.Dispose();
                fs.Dispose();
            }
        }
        /// <summary>
        /// 获取日志的目录
        /// </summary>
        /// <returns></returns>
        private string GetDir()
        {
            string logPath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, Path);
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            return logPath;
        }
        
        /// <summary>
        /// 获取日志的路径
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetPath(DateTime date)
        {
            string logPath = GetDir();
            string fileName = string.Format(FileTemplate, date.ToString(DateNameTemplate));
            logPath += "\\" + fileName;
            return logPath;
        }
        
        /// <summary>
        /// 自己想写入日志使用
        /// </summary>
        /// <param name="msg"></param>
        public void Info(string msg)
        {
            WriteInfo(msg);
        }
        /// <summary>
        /// 发生错误处理
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            string msg = ex.Message + "\r\n";
            msg += ex.ToString();
            WriteInfo(msg);
        }

        /// <summary>
        /// 读取当天日志文件信息
        /// </summary>
        /// <returns></returns>
        public string ReadLog()
        {
            return ReadLog(DateTime.Now);
        }

        /// <summary>
        /// 按日期读取日志文件信息
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ReadLog(DateTime date)
        {
            string logPath = GetPath(date);
            FileStream fs = new FileStream(logPath, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            string s = reader.ReadToEnd();
            reader.Dispose();
            fs.Dispose();
            return s;
        }

        /// <summary>
        /// 获取目录下所有日志文件
        /// </summary>
        /// <returns></returns>
        public List<FileInfo> GetLogs()
        {
            string logPath = GetDir();
            string[] files = Directory.GetFiles(logPath, "LOG_*.txt");
            List<FileInfo> results = new List<FileInfo>();
            foreach (var item in files)
            {
                FileInfo file = new FileInfo(item);
                results.Add(file);
            }
            return results;
        }
    }
}
