using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public interface IOrderStatusLogsRepository
    {
        /// <summary>
        /// 变更状态记录弹出层分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<OrderStatusLogs> GetPageOrderStatusLogs(long orderId, int pageIndex, int pageSize);

        /// <summary>
        /// 判断订单是否有状态变更记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        bool IsExistsStatusLogs(long orderId);

        /// <summary>
        /// 添加订单变更状态记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool AddOrderStatusLogs(OrderStatusLogs entity);
    }
}
