using System;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using MySqlSugar;
using System.Text;
using System.Linq.Expressions;

namespace SexyColor.CommonComponents
{
    public class OrderInfoRepository : Repository<OrderInfo>, IOrderInfoRepository
    {

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateCache(OrderInfo entity)
        {
            return base.UpdateByCache(entity);
        }

        /// <summary>
        /// 按照更新参数更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="obj"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public bool UpdateCache(OrderInfo entity, object obj, Expression<Func<OrderInfo, bool>> lambda)
        {
            var result = base.UpdateByCache(entity, obj, lambda);
            return result;
        }

        /// <summary>
        /// 后台订单分页
        /// </summary>
        /// <param name="orderInfoquery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OrderInfo> GetPageOrderInfo(OrderInfoQuery orderInfoquery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleOrderByEunm(orderInfoquery.OrderInfoSortBy, out orderBy);
            object pars = new object();
            whereSql = HandleQueryBySqlable(orderInfoquery, out pars);
            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Orderid);
        }

        /// <summary>
        /// 处理SQL条件
        /// </summary>
        /// <param name="orderInfoQuery"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        private string HandleQueryBySqlable(OrderInfoQuery orderInfoQuery, out object pars)
        {
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            var sqlTable = new Sqlable();
            sqlTable.Sql = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(orderInfoQuery.Buyers))
            {
                sqlTable = sqlTable.Where("buyers_name = @Buyers");
                paramsDictionary.Add("Buyers", orderInfoQuery.Buyers);
            }
            if (!string.IsNullOrWhiteSpace(orderInfoQuery.BuyersPhone))
            {
                sqlTable = sqlTable.Where("buyers_phone = @BuyersPhone");
                paramsDictionary.Add("BuyersPhone", orderInfoQuery.BuyersPhone);
            }
            if (!string.IsNullOrWhiteSpace(orderInfoQuery.OrderNumber))
            {
                sqlTable = sqlTable.Where("order_number = @OrderNumber");
                paramsDictionary.Add("OrderNumber", orderInfoQuery.OrderNumber);
            }
            if (!string.IsNullOrWhiteSpace(orderInfoQuery.WaybillNumber))
            {
                sqlTable = sqlTable.Where("waybill_number = @WaybillNumber");
                paramsDictionary.Add("WaybillNumber", orderInfoQuery.WaybillNumber);
            }
            if (!string.IsNullOrWhiteSpace(orderInfoQuery.OtherOrderNumber))
            {
                sqlTable = sqlTable.Where("other_transaction_code = @OtherOrderNumber");
                paramsDictionary.Add("OtherOrderNumber", orderInfoQuery.OtherOrderNumber);
            }

            if (orderInfoQuery.IsUse.HasValue)
            {
                sqlTable = sqlTable.Where("is_use = @IsUse");
                paramsDictionary.Add("IsUse", orderInfoQuery.IsUse);
            }

            if (orderInfoQuery.IsCompleteComment.HasValue)
            {
                sqlTable = sqlTable.Where("is_complete_comment = @IsCompleteComment");
                paramsDictionary.Add("IsCompleteComment", orderInfoQuery.IsCompleteComment);
            }

            if (orderInfoQuery.OrderType.HasValue)
            {
                sqlTable = sqlTable.Where("order_type = @OrderType");
                paramsDictionary.Add("OrderType", (int)orderInfoQuery.OrderType.Value);
            }

            if (orderInfoQuery.OrderRightsStatus.HasValue)
            {
                sqlTable = sqlTable.Where("rights_status = @OrderRightsStatus");
                paramsDictionary.Add("OrderRightsStatus", (int)orderInfoQuery.OrderRightsStatus.Value);
            }

            if (orderInfoQuery.OrderLogisticsMode.HasValue)
            {
                sqlTable = sqlTable.Where("logistics_type = @OrderLogisticsMode");
                paramsDictionary.Add("OrderLogisticsMode", (int)orderInfoQuery.OrderLogisticsMode.Value);
            }
 

            if (orderInfoQuery.OrderInfoStatus.HasValue)
            {
                sqlTable = sqlTable.Where("status = @Status");
                paramsDictionary.Add("Status", (int)orderInfoQuery.OrderInfoStatus.Value);
            }

            if (orderInfoQuery.OrderPayType.HasValue)
            {
                sqlTable = sqlTable.Where("pay_type = @PayType");
                paramsDictionary.Add("PayType", (int)orderInfoQuery.OrderPayType.Value);
            }

            if (orderInfoQuery.RealPriceLowerLimit.HasValue)
            {
                sqlTable = sqlTable.Where("real_price >= @RealPriceLowerLimit");
                paramsDictionary.Add("RealPriceLowerLimit", orderInfoQuery.RealPriceLowerLimit);
            }
            if (orderInfoQuery.RealPriceUpperLimit.HasValue)
            {
                sqlTable = sqlTable.Where("real_price <= @RealPriceUpperLimit");
                paramsDictionary.Add("RealPriceUpperLimit", orderInfoQuery.RealPriceUpperLimit);
            }

            if (orderInfoQuery.OrderTimeLowerLimit.HasValue)
            {
                sqlTable = sqlTable.Where("dete_created >= @OrderTimeLowerLimit");
                paramsDictionary.Add("OrderTimeLowerLimit", orderInfoQuery.OrderTimeLowerLimit.Value);
            }
            if (orderInfoQuery.OrderTimeUpperLimit.HasValue)
            {
                sqlTable = sqlTable.Where("dete_created <= @OrderTimeUpperLimit");
                paramsDictionary.Add("OrderTimeUpperLimit", orderInfoQuery.OrderTimeUpperLimit.Value);
            }

            pars = paramsDictionary;

            foreach (var item in sqlTable.Where)
            {
                sqlTable.Sql.Append(item);
            }
            return sqlTable.Sql.ToString().TrimStart(" AND".ToCharArray());
        }

        /// <summary>
        /// 处理排序条件
        /// </summary>
        /// <param name="value"></param>
        /// <param name="orderBy"></param>
        private void HandleOrderByEunm(OrderInfoSortBy value, out string orderBy)
        {
            orderBy = string.Empty;
            switch (value)
            {
                case OrderInfoSortBy.Keynum_ASC:
                    orderBy = "orderid ASC";
                    break;
                case OrderInfoSortBy.Keynum_DESC:
                    orderBy = "orderid DESC";
                    break;
                case OrderInfoSortBy.Price_ASC:
                    orderBy = "real_price ASC";
                    break;
                case OrderInfoSortBy.Price_DESC:
                    orderBy = "real_price DESC";
                    break;
                case OrderInfoSortBy.DateCreated_ASC:
                    orderBy = "date_created ASC";
                    break;
                case OrderInfoSortBy.DateCreated_DESC:
                    orderBy = "date_created DESC";
                    break;
                case OrderInfoSortBy.OrderNumber_ASC:
                    orderBy = "order_number ASC";
                    break;
                case OrderInfoSortBy.OrderNumber_DESC:
                    orderBy = "order_number DESC";
                    break;
            }
        }

        /// <summary>
        /// 根据订单流水号获取订单信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoByOrderNumber(string orderNumber)
        {
            OrderInfo info =  base.GetByCache(w => w.OrderNumber == orderNumber, orderNumber);
            return info;
        }

        /// <summary>
        /// 根据订单编号获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoByOrderid(long orderId)
        {
            OrderInfo info = base.GetByCache(w => w.Orderid == orderId, orderId);
            return info;
        }
    }
}
