using System;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public class OrderStatusLogsRepository : Repository<OrderStatusLogs>, IOrderStatusLogsRepository
    {
        /// <summary>
        /// 变更状态记录弹出层分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OrderStatusLogs> GetPageOrderStatusLogs(long orderId, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = " orderid = @Orderid ";
            string orderBy = "date_created DESC";
            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, new { Orderid = orderId }, orderBy, i => i.Id);
        }

        /// <summary>
        /// 判断订单是否有状态变更记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool IsExistsStatusLogs(long orderId)
        {
            int count = base.GetInt("SELECT 1 from sc_order_status_logs s WHERE EXISTS (SELECT * FROM sc_order_status_logs where s.orderid = @Orderid)", new { Orderid = orderId });
            return count > 0 ? true : false;
        }

        /// <summary>
        /// 添加订单变更状态记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddOrderStatusLogs(OrderStatusLogs entity)
        {
            int result = 0;
            object objId = base.AddByCache(entity, true);
            int.TryParse(objId.ToString(), out result);
            return result > 0 ? true : false;
        }
    }
}
