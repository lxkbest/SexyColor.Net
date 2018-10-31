using System;
using System.Collections.Generic;
using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public class OrderLineService : IOrderLineService
    {
        public IOrderLineRepository orderLineRepository { get; set; }

        /// <summary>
        /// 根据订单编号获取订单行列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IEnumerable<OrderLine> GetOrderLineEntitysByOrderid(long orderId)
        {
            return orderLineRepository.GetOrderLineEntitysByOrderid(orderId);
        }
    }
}
