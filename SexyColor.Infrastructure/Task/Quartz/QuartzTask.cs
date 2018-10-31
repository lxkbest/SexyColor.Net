using System;
using System.Threading.Tasks;
using Quartz;

namespace SexyColor.Infrastructure
{
    public class QuartzTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            int @int = context.JobDetail.JobDataMap.GetInt("Id");
            TaskInfo task = TaskSchedulerFactory.GetScheduler().GetTask(@int);
            if (task == null)
            {
                throw new ArgumentException("Not found task ：" + task.TaskName);
            }
            TaskInfoService service = new TaskInfoService();
            task.IsRuning = true;
            DateTime startDate = DateTime.Now;
            service.SaveTaskStatus(task);
            try
            {
                ((ITask)Activator.CreateInstance(Type.GetType(task.TaskType))).Execute(task);
                task.LastSuccess = true;
            }
            catch (Exception ex)
            {
                LoggerFactory.GetLogger().Error(ex, string.Format("Exception while running job {0} of type {1}", context.JobDetail.Key, context.JobDetail.JobType.ToString()));
                task.LastSuccess = false;
            }
            task.IsRuning = false;
            task.LastStartDate = new DateTime?(startDate);
            if (context.NextFireTimeUtc.HasValue)
            {
                DateTime nextDate = context.NextFireTimeUtc.Value.ToLocalTime().DateTime;
                task.NextDate = new DateTime?(nextDate);
            }
            else
            {
                task.NextDate = null;
            }
            task.LastEndDate = new DateTime?(DateTime.Now);
            service.SaveTaskStatus(task);

            return Task.CompletedTask;
        }
    }
}
