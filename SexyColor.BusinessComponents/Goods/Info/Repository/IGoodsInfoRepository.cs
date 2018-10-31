using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsInfoRepository : IRepository<GoodsInfo>
    {
        /// <summary>
        /// 后台分页
        /// </summary>
        /// <param name="goodsInfoQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<GoodsInfo> GetPageGoodsInfo(GoodsInfoQuery goodsInfoQuery, int pageIndex, int pageSize);

        /// <summary>
        /// WEBAPI分页
        /// </summary>
        /// <param name="parsms"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<GoodsInfo> GetPageGoodsInfoAPI(Dictionary<string, object> parsms, string orderBy, int pageIndex, int pageSize);

        /// <summary>
        /// 更新实体（带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool UpdateCache(GoodsInfo entity);

        /// <summary>
        /// 按照更新参数更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="obj"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        bool UpdateCache(GoodsInfo entity, object obj, Expression<Func<GoodsInfo, bool>> lambda);

        /// <summary>
        /// 发布商品
        /// </summary>
        /// <param name="infoEntity"></param>
        /// <param name="imgEntitys"></param>
        /// <returns></returns>
        bool ReleaseGoods(GoodsInfo infoEntity, List<GoodsInRotationImage> imgEntitys, string[] detailsImgArr);

        /// <summary>
        /// 更新实体缓存
        /// </summary>
        /// <param name="entity"></param>
        void UpdateCacheByEntity(GoodsInfo entity);

        /// <summary>
        /// 更新实体缓存（列表）
        /// </summary>
        /// <param name="entitys"></param>
        void UpdateCacheByEntitys(List<GoodsInfo> entitys);

        /// <summary>
        /// 获取top条数的热门商品或不热门商品 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="isHot"></param>
        /// <returns></returns>
        IEnumerable<GoodsInfo> GetGoodsTopNumberByHot(int top, bool isHot);
    }
}
