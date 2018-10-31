using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class GoodsSkuInoutService : IGoodsSkuInoutService
    {
        public IGoodsSkuInoutRepository goodsSkuInoutRepository { get; set; }

        public PagingDataSet<GoodsSkuInout> GetGoodsSkuInout(GoodsSkuInoutQuery goodsSkuInoutQuery, int pageIndex, int pageSize)
        {
            return goodsSkuInoutRepository.GetGoodsSkuInout(goodsSkuInoutQuery, pageIndex, pageSize);
        }
    }
}
