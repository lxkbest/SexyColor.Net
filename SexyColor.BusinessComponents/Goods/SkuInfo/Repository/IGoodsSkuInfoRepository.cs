using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsSkuInfoRepository : IRepository<GoodsSkuInfo>
    {

        /// <summary>
        /// 根据商品编号获取商品属性列表
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        IEnumerable<GoodsSkuInfo> GetGoodsSkuInfoByGoodsId(long goodsId);

        /// <summary>
        /// 商品属性插入添加缓存
        /// </summary>
        /// <param name="entitys"></param>
        void InsertCacheByEntitys(List<GoodsSkuInfo> entitys);

        /// <summary>
        /// 修改商品属性状态
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool ModifySkuStatus(GoodsSkuInfo skuEntity, GoodsSkuInfo checkEntity, GoodsInfo infoEntity, GoodsSkuInout inoutEntity);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateByEntity(GoodsSkuInfo entity);

        /// <summary>
        /// 更新缓存参数
        /// </summary>
        /// <param name="parameName"></param>
        /// <param name="parameValue"></param>
        void UpdateCacheByParame(string parameName, long parameValue);
    }
}
