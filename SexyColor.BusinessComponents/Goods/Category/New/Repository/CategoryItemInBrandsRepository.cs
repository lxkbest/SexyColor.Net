using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using MySqlSugar;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class CategoryItemInBrandsRepository : Repository<CategoryItemInBrands>, ICategoryItemInBrandsRepository
    {
        /// <summary>
        /// 根据商品分类项获取包含品牌项集合
        /// </summary>
        /// <param name="categoryItemId"></param>
        /// <returns></returns>
        public IEnumerable<CategoryItemInBrands> GetCategoryItemInBrandsByCategoryItemId(int categoryItemId)
        {
            var cacheKey = string.Format("{0}GetCategoryItemInBrandsByCategoryItemId:CategoryItemId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "CategoryItemId", categoryItemId), categoryItemId);
            List<CategoryItemInBrands> list = cacheService.Get<List<CategoryItemInBrands>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<CategoryItemInBrands>().Where(w => w.CategoryItemId == categoryItemId).ToList();
                }
                if (list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }
    }
}
