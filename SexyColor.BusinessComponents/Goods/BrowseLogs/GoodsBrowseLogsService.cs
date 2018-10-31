using System;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class GoodsBrowseLogsService : IGoodsBrowseLogsService
    {
        public IGoodsBrowseLogsRepository goodsBrowseLogsRepository { get; set; }

        /// <summary>
        /// 获取浏览记录分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsBrowseLogs> GetGoodsBrowseLogs(long userId, int pageIndex, int pageSize)
        {
            return goodsBrowseLogsRepository.GetGoodsBrowseLogs(userId, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取商品浏览记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public long GetGoodsBrowseLogsCount(long userId)
        {
            return goodsBrowseLogsRepository.GetGoodsBrowseLogsCount(userId);
        }
    }
}
