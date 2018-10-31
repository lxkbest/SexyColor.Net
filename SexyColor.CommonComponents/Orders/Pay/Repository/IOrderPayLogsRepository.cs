using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public interface IOrderPayLogsRepository
    {
        /// <summary>
        /// 支付记录分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<OrderPayLogs> GetPageOrderPayLogs(long orderId, int pageIndex, int pageSize);

        /// <summary>
        /// 判断订单是否有支付记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        bool IsExistsPayLogs(long orderId);

    }
}
