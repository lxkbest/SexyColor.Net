using System;

namespace SexyColor.Infrastructure
{
    public class TaskInfoRepository : Repository<TaskInfo>, ITaskInfoRepository
    {
        public void SaveTaskStatus(TaskInfo entity)
        {
            base.Update(entity);
        }
    }
}
