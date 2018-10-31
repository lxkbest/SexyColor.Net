using System;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class UserRanksService : IUserRanksService
    {

        public IUserRanksRepository userRanksRepository { get; set; }

        /// <summary>
        /// 获取用户等级分页
        /// </summary>
        /// <param name="userRanksQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<UserRanks> GetUserRanks(UserRanksQuery userRanksQuery, int pageIndex, int pageSize)
        {
            return userRanksRepository.GetUserRanks(userRanksQuery, pageIndex, pageSize);
        }

        public UserRanks GetFullUserRanks(int rankId)
        {
            return userRanksRepository.GetFullUserRanks(rankId);
        }

        public bool EditUserRanks(UserRanks userRanks)
        {
            return userRanksRepository.UpdateCache(userRanks);
        }
    }
}
