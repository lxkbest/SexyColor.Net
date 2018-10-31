using SexyColor.Infrastructure;
using System.Collections.Generic;
using MySqlSugar;
using System;

namespace SexyColor.BusinessComponents
{
    public class GoodsCategoryLevel2Repository : Repository<GoodsCategoryLevel2>, IGoodsCategoryLevel2Repository
    {
        /// <summary>
        /// 根据一级分类编号获取二级分类
        /// </summary>
        /// <param name="level1Id"></param>
        /// <returns></returns>
        public IEnumerable<GoodsCategoryLevel2> GetCategoryLevel2ByLevel1Id(int categoryLevel1Id)
        {
            var cacheKey = string.Format("{0}GetCategoryLevel2ByLevel1Id:CategoryLevel1Id-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "CategoryLevel1Id", categoryLevel1Id), categoryLevel1Id);
            List <GoodsCategoryLevel2> list = cacheService.Get<List<GoodsCategoryLevel2>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list =  db.Queryable<GoodsCategoryLevel2>().Where(w => w.CategoryLevel1Id == categoryLevel1Id).ToList();
                    //list = db.Sqlable().From("sc_goods_category_level2", "s")
                    //             .Where("s.category_level1_id=@category_level1_id")
                    //             .SelectToList<GoodsCategoryLevel2>("s.*", new { category_level1_id = level1Id });
                }
                if(list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdataCategoryLevel(GoodsCategoryLevel2 entity, int oldCategoryLevel2Id)
        {
            var result = base.UpdateByCache(entity);
            if (result && oldCategoryLevel2Id > 0 && entity.CategoryLevel1Id != oldCategoryLevel2Id)
                RealTimeCacheHelper.IncreaseAreaVersion("CategoryLevel1Id", oldCategoryLevel2Id);
            return result;
        }

        /// <summary>
        /// 根据一级分类编号获取二级分类JSON
        /// </summary>
        /// <returns></returns>
        public string GetCategoryLevel2Json(int categoryLevelId)
        {
            var cacheKey = string.Format("{0}GetCategoryLevel2Json", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "CategoryLevel1Id", categoryLevelId));
            string json = cacheService.Get<string>(cacheKey);
            if (json == null)
            {
                using (var db = DbService.GetInstance())
                {
                    json = db.Sqlable().From("sc_goods_category_level2", "s")
                                 .Where("category_level1_id=@category_level1_id")
                                 .SelectToJson("s.category_level2_id,s.category_name", new { category_level1_id = categoryLevelId });
                }
                if (!string.IsNullOrEmpty(json))
                    cacheService.Add(cacheKey, json, CachingExpirationType.UsualObjectCollection);
            }
            return json;
        }
    }
}
