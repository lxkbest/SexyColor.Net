using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using MySqlSugar;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class CategoryItemInCharaRepository : Repository<CategoryItemInChara>, ICategoryItemInCharaRepository
    {
        /// <summary>
        /// 根据商品分类项获取包含特征项集合
        /// </summary>
        /// <param name="categoryItemId"></param>
        /// <returns></returns>
        public IEnumerable<CategoryItemInChara> GetCategoryItemInCharaByCategoryItemId(int categoryItemId)
        {
            var cacheKey = string.Format("{0}GetCategoryItemInCharaByCategoryItemId:CategoryItemId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "CategoryItemId", categoryItemId), categoryItemId);
            List<CategoryItemInChara> list = cacheService.Get<List<CategoryItemInChara>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<CategoryItemInChara>().Where(w => w.CategoryItemId == categoryItemId).ToList();
                }
                if (list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }
    }
}
