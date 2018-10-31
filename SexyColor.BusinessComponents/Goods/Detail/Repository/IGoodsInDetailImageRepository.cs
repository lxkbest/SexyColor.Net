using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsInDetailImageRepository : IRepository<GoodsInDetailImage>
    {
        /// <summary>
        /// 根据goodid获取所有实体
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        IEnumerable<GoodsInDetailImage> GetAllEntityByGoodsId(long goodsId);
       
        /// <summary>
        /// 更新实体缓存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool UpdateCacheByEntity(GoodsInDetailImage entity);
        /// <summary>
        /// 添加实体并添加到缓存
        /// </summary>
        /// <param name="entity"></param>
        void UpdateCacheByAddEntity(GoodsInDetailImage entity);

        /// <summary>
        /// 根据id获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GoodsInDetailImage GetSingleEntity(long id);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteEntityByCache(GoodsInDetailImage entity);
    }
}
