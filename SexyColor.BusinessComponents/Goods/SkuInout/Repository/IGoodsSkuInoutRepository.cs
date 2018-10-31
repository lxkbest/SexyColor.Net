using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsSkuInoutRepository : IRepository<GoodsSkuInout>
    {
        PagingDataSet<GoodsSkuInout> GetGoodsSkuInout(GoodsSkuInoutQuery goodsSkuInoutQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 新增实体缓存
        /// </summary>
        /// <param name="entitys"></param>
        void InsertCacheByEntity(GoodsSkuInout entity);
    }
}
