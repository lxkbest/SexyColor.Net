using SexyColor.Infrastructure;
using System.Collections.Generic;
using MySqlSugar;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class PointLogsRepository : Repository<PointLogs>, IPointLogsRepository
    {
        public PagingDataSet<PointLogs> GetPointLogs(PointLogsQuery pointLogsQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleOrderByEunm(pointLogsQuery.PointLogsSortBy, out orderBy);
            object pars = new object();
            whereSql = HandleQueryBySqlable(pointLogsQuery, out pars);
            return GetPageListByCache(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Id);
        }

        /// <summary>
        /// 使用Sqlable处理SQL拼接（支持参数化）
        /// </summary>
        /// <param name="pointLogsQuery"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        private string HandleQueryBySqlable(PointLogsQuery pointLogsQuery, out object pars)
        {
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            var sqlTable = new Sqlable();
            sqlTable.Sql = new StringBuilder();
            if (pointLogsQuery.IsIncome.HasValue)
            { 
                sqlTable = sqlTable.Where("is_income = @IsIncome");
                paramsDictionary.Add("IsIncome", pointLogsQuery.IsIncome);
            }
            if (!string.IsNullOrWhiteSpace(pointLogsQuery.UserName))
            {
                sqlTable = sqlTable.Where("userid in (select userid from sc_user where username LIKE @UserName)");
                paramsDictionary.Add("UserName", $"%{pointLogsQuery.UserName}%");
            }
            if (!string.IsNullOrWhiteSpace(pointLogsQuery.Itemsname))
            {
                sqlTable = sqlTable.Where("itemsname LIKE @Itemsname");
                paramsDictionary.Add("itemsname", $"%{pointLogsQuery.Itemsname}%");
            }
            if (pointLogsQuery.SexyPoints.HasValue)
            {
                sqlTable = sqlTable.Where("sexy_points = @SexyPoints");
                paramsDictionary.Add("SexyPoints", pointLogsQuery.SexyPoints);
            }
            if (pointLogsQuery.ExperiencePoints.HasValue)
            {
                sqlTable = sqlTable.Where("experience_points = @ExperiencePoints");
                paramsDictionary.Add("ExperiencePoints", pointLogsQuery.ExperiencePoints);
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
        private void HandleOrderByEunm(PointLogsSortBy? orderBy, out string order)
        {
            order = string.Empty;
            switch (orderBy)
            {
                case PointLogsSortBy.Id:
                    order = "id ASC";
                    break;
                case PointLogsSortBy.Id_Desc:
                    order = "id DESC";
                    break;
            }
        }
    }
}
