using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class PointLogsService : IPointLogsService
    {
        public IPointLogsRepository pointLogsRepository { get; set; }

        public PagingDataSet<PointLogs> GetPointLogs(PointLogsQuery pointLogsQuery, int pageIndex, int pageSize)
        {
            return pointLogsRepository.GetPointLogs(pointLogsQuery, pageIndex, pageSize);
        }
    }
}
