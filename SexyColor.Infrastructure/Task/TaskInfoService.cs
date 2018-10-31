using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    public class TaskInfoService
    {
        private ITaskInfoRepository taskInfoRepository;

        public TaskInfoService() : this(new TaskInfoRepository())
        {
        }

        public TaskInfoService(ITaskInfoRepository taskInfoRepository)
        {
            this.taskInfoRepository = taskInfoRepository;
        }

        public TaskInfo GetTaskInfo(int taskId)
        {
            return taskInfoRepository.GetByCache(w => w.Id == taskId, taskId);
        }

        public IEnumerable<TaskInfo> GetTaskInfoAll()
        {
            return taskInfoRepository.GetAllByCache("id desc", w => w.Id);
        }

        public void SaveTaskStatus(TaskInfo entity)
        {
            taskInfoRepository.SaveTaskStatus(entity);
        }

        public void Update(TaskInfo entity)
        {
            taskInfoRepository.Update(entity);
            TaskSchedulerFactory.GetScheduler().Update(entity);
        }
    }
}
