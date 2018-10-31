using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IPointLogsService
    {
        PagingDataSet<PointLogs> GetPointLogs(PointLogsQuery pointLogsQuery, int value, int pageSize);
    }
}
