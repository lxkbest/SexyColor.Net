using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using MySqlSugar;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class GoodsCategoryInItemRepository : Repository<GoodsCategoryInItem>, IGoodsCategoryInItemRepository
    {
        /// <summary>
        /// 根据商品分类获取分类项
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsCategoryInItem> GetGoodsCategoryInItemByCategoryId(int categoryId)
        {
            var cacheKey = string.Format("{0}GetGoodsCategoryInItemByCategoryId:CategoryId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "CategoryId", categoryId), categoryId);
            List<GoodsCategoryInItem> list = cacheService.Get<List<GoodsCategoryInItem>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<GoodsCategoryInItem>().Where(w => w.CategoryId == categoryId).ToList();
                }
                if (list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }
    }
}
