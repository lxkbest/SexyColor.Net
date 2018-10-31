using System.Collections;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IOrderLineService
    {
        /// <summary>
        /// 根据订单编号获取订单行列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        IEnumerable<OrderLine> GetOrderLineEntitysByOrderid(long orderId);
    }
}
