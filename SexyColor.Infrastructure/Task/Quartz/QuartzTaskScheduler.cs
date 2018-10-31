using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SexyColor.Infrastructure
{
    public class QuartzTaskScheduler : ITaskScheduler
    {
        private static List<TaskInfo> tasks;

        public QuartzTaskScheduler() : this(new TaskInfoRepository())
        {
        }

        public QuartzTaskScheduler(TaskInfoRepository repository)
        {
            if (tasks == null)
            {
                tasks = new TaskInfoService(repository).GetTaskInfoAll().ToList<TaskInfo>();
            }
        }

        public TaskInfo GetTask(int taskId)
        {
            return tasks.FirstOrDefault<TaskInfo>(w => (w.Id == taskId));
        }

        /// <summary>
        /// 重启
        /// </summary>
        public void ResumeAll()
        {
            this.Stop();
            this.Start();
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="name"></param>
        private void Remove(string name)
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            factory.GetScheduler().Result.DeleteJob(new JobKey(name));
        }

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="Id"></param>
        public void Run(int Id)
        {
            TaskInfo task = this.GetTask(Id);
            this.Run(task);
        }

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="task"></param>
        public void Run(TaskInfo task)
        {
            if (task != null)
            {
                Type type = Type.GetType(task.TaskType);
                if (type == null)
                {
                    LoggerFactory.GetLogger().Error(string.Format("任务： {0} 的taskType为空。", task.TaskName));
                }
                else
                {
                    ITask task2 = (ITask)Activator.CreateInstance(type);
                    if ((task2 != null) && !task.IsRuning)
                    {
                        try
                        {
                            task2.Execute(null);
                        }
                        catch (Exception ex)
                        {
                            LoggerFactory.GetLogger().Error(ex, string.Format("执行任务： {0} 出现异常。", task.TaskName));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存任务状态
        /// </summary>
        public void SaveTaskStatus()
        {
            foreach (TaskInfo info in tasks)
            {
                new TaskInfoService().SaveTaskStatus(info);
            }
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        public void Start()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler().Result;
            foreach (TaskInfo info in tasks)
            {
                if (info.IsEnabled)
                {
                    Type type = Type.GetType(info.TaskType);
                    if (type != null)
                    {
                        string name = type.Name + "_trigger";
                        IJobDetail jobDetail = JobBuilder.Create(typeof(QuartzTask)).WithIdentity(type.Name).Build();
                        jobDetail.JobDataMap.Add(new KeyValuePair<string, object>("Id", info.Id));
                        TriggerBuilder builder = TriggerBuilder.Create().WithIdentity(name).WithCronSchedule(info.TaskRule);
                        if (info.StartDate > DateTime.MinValue)
                        {
                            builder.StartAt(new DateTimeOffset(info.StartDate));
                        }
                        DateTime? endDate = info.EndDate;
                        DateTime startDate = info.StartDate;
                        if (endDate.HasValue ? (endDate.GetValueOrDefault() > startDate) : false)
                        {
                            DateTime? nullable2 = info.EndDate;
                            builder.EndAt(nullable2.HasValue ? new DateTimeOffset?(nullable2.GetValueOrDefault()) : null);
                        }
                        ICronTrigger trigger = (ICronTrigger)builder.Build();
                        DateTime dateTime = scheduler.ScheduleJob(jobDetail, trigger).Result.DateTime;
                        info.NextDate = new DateTime?(TimeZoneInfo.ConvertTime(dateTime, trigger.TimeZone));
                    }
                }
            }
            scheduler.Start();
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            factory.GetScheduler().Result.Shutdown(true);
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="task"></param>
        public void Update(TaskInfo task)
        {
            if (task != null)
            {
                int num = tasks.FindIndex(n => n.Id == task.Id);
                if (tasks[num] != null)
                {
                    task.TaskType = tasks[num].TaskType;
                    task.LastEndDate = tasks[num].LastEndDate;
                    task.LastStartDate = tasks[num].LastStartDate;
                    task.LastSuccess = tasks[num].LastSuccess;
                    tasks[num] = task;
                    Type type = Type.GetType(task.TaskType);
                    if (type != null)
                    {
                        this.Remove(type.Name);
                        if (task.IsEnabled)
                        {
                            string name = type.Name + "_trigger";
                            ISchedulerFactory factory = new StdSchedulerFactory();
                            IScheduler scheduler = factory.GetScheduler().Result;
                            IJobDetail jobDetail = JobBuilder.Create(typeof(QuartzTask)).WithIdentity(type.Name).Build();
                            jobDetail.JobDataMap.Add(new KeyValuePair<string, object>("Id", task.Id));
                            TriggerBuilder builder = TriggerBuilder.Create().WithIdentity(name).WithCronSchedule(task.TaskRule);
                            if (task.StartDate > DateTime.MinValue)
                            {
                                builder.StartAt(new DateTimeOffset(task.StartDate));
                            }
                            if (task.EndDate.HasValue)
                            {
                                DateTime? endDate = task.EndDate;
                                DateTime startDate = task.StartDate;
                                if (endDate.HasValue ? (endDate.GetValueOrDefault() > startDate) : false)
                                {
                                    DateTime? nullable3 = task.EndDate;
                                    builder.EndAt(nullable3.HasValue ? new DateTimeOffset?(nullable3.GetValueOrDefault()) : null);
                                }
                            }
                            ICronTrigger trigger = (ICronTrigger)builder.Build();
                            DateTime dateTime = scheduler.ScheduleJob(jobDetail, trigger).Result.DateTime;
                            task.NextDate = new DateTime?(TimeZoneInfo.ConvertTime(dateTime, trigger.TimeZone));
                        }
                    }
                }
            }
        }
    }
}
