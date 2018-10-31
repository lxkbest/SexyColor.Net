using System;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    public class GoodsCollectionLogsRepository : Repository<GoodsCollectionLogs>, IGoodsCollectionLogsRepository
    {
        /// <summary>
        /// 获取商品收藏分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsCollectionLogs> GetGoodsCollectionLogs(long userId, int pageIndex, int pageSize)
        {
            int totalCount,
                totalPage;
            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, "userid=" + userId, "date_created DESC", m => m.Id);
        }

        /// <summary>
        /// 获取商品收藏数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public long GetGoodsCollectionLogsCount(long userId)
        {
            var cacheKey = string.Format("{0}GetGoodsCollectionLogsCount:Userid-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Userid", userId), userId);
            object count = cacheService.Get(cacheKey);
            if (count == null)
            {
                using (var db = DbService.GetInstance())
                {
                    count = db.Queryable<GoodsCollectionLogs>().Where(m => m.Userid == userId).Count();
                }
                if (count != null)
                    cacheService.Add(cacheKey, count, CachingExpirationType.UsualObjectCollection);
            }

            return Convert.ToInt64(count);
        }
    }
}
