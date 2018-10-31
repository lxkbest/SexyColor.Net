using System;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public class OrderPayLogsService : IOrderPayLogsService
    {
        public IOrderPayLogsRepository orderPayLogsRepository { get; set; }


        /// <summary>
        /// 支付记录分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OrderPayLogs> GetPageOrderPayLogs(long orderId, int pageIndex, int pageSize)
        {
            return orderPayLogsRepository.GetPageOrderPayLogs(orderId, pageIndex, pageSize);
        }

        /// <summary>
        /// 判断订单是否有支付记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool IsExistsPayLogs(long orderId)
        {
            return orderPayLogsRepository.IsExistsPayLogs(orderId);
        }
    }
}
