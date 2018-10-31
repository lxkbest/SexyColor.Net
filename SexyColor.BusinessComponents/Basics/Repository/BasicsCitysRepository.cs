using System.Collections.Generic;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    public class BasicsCitysRepository : Repository<BasicsCitys>, IBasicsCitysRepository
    {
     
        public IEnumerable<BasicsCitys> GetCitys(int pid)
        {
            //格式：{0}GetRolesByConnectToUser:ConnectToUser-{1}:ConnectToUser2参数-{3}:ConnectToUser2参数-{3}

            var cacheKey = string.Format("{0}GetCitys:PId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "PId", pid), pid);
            List<BasicsCitys> citys = cacheService.Get<List<BasicsCitys>>(cacheKey);

            if (citys == null)
            {
                using (var db = DbService.GetInstance())
                {
                    citys = db.Sqlable().From("sc_basics_citys", "s")
                                 .Where("s.provinceid=@provinceid")
                                 .SelectToList<BasicsCitys>("s.*", new { Provinceid = pid });
                }
                if(citys != null)
                    cacheService.Add(cacheKey, citys, CachingExpirationType.UsualObjectCollection);
            }
            return citys;
        }
    }
}
