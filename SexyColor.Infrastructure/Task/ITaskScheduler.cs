using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{

    public interface ITaskScheduler
    {
        TaskInfo GetTask(int Id);
        void ResumeAll();
        void Run(int Id);
        void SaveTaskStatus();
        void Start();
        void Stop();
        void Update(TaskInfo task);
    }
}
