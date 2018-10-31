using System.Collections.Generic;
using SexyColor.Infrastructure;
using MySqlSugar;
using System;

namespace SexyColor.BusinessComponents
{
    public class GoodsSkuInfoRepository : Repository<GoodsSkuInfo>, IGoodsSkuInfoRepository
    {
        /// <summary>
        /// 根据商品编号获取商品属性列表
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsSkuInfo> GetGoodsSkuInfoByGoodsId(long goodsId)
        {
            var cacheKey = string.Format("{0}GetGoodsSkuInfoByGoodsId:Goodsid-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Goodsid", goodsId), goodsId);
            List<GoodsSkuInfo> list = cacheService.Get<List<GoodsSkuInfo>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<GoodsSkuInfo>().Where(w => w.Goodsid == goodsId).ToList();
                }
                if(list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }

        /// <summary>
        /// 商品属性插入添加缓存
        /// </summary>
        /// <param name="entitys"></param>
        public void InsertCacheByEntitys(List<GoodsSkuInfo> entitys)
        {
            foreach (var entity in entitys)
            {
                OnInserted(entity);
            }
        }

        /// <summary>
        /// 修改商品属性状态
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ModifySkuStatus(GoodsSkuInfo skuEntity, GoodsSkuInfo checkEntity, GoodsInfo infoEntity, GoodsSkuInout inoutEntity)
        {
            if (skuEntity == null)
                return false;

            using (var db = DbService.GetInstance())
            {
                try
                {
                    db.BeginTran();
                    var result = db.Update<GoodsSkuInfo>(skuEntity);
                    if (checkEntity != null && checkEntity.Skuid > 0)
                    {
                        checkEntity.IsDefault = true;
                        db.Update<GoodsSkuInfo>(checkEntity);
                    }
                    if (result)
                    {
                        RealTimeCacheHelper.IncreaseAreaVersion("Goodsid", skuEntity.Goodsid);
                        db.Insert(inoutEntity, true);
                        db.Update<GoodsInfo>(new { stock = infoEntity.Stock }, w => w.Goodsid == infoEntity.Goodsid);

                        db.CommitTran();
                        return true;
                    }
                    db.RollbackTran();
                    return false;
                    
                }
                catch
                {
                    db.RollbackTran();
                    return false;
                }
            }



        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns></returns>
        public bool UpdateByEntity(GoodsSkuInfo entity)
        {
            var result = base.UpdateByCache(entity);
            return result;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="parameName"></param>
        /// <param name="parameValue"></param>
        public void UpdateCacheByParame(string parameName, long parameValue)
        {
            RealTimeCacheHelper.IncreaseAreaVersion(parameName, parameValue);
        }
    }
}
