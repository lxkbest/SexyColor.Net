﻿using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsBrowseLogsService
    {
        /// <summary>
        /// 获取浏览记录分页
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<GoodsBrowseLogs> GetGoodsBrowseLogs(long userId, int value, int pageSize);

        /// <summary>
        /// 获取商品浏览记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        long GetGoodsBrowseLogsCount(long userId);
    }
}
