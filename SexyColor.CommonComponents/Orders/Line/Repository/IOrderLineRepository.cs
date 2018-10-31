using System.Collections.Generic;
using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public interface IOrderLineRepository
    {
        /// <summary>
        /// 根据订单编号获取订单行列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        IEnumerable<OrderLine> GetOrderLineEntitysByOrderid(long orderId);

    }
}
