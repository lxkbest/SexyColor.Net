namespace SexyColor.Infrastructure
{
    public static class TaskSchedulerFactory
    {
        private static ITaskScheduler _scheduler;

        public static ITaskScheduler GetScheduler()
        {
            if (_scheduler == null)
            {
                _scheduler = DIContainer.Resolve<ITaskScheduler>();
            }
            return _scheduler;
        }
    }
}
