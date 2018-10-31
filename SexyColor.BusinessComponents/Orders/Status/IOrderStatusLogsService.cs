using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IOrderStatusLogsService
    {
        /// <summary>
        /// 判断订单是否有状态变更记录
        /// </summary>
        /// <param name="oredrId"></param>
        /// <returns></returns>
        bool IsExistsStatusLogs(long oredrId);

        /// <summary>
        /// 变更状态记录弹出层分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<OrderStatusLogs> GetPageOrderStatusLogs(long orderId, int pageIndex, int pageSize);
    }
}
