using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsTypeRepository : IRepository<GoodsType>
    {
        /// <summary>
        /// 获取商品类型实体
        /// </summary>
        /// <returns></returns>
        GoodsType GetEntityById(long goodsTypeId);
    }
}
