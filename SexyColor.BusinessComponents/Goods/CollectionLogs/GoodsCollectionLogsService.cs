using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class GoodsCollectionLogsService : IGoodsCollectionLogsService
    {
        public IGoodsCollectionLogsRepository goodsCollectionLogsRepository { get; set; }

        /// <summary>
        /// 获取商品收藏记录分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsCollectionLogs> GetGoodsCollectionLogs(long userId, int pageIndex, int pageSize)
        {
            return goodsCollectionLogsRepository.GetGoodsCollectionLogs(userId, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取商品收藏记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public long GetGoodsCollectionLogsCount(long userId)
        {
            return goodsCollectionLogsRepository.GetGoodsCollectionLogsCount(userId);
        }
    }
}
