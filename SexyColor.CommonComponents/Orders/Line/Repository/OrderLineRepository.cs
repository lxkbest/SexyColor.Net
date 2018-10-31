using System;
using System.Collections.Generic;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.CommonComponents
{
    public class OrderLineRepository : Repository<OrderInfo>, IOrderLineRepository
    {
        /// <summary>
        /// 根据订单编号获取订单行列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IEnumerable<OrderLine> GetOrderLineEntitysByOrderid(long orderId)
        {
            var cacheKey = string.Format("{0}GetOrderLineEntitysByOrderid:Orderid-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Orderid", orderId), orderId);
            List<OrderLine> list = cacheService.Get<List<OrderLine>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<OrderLine>().Where(w => w.Orderid == orderId).ToList();
                }
                cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }


    }
}
