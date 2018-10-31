using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IUserRanksService
    {
        PagingDataSet<UserRanks> GetUserRanks(UserRanksQuery userRanksQuery, int pageIndex, int pageSize);

        UserRanks GetFullUserRanks(int rankId);

        bool EditUserRanks(UserRanks userRanks);
    }
}
