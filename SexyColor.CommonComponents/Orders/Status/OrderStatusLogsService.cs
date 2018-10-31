using SexyColor.BusinessComponents;
using System;
using System.Collections.Generic;
using System.Text;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public class OrderStatusLogsService : IOrderStatusLogsService
    {
        public IOrderStatusLogsRepository orderStatusLogsRepository { get; set; }

        /// <summary>
        /// 变更状态记录弹出层分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OrderStatusLogs> GetPageOrderStatusLogs(long orderId, int pageIndex, int pageSize)
        {
            return orderStatusLogsRepository.GetPageOrderStatusLogs(orderId, pageIndex, pageSize);
        }

        /// <summary>
        /// 判断订单是否有状态变更记录
        /// </summary>
        /// <param name="oredrId"></param>
        /// <returns></returns>
        public bool IsExistsStatusLogs(long orderId)
        {
            return orderStatusLogsRepository.IsExistsStatusLogs(orderId);
        }
    }
}
