using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IOperationLogsRepository : IRepository<OperationLogs>
    {
        /// <summary>
        /// 获取操作日志分页
        /// </summary>
        /// <param name="operationLogsQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<OperationLogs> GetPageOperationLogs(OperationLogsQuery operationLogsQuery, int pageIndex, int pageSize);
    }
}
