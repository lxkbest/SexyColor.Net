using System.Collections.Generic;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public interface IFollowsRepository : IRepository<Follows>
    {
        IEnumerable<long> GetFollowedIds(long userId);
        IEnumerable<long> GetFocusIds(long userId);
    }
}
