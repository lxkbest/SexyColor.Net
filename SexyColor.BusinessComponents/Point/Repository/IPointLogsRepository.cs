
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IPointLogsRepository : IRepository<PointLogs>
    {
        PagingDataSet<PointLogs> GetPointLogs(PointLogsQuery pointLogsQuery, int pageIndex, int pageSize);
    }
}
