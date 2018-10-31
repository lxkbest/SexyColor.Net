using System.Collections.Generic;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    public class GoodsInRotationImageRepository : Repository<GoodsInRotationImage>, IGoodsInRotationImageRepository
    {
        /// <summary>
        /// 删除实体根据缓存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntityByCache(GoodsInRotationImage entity)
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
        /// 根据id获取轮换图实体集合
        /// </summary>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        public IEnumerable<GoodsInRotationImage> GetAllGoodsRotationImage(long goodsid)
        {
            var cacheKey = string.Format("{0}GetAllGoodsRotationImage:GoodsId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "GoodsId", goodsid), goodsid);
            List<GoodsInRotationImage> lists = cacheService.Get<List<GoodsInRotationImage>>(cacheKey);
            if (lists == null)
            {
                using (var db = DbService.GetInstance())
                {
                    lists = db.Queryable<GoodsInRotationImage>().Where("goodsid=@GoodsId", new { GoodsId = goodsid }).OrderBy(m=>m.Number).ToList();
                }
                if(lists != null)
                    cacheService.Add(cacheKey, lists, CachingExpirationType.UsualObjectCollection);
            }
            return lists;
        }
        /// <summary>
        /// 根据id获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsInRotationImage GetGoodsInRotationImage(long id)
        {
            return base.GetByCache(m => m.Id == id, id);
        }

        public void InsertCacheByEntitys(List<GoodsInRotationImage> entitys)
        {
            foreach (var entity in entitys)
            {
                OnInserted(entity);
            }
        }
        /// <summary>
        /// 更新添加实体缓存
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCacheByAddEntity(GoodsInRotationImage entity)
        {
            RealTimeCacheHelper.IncreaseAreaVersion("GoodsId", entity.Goodsid);
        }

        /// <summary>
        /// 更新实体缓存
        /// </summary>
        /// <param name="rotationImg"></param>
        /// <returns></returns>
        public bool UpdateCacheByEntity(GoodsInRotationImage rotationImg)
        {
            var result = base.UpdateByCache(rotationImg);
            if (result)
            {
                RealTimeCacheHelper.IncreaseAreaVersion("GoodsId", rotationImg.Goodsid);

            }
            return result;
            
        }
    }
}
