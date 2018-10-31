﻿using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsCollectionLogsService
    {
        /// <summary>
        /// 获取商品收藏记录分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<GoodsCollectionLogs> GetGoodsCollectionLogs(long userId, int value, int pageSize);

        /// <summary>
        /// 获取商品收藏记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        long GetGoodsCollectionLogsCount(long userId);
    }
}
