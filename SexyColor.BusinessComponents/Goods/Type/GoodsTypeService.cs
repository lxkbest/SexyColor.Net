namespace SexyColor.BusinessComponents
{
    public class GoodsTypeService : IGoodsTypeService
    {
        public IGoodsTypeRepository goodsTypeRepository { get; set; }

        /// <summary>
        /// 获取商品类型实体
        /// </summary>
        /// <returns></returns>
        public GoodsType GetGoodsType(long goodsTypeId)
        {
            return goodsTypeRepository.GetEntityById(goodsTypeId);
        }
    }
}
