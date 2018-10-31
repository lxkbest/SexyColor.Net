using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IOrderInfoService
    {
        /// <summary>
        /// 后台订单分页
        /// </summary>
        /// <param name="orderInfoquery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<OrderInfo> GetPageOrderInfo(OrderInfoQuery orderInfoquery, int pageIndex, int pageSize);

        /// <summary>
        /// 根据订单流水号获取订单信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        OrderInfo GetOrderInfoByOrderNumber(string orderNumber);

        /// <summary>
        /// 根据订单编号获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderInfo GetOrderInfoByOrderid(long orderId);

        /// <summary>
        /// 发货订单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name=""></param>
        bool SandOrder(long orderId, long userId, out OrderChangeStatus changeStauts, string logisticsCode = "", string waybillNumber = "");
    }
}
