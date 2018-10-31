using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsSkuInfoService
    {
        GoodsSkuInfo GetGoodsSkuInfo(long skuid);

        /// <summary>
        /// 根据商品编号获取商品属性列表
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        IEnumerable<GoodsSkuInfo> GetGoodsSkuInfoByGoodsId(long goodsId);

        /// <summary>
        /// 修改商品属性状态
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool ModifySkuStatus(int skuId, int checkSkuId, long userId, GoodsSkuInfoStatus status);
    }
}
