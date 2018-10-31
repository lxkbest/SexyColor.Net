using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsInfoService
    {
        /// <summary>
        /// 后台商品信息分页
        /// </summary>
        /// <param name="goodsInfoQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<GoodsInfo> GetPageGoodsInfo(GoodsInfoQuery goodsInfoQuery, int pageIndex, int pageSize);

        /// <summary>
        /// API商品信息分页数据
        /// </summary>
        /// <param name="parsms"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<GoodsInfo> GetPageGoodsInfoAPI(Dictionary<string, object> parsms, string orderBy, int pageIndex, int pageSize);

        /// <summary>
        /// 批量回收商品
        /// </summary>
        /// <param name="goodsIds"></param>
        void RecycleGoods(IEnumerable<long> goodsIds);

        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <param name="goodsId"></param>
        void RecoveryGoods(long goodsId);

        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        GoodsInfo GetGoodsInfo(long goodsId);

        /// <summary>
        /// 上下架商品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        GoodsInfo UpDownShelvesGoods(GoodsInfo entity, bool isUpDown);

        /// <summary>
        /// 后台发布商品
        /// </summary>
        /// <param name="infoEntity"></param>
        /// <param name="skuEntitys"></param>
        /// <param name="imgEntitys"></param>
        /// <returns></returns>
        bool ReleaseGoods(GoodsInfo infoEntity, List<GoodsSkuInfo> skuEntitys, List<GoodsInRotationImage> imgEntitys, string[] detailsImgArr, long userId);

        /// <summary>
        /// 设置商品基本信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        void SetGoodsInfo(GoodsInfo entity, out SetGoodsInfoStatus status);

        /// <summary>
        /// 设置商品属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="skuEntitys"></param>
        bool SetGoodsSkuInfo(GoodsInfo entity, List<GoodsSkuInfo> skuEntitys, long userId);

        /// <summary>
        /// 获取top条数的热门商品
        /// </summary>
        /// <param name="top">需要几条</param>
        /// <returns></returns>
        IEnumerable<GoodsInfo> GetGoodsTopNumberByHot(int top);
    }
}
