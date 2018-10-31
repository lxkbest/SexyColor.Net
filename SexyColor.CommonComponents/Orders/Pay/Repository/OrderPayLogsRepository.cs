using System;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public class OrderPayLogsRepository : Repository<OrderPayLogs>, IOrderPayLogsRepository
    {
        /// <summary>
        /// 支付记录分页
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OrderPayLogs> GetPageOrderPayLogs(long orderId, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = " orderid = @Orderid ";
            string orderBy = "date_created DESC";
            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, new { Orderid = orderId }, orderBy, i => i.Id);
        }

        /// <summary>
        /// 判断订单是否有支付记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool IsExistsPayLogs(long orderId)
        {
            int count = base.GetInt("SELECT 1 from sc_order_pay_logs s WHERE EXISTS (SELECT * FROM sc_order_pay_logs where s.orderid = @Orderid)", new { Orderid = orderId });
            return count > 0 ? true : false;
        }
    }
}
