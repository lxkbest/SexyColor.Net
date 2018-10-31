using System;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    public class GoodsCategoryLevel1Repository : Repository<GoodsCategoryLevel1>, IGoodsCategoryLevel1Repository
    {
        public string GetCategoryLevel1AllJson()
        {
            var cacheKey = string.Format("{0}GetCategoryLevel1AllJson", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.GlobalVersion));
            string json = cacheService.Get<string>(cacheKey);
            if (json == null)
            {
                using (var db = DbService.GetInstance())
                {
                    json = db.Sqlable().From("sc_goods_category_level1", "s")
                                 .SelectToJson("s.category_level1_id,s.category_name");
                }
                if(!string.IsNullOrEmpty(json))
                    cacheService.Add(cacheKey, json, CachingExpirationType.UsualObjectCollection);
            }
            return json;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdataCategoryLevel(GoodsCategoryLevel1 entity)
        {
            var result = base.UpdateByCache(entity);
            if (result)
                RealTimeCacheHelper.IncreaseGlobalVersion();
            return result;
        }
    }
}
