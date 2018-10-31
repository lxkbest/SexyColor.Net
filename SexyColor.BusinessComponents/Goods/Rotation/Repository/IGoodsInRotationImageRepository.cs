using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsInRotationImageRepository : IRepository<GoodsInRotationImage>
    {
        void InsertCacheByEntitys(List<GoodsInRotationImage> entitys);
        IEnumerable<GoodsInRotationImage> GetAllGoodsRotationImage(long goodsId);
        GoodsInRotationImage GetGoodsInRotationImage(long goodsId);
        bool UpdateCacheByEntity(GoodsInRotationImage rotationImg);
        void UpdateCacheByAddEntity(GoodsInRotationImage rotationImg);
        /// <summary>
        /// 删除实体根据缓存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteEntityByCache(GoodsInRotationImage entity);
    }
}
