using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IPointItemsService
    {
        PagingDataSet<PointItems> GetPointItems(int value, int pageSize);
        PointItems GetFullPointItems(int itemskey);
        bool EditPointItems(PointItems pointItems);
        bool AddPointItems(PointItems pointItems);
    }
}
