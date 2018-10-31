using SexyColor.Infrastructure;
using MySqlSugar;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class OperationLogsRepository : Repository<OperationLogs>, IOperationLogsRepository
    {
        /// <summary>
        /// 获取操作日志分页
        /// </summary>
        /// <param name="operationLogsQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OperationLogs> GetPageOperationLogs(OperationLogsQuery operationLogsQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleOrderByEunm(operationLogsQuery.OperationLogsSortBy.Value, out orderBy);
            object pars = new object();
            whereSql = HandleQueryBySqlable(operationLogsQuery, out pars);
            return GetPageListByCache<int>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Id);
        }

        /// <summary>
        /// 使用Sqlable处理SQL拼接（支持参数化）
        /// </summary>
        /// <param name="operationLogsQuery"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public string HandleQueryBySqlable(OperationLogsQuery operationLogsQuery, out object pars)
        {
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            var sqlTable = new Sqlable(); sqlTable.Sql = new StringBuilder();
            if (operationLogsQuery.ApplicationId.HasValue)
            {
                sqlTable = sqlTable.Where("applicationid = @ApplicationId");
                paramsDictionary.Add("ApplicationId", operationLogsQuery.ApplicationId.Value);
            }
            if (!string.IsNullOrWhiteSpace(operationLogsQuery.OperationName))
            {
                sqlTable = sqlTable.Where("operation_object_name LIKE @OperationName");
                paramsDictionary.Add("OperationName", $"%{operationLogsQuery.OperationName}%");
            }
            if (!string.IsNullOrWhiteSpace(operationLogsQuery.Username))
            {
                sqlTable = sqlTable.Where("operator_username LIKE @Username");
                paramsDictionary.Add("Username", $"%{operationLogsQuery.Username}%");
            }
            if (!string.IsNullOrWhiteSpace(operationLogsQuery.OperationType))
            {
                sqlTable = sqlTable.Where("operation_type = @OperationType");
                paramsDictionary.Add("OperationType", operationLogsQuery.OperationType);
            }
            if (operationLogsQuery.TimeLowerLimit.HasValue)
            {
                sqlTable = sqlTable.Where("date_created >= @TimeLowerLimit");
                paramsDictionary.Add("TimeLowerLimit", operationLogsQuery.TimeLowerLimit);
            }
            if (operationLogsQuery.TimeUpperLimit.HasValue)
            {
                sqlTable = sqlTable.Where("date_created <= @TimeUpperLimit");
                paramsDictionary.Add("TimeUpperLimit", operationLogsQuery.TimeUpperLimit);
            }
            pars = paramsDictionary;

            foreach (var item in sqlTable.Where)
            {
                sqlTable.Sql.Append(item);
            }
            return sqlTable.Sql.ToString().TrimStart(" AND".ToCharArray());
        }
        /// <summary>
        /// 使用枚举处理排序方式
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="order"></param>
        private void HandleOrderByEunm(OperationLogsSortBy orderBy, out string order)
        {
            order = string.Empty;
            switch (orderBy)
            {
                case OperationLogsSortBy.Id:
                    order = "id ASC";
                    break;
                case OperationLogsSortBy.Id_Desc:
                    order = "id DESC";
                    break;
                case OperationLogsSortBy.DateCreated:
                    order = "date_created ASC";
                    break;
                default:
                    order = "date_created DESC";
                    break;
            }
        }
    }
}
