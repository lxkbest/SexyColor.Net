using System;
using System.Collections.Generic;
using System.IO;

namespace SexyColor.CommonComponents
{
    public interface ILogger
    {
        void Error(Exception ex);
        List<FileInfo> GetLogs();
        void Info(string msg);
        string ReadLog();
        string ReadLog(DateTime date);
    }
}
