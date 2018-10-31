using System.Collections;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IOrderPayLogsService
    {
        
        /// <summary>
        /// 支付记录分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<OrderPayLogs> GetPageOrderPayLogs(long orderId, int value, int pageSize);

        /// <summary>
        /// 判断订单是否有支付记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        bool IsExistsPayLogs(long orderId);

    }
}
