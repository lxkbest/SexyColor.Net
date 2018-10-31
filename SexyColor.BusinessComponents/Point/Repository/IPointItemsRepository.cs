using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IPointItemsRepository : IRepository<PointItems>
    {
        PagingDataSet<PointItems> GetPointItems(int pageIndex, int pageSize);
        PointItems GetFullPointType(int itemskey);
        bool UpdateCache(PointItems pointItems);
        bool AddPointItems(PointItems pointItems);
    }
}
