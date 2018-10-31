using System.Collections.Generic;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    public class GoodsInDetailImageRepository : Repository<GoodsInDetailImage>, IGoodsInDetailImageRepository
    {
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntityByCache(GoodsInDetailImage entity)
        {
            var result = base.Delete(m => m.Id == entity.Id);
            if (result)
            {
                OnDeleted(entity);
                RealTimeCacheHelper.IncreaseAreaVersion("GoodsId", entity.Goodsid);
            }
            return result;
        }

        /// <summary>
        /// 根据goodid获取所有实体
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsInDetailImage> GetAllEntityByGoodsId(long goodsId)
        {
            var cacheKey = string.Format("{0}GetAllEntityByGoodsId:GoodsId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "GoodsId", goodsId), goodsId);
            List<GoodsInDetailImage> list = cacheService.Get<List<GoodsInDetailImage>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<GoodsInDetailImage>().Where("goodsid=@GoodsId", new { GoodsId = goodsId }).OrderBy(m => m.Number).ToList();
                }
                if (list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }
        /// <summary>
        /// 根据id获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsInDetailImage GetSingleEntity(long id)
        {
            return base.GetByCache(m => m.Id == id, id);
        }
        /// <summary>
        /// 添加实体并添加到缓存
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCacheByAddEntity(GoodsInDetailImage entity)
        {
            RealTimeCacheHelper.IncreaseAreaVersion("GoodsId", entity.Goodsid);
        }

        /// <summary>
        /// 更新实体缓存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool UpdateCacheByEntity(GoodsInDetailImage entity)
        {
            var result = base.UpdateByCache(entity);
            if (result)
            {
                RealTimeCacheHelper.IncreaseAreaVersion("GoodsId", entity.Goodsid);

            }
            return result;
        }
    }
}
