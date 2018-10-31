using System;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public class OrderInfoService : IOrderInfoService
    {
        //用户仓储
        public IUserRepository userRepository { get; set; }
        //订单仓储
        public IOrderInfoRepository orderInfoRepository { get; set; }
        //订单变更状态记录仓储
        public IOrderStatusLogsRepository orderStatusLogsRepository { get; set; }

        /// <summary>
        /// 后台订单分页
        /// </summary>
        /// <param name="orderInfoquery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OrderInfo> GetPageOrderInfo(OrderInfoQuery orderInfoquery, int pageIndex, int pageSize)
        {
            return orderInfoRepository.GetPageOrderInfo(orderInfoquery, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据订单流水号获取订单信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoByOrderNumber(string orderNumber)
        {
            return orderInfoRepository.GetOrderInfoByOrderNumber(orderNumber);
        }

        /// <summary>
        /// 根据订单编号获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoByOrderid(long orderId)
        {
            return orderInfoRepository.GetOrderInfoByOrderid(orderId);
        }

        /// <summary>
        /// 发货订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="logisticsCode"></param>
        /// <param name="waybillNumber"></param>
        /// <returns></returns>
        public bool SandOrder(long orderId, long userId, out OrderChangeStatus changeStauts, string logisticsCode = "", string waybillNumber = "")
        {
            changeStauts = OrderChangeStatus.Updated;
            if (orderId <= 0 || userId <= 0)
            {
                changeStauts = OrderChangeStatus.UnknownFailure;
                return false;
            }
            OrderInfo entity = orderInfoRepository.GetOrderInfoByOrderid(orderId);
            if (entity == null)
            {
                changeStauts = OrderChangeStatus.UnknownFailure;
                return false;
            }

            User userEntity = userRepository.GetUser(userId);
            if (userEntity == null)
            {
                changeStauts = OrderChangeStatus.UnknownFailure;
                return false;
            }

            if (string.IsNullOrWhiteSpace(logisticsCode) || string.IsNullOrWhiteSpace(waybillNumber))
            {
                changeStauts = OrderChangeStatus.LogisticsCodeOrWaybillNumberNULL;
                return false;
            }

            OrderModifyStatus currentStatus = (OrderModifyStatus)Enum.Parse(typeof(OrderModifyStatus), entity.Status.ToString());
            if (currentStatus != OrderModifyStatus.Send)
            {
                changeStauts = OrderChangeStatus.StatusException;
                return false;
            }

            entity.LogisticsCode = logisticsCode;
            entity.WaybillNumber = waybillNumber;
            entity.Status = (int)OrderModifyStatus.Complete;
            entity.LogisticsType = (int)OrderLogisticsMode.ExpressSend;
            entity.SendDate = DateTime.Now;


            var result = HandleOrder(entity, currentStatus, userEntity);
            return result != null ? true : false;
        }

        /// <summary>
        /// 处理订单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        private OrderInfo HandleOrder(OrderInfo entity, OrderModifyStatus currentStatus, User operationUser)
        {
            OrderInfo resultEntity = null;
            switch (currentStatus)
            {
                case OrderModifyStatus.Pay:
                    break;
                case OrderModifyStatus.Send:
                    resultEntity = HandleSendOrder(entity, currentStatus, operationUser);
                    break;
                case OrderModifyStatus.Cancel:
                    break;
                case OrderModifyStatus.Collect:
                    break;
                case OrderModifyStatus.Complete:
                    break;
                case OrderModifyStatus.Flish:
                    break;
                case OrderModifyStatus.Retreat:
                    break;
            }
            return resultEntity;
        }

        /// <summary>
        /// 处理发货
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        private OrderInfo HandleSendOrder(OrderInfo entity, OrderModifyStatus status, User operationUser)
        {
            try
            {
                int errorCount = 0;
                //更新订单
                var result = orderInfoRepository.UpdateCache(entity,
                     new
                     {
                         logistics_code = entity.LogisticsCode,
                         waybill_number = entity.WaybillNumber,
                         status = entity.Status,
                         logistics_type = entity.LogisticsType,
                         send_date = entity.SendDate
                     }, w => w.Orderid == entity.Orderid);

                if (!result)
                    errorCount++;

                var oldStatusName = Utility.GetDisplayByEnumMemberInfo(typeof(OrderModifyStatus), Enum.Parse(typeof(OrderModifyStatus), ((int)status).ToString()));

                //添加状态变更记录
                var statusLogsEntity = OrderStatusLogs.New();
                statusLogsEntity.Orderid = entity.Orderid;
                statusLogsEntity.OrderNumber = entity.OrderNumber;
                statusLogsEntity.Status = entity.Status;
                statusLogsEntity.StatusName = Utility.GetDisplayByEnumMemberInfo(typeof(OrderModifyStatus), Enum.Parse(typeof(OrderModifyStatus), entity.Status.ToString()));
                statusLogsEntity.DateCreated = DateTime.Now;
                statusLogsEntity.Userid = operationUser.UserId;
                statusLogsEntity.Username = operationUser.UserName;
                statusLogsEntity.Operation = $"用户：{operationUser.NickName}操作订单发货成功，订单状态由：{oldStatusName} 变更为 {statusLogsEntity.StatusName}";

                result = orderStatusLogsRepository.AddOrderStatusLogs(statusLogsEntity);
                if (!result)
                    errorCount++;

                return errorCount > 0 ? null : entity;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
