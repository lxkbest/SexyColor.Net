using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class GoodsSkuInoutRepository : Repository<GoodsSkuInout>, IGoodsSkuInoutRepository
    {
        public PagingDataSet<GoodsSkuInout> GetGoodsSkuInout(GoodsSkuInoutQuery goodsSkuInoutQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleOrderByEunm(goodsSkuInoutQuery.GoodsSkuInoutSortBy, out orderBy);
            object pars = new object();
            whereSql = HandleQueryBySqlable(goodsSkuInoutQuery, out pars);
            return GetPageListByCache(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Id);
        }

        private string HandleQueryBySqlable(GoodsSkuInoutQuery goodsSkuInoutQuery, out object pars)
        {
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            var sqlTable = new Sqlable();
            sqlTable.Sql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(goodsSkuInoutQuery.GoodsName))
            {
                sqlTable = sqlTable.Where("goodsid in (select goodsid from sc_goods_info where goods_name LIKE @GoodsName)");
                paramsDictionary.Add("GoodsName", $"%{goodsSkuInoutQuery.GoodsName}%");
            }
            if (!string.IsNullOrWhiteSpace(goodsSkuInoutQuery.SkuName))
            {
                sqlTable = sqlTable.Where("skuid in (select skuid from sc_goods_sku_info where sku_name LIKE @SkuName)");
                paramsDictionary.Add("SkuName", $"%{goodsSkuInoutQuery.SkuName}%");
            }
            if (goodsSkuInoutQuery.InoutNumberMin.HasValue)
            {
                sqlTable = sqlTable.Where("inout_number >= @InoutNumberMin");
                paramsDictionary.Add("InoutNumberMin", goodsSkuInoutQuery.InoutNumberMin);
            }
            if (goodsSkuInoutQuery.InoutNumberMax.HasValue)
            {
                sqlTable = sqlTable.Where("inout_number <= @InoutNumberMax");
                paramsDictionary.Add("InoutNumberMax", goodsSkuInoutQuery.InoutNumberMax);
            }
            if (goodsSkuInoutQuery.IsOut.HasValue)
            {
                sqlTable = sqlTable.Where("is_out = @IsOut");
                paramsDictionary.Add("IsOut", goodsSkuInoutQuery.IsOut);
            }

            pars = paramsDictionary;

            foreach (var item in sqlTable.Where)
            {
                sqlTable.Sql.Append(item);
            }
            return sqlTable.Sql.ToString().TrimStart(" AND".ToCharArray());
        }

        private void HandleOrderByEunm(GoodsSkuInoutSortBy? goodsSkuInoutSortBy, out string order)
        {
            order = string.Empty;
            switch (goodsSkuInoutSortBy)
            {
                case GoodsSkuInoutSortBy.DateCreated:
                    order = "date_created ASC";
                    break;
                case GoodsSkuInoutSortBy.DateCreated_Desc:
                    order = "date_created DESC";
                    break;
            }
        }

        /// <summary>
        /// 新增实体缓存
        /// </summary>
        /// <param name="entity"></param>
        public void InsertCacheByEntity(GoodsSkuInout entity)
        {
            OnInserted(entity);
        }
    }
}
