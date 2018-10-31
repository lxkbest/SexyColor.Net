
namespace SexyColor.Infrastructure
{
    public interface ITaskInfoRepository : IRepository<TaskInfo>
    {
        void SaveTaskStatus(TaskInfo entity);
    }
}
