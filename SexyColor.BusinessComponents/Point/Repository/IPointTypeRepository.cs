using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IPointTypeRepository : IRepository<PointType>
    {
        PagingDataSet<PointType> GetPointType(int pageIndex, int pageSize);
        PointType GetFullPointType(string typekey);
        bool UpdateCache(PointType pointType);
        bool AddPointType(PointType pointType);
    }
}
