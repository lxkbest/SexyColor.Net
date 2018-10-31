using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IPointTypeService
    {
        PagingDataSet<PointType> GetPointType(int pageIndex, int pageSize);
        PointType GetFullPointType(string typekey);
        bool EditPointType(PointType pointType);
        bool AddPointType(PointType pointType);
        IEnumerable<PointType> GetPointTypeList();
    }
}
