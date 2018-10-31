using System.Collections.Generic;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    public class BasicsAreasRepository : Repository<BasicsAreas>, IBasicsAreasRepository
    {
        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IEnumerable<BasicsAreas> GetAreas(int pid)
        {
            var cacheKey = string.Format("{0}GetAreas:PId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "PId", pid), pid);
            List<BasicsAreas> areas = cacheService.Get<List<BasicsAreas>>(cacheKey);
            if (areas == null)
            {
                using (var db = DbService.GetInstance())
                {
                    areas = db.Sqlable().From("sc_basics_areas", "s")
                                 .Where("s.cityid=@cityid")
                                 .SelectToList<BasicsAreas>("s.*", new { cityid = pid });
                }
                if(areas != null)
                    cacheService.Add(cacheKey, areas, CachingExpirationType.UsualObjectCollection);
            }
            return areas;
        }
    }
}
