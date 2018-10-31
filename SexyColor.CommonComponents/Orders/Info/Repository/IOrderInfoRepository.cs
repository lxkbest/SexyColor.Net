using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SexyColor.CommonComponents
{
    public interface IOrderInfoRepository
    {

        /// <summary>
        /// 更新实体（带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool UpdateCache(OrderInfo entity);

        /// <summary>
        /// 按照更新参数更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="obj"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        bool UpdateCache(OrderInfo entity, object obj, Expression<Func<OrderInfo, bool>> lambda);

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
    }
}
