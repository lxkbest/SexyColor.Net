using SexyColor.Infrastructure;
using MySqlSugar;
using System.Text;
using System.Collections.Generic;
using System;

namespace SexyColor.BusinessComponents
{
    public class UserRanksRepository : Repository<UserRanks>, IUserRanksRepository
    {
        public UserRanks GetFullUserRanks(int rankId)
        {
            return base.GetByCache(m => m.Rank == rankId, rankId);
        }

        /// <summary>
        /// 获取用户等级分页
        /// </summary>
        /// <param name="userRanksQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<UserRanks> GetUserRanks(UserRanksQuery userRanksQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleOrderByEunm(userRanksQuery.UserRanksSortBy, out orderBy);
            object pars = new object();
            whereSql = HandleQueryBySqlable(userRanksQuery, out pars);
            return GetPageListByCache<int>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Rank);
        }

        public bool UpdateCache(UserRanks userRanks)
        {
            return base.UpdateByCache(userRanks);
        }

        /// <summary>
        /// 使用枚举处理排序方式
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="order"></param>
        private void HandleOrderByEunm(UserRanksSortBy? orderBy, out string order)
        {
            order = string.Empty;
            switch (orderBy)
            {
                case UserRanksSortBy.Rank:
                    order = "rank ASC";
                    break;
                case UserRanksSortBy.Rank_Desc:
                    order = "rank DESC";
                    break;
            }
        }

        /// <summary>
        /// 使用Sqlable处理SQL拼接（支持参数化）
        /// </summary>
        /// <param name="userRanksQuery"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        private string HandleQueryBySqlable(UserRanksQuery userRanksQuery, out object pars)
        {
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            var sqlTable = new Sqlable();
            sqlTable.Sql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(userRanksQuery.Rank))
            {
                sqlTable = sqlTable.Where("rank LIKE @Rank");
                paramsDictionary.Add("Rank", $"%{userRanksQuery.Rank}%");
            }
            if (!string.IsNullOrWhiteSpace(userRanksQuery.RankName))
            {
                sqlTable = sqlTable.Where("rank_name LIKE @RankName");
                paramsDictionary.Add("RankName", $"%{userRanksQuery.RankName}%");
            }
            if (!string.IsNullOrWhiteSpace(userRanksQuery.PointLower))
            {
                sqlTable = sqlTable.Where("point_lower = @PointLower");
                paramsDictionary.Add("PointLower", userRanksQuery.PointLower);
            }
           
            pars = paramsDictionary;

            foreach (var item in sqlTable.Where)
            {
                sqlTable.Sql.Append(item);
            }
            return sqlTable.Sql.ToString().TrimStart(" AND".ToCharArray());
        }

    }
}
