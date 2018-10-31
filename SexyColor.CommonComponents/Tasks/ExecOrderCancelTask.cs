using System;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public class ExecOrderCancelTask : ITask
    {
        private static int i = 1;
        public void Execute(TaskInfo task = null)
        {
            LoggerFactory.GetLogger().Error($"执行取消订单第{i}次");
            i++;
        }
    }
}
