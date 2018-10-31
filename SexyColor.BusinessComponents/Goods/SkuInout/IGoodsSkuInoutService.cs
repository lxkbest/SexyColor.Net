using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsSkuInoutService
    {
        PagingDataSet<GoodsSkuInout> GetGoodsSkuInout(GoodsSkuInoutQuery goodsSkuInoutQuery, int pageIndex, int pageSize);
    }
}
