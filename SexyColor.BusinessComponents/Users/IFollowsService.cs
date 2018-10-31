using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IFollowsService
    {
        /// <summary>
        /// 获取粉丝id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<long> GetFollowedIds(long userId);
        /// <summary>
        /// 获取关注用户id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<long> GetFocusIds(long userId);
    }
}
