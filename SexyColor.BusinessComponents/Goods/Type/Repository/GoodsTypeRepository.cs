using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class GoodsTypeRepository : Repository<GoodsType>, IGoodsTypeRepository
    {
        /// <summary>
        ///  获取商品类型实体
        /// </summary>
        /// <param name="goodsTypeId"></param>
        /// <returns></returns>
        public GoodsType GetEntityById(long goodsTypeId)
        {
            return base.GetByCache(m=>m.GoodsTypeId==goodsTypeId,goodsTypeId);
        }
    }
}
