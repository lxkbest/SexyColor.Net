using SexyColor.Infrastructure;
using MySqlSugar;
using System;

namespace SexyColor.BusinessComponents
{
    public class GoodsBrowseLogsRepository : Repository<GoodsBrowseLogs>, IGoodsBrowseLogsRepository
    {
        /// <summary>
        /// 获取商品浏览记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public long GetGoodsBrowseLogsCount(long userId)
        {
            var cacheKey = string.Format("{0}GetGoodsBrowseLogsCount:Userid-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Userid", userId), userId);
            object count = cacheService.Get(cacheKey);
            if (count == null)
            {
                using (var db = DbService.GetInstance())
                {
                    count = db.Queryable<GoodsBrowseLogs>().Where(m => m.Userid== userId).Count();
                }
                if (count != null)
                    cacheService.Add(cacheKey, count, CachingExpirationType.UsualObjectCollection);
            }

            return Convert.ToInt64(count);
        }

        /// <summary>
        /// 获取浏览记录分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsBrowseLogs> GetGoodsBrowseLogs(long userId, int pageIndex, int pageSize)
        {
            int totalCount,
                totalPage;
            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage,"userid="+userId, "date_created DESC", m=>m.Id);
        }
    }
}
