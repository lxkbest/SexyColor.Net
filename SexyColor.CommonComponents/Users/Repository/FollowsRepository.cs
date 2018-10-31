using System;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using MySqlSugar;
using System.Linq;

namespace SexyColor.CommonComponents
{
    public class FollowsRepository : Repository<Follows>, IFollowsRepository
    {
        public IEnumerable<long> GetFocusIds(long userId)
        {
            var cacheKey = string.Format("{0}GetFocusIds:UserId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "UserId", userId), userId);
            List<string> lists = cacheService.Get<List<string>>(cacheKey);

            if (lists == null)
            {
                using (var db = DbService.GetInstance())
                {
                    lists = db.Sqlable().From("sc_follows", "s")
                                 .Where("s.followed_userid=@userId")
                                 .SelectToList<string>("s.userid", new { userId = userId });
                }
                if (lists != null)
                    cacheService.Add(cacheKey, lists, CachingExpirationType.UsualObjectCollection);
            }
            IEnumerable<long> ids = lists.Select(long.Parse).ToList();
            return ids;
        }

        public IEnumerable<long> GetFollowedIds(long userId)
        {
            //格式：{0}GetRolesByConnectToUser:ConnectToUser-{1}:ConnectToUser2参数-{3}:ConnectToUser2参数-{3}

            var cacheKey = string.Format("{0}GetFollowedIds:UserId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "UserId", userId), userId);
            List<string> lists = cacheService.Get<List<string>>(cacheKey);

            if (lists == null)
            {
                using (var db = DbService.GetInstance())
                {
                    lists = db.Sqlable().From("sc_follows", "s")
                                 .Where("s.userid=@userId")
                                 .SelectToList<string>("s.followed_userid", new { userId = userId });
                }
                if(lists != null)
                    cacheService.Add(cacheKey, lists, CachingExpirationType.UsualObjectCollection);
            }
            IEnumerable<long> ids = lists.Select(long.Parse).ToList(); ;
            return ids;
        }
    }
}
