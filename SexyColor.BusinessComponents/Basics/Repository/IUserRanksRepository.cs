using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IUserRanksRepository : IRepository<UserRanks>
    {
        PagingDataSet<UserRanks> GetUserRanks(UserRanksQuery userRanksQuery, int pageIndex, int pageSize);
        UserRanks GetFullUserRanks(int rankId);
        bool UpdateCache(UserRanks userRanks);
    }
}
