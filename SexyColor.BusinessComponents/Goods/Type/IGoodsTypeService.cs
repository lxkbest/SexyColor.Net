namespace SexyColor.BusinessComponents
{
    public interface IGoodsTypeService
    {
        /// <summary>
        /// 获取商品类型实体
        /// </summary>
        /// <returns></returns>
         GoodsType GetGoodsType(long goodsTypeId);
    }
}
