using System;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using MySqlSugar;
using System.Text;
using System.Linq.Expressions;

namespace SexyColor.BusinessComponents
{
    public class GoodsInfoRepository : Repository<GoodsInfo>, IGoodsInfoRepository
    {

        /// <summary>
        /// 后台商品信息分页
        /// </summary>
        /// <param name="goodsInfoQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsInfo> GetPageGoodsInfo(GoodsInfoQuery goodsInfoQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleOrderByEunm(goodsInfoQuery.GoodsInfoSortBy, out orderBy);
            object pars = new object();
            whereSql = HandleQueryBySqlable(goodsInfoQuery, out pars);
            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Goodsid);
        }

        /// <summary>
        /// WEBAPI分页
        /// </summary>
        /// <param name="parsms"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsInfo> GetPageGoodsInfoAPI(Dictionary<string, object> parsms, string orderBy, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            object pars = new object();
            whereSql = HandleQueryBySqlableAPI(parsms, out pars);
            //return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Goodsid);
            return ExcePageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, pars, orderBy, i => i.Goodsid);
        }

        public PagingDataSet<GoodsInfo> ExcePageListByCache<U>(int pageIndex, int pageSize, out int totalCount, out int totalPage, string whereSql, object whereObj, string

orderBy, Expression<Func<GoodsInfo, U>> primaryKey)
        {

            using (var db = DbService.GetInstance())
            {
                Queryable<GoodsInfo> temp = null;
                if (!string.IsNullOrWhiteSpace(whereSql))
                    temp = db.Queryable<GoodsInfo>()
                        .JoinTable<GoodsInfoInChara>((s1, s2) => s1.Goodsid == s2.Goodsid)
                        .Where(whereSql, whereObj);
                else
                    temp = db.Queryable<GoodsInfo>();
                var list = temp.ToList();
                totalCount = temp.Count();
                totalPage = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                temp = temp.OrderBy(orderBy);
                IEnumerable<U> enumerable = temp.Skip<GoodsInfo>(pageSize * (pageIndex - 1)).Take<GoodsInfo>(pageSize).Select<U>("s1.goodsid").ToList();
                return new PagingDataSet<GoodsInfo>(CacheByEntityIds<U>(enumerable)) { PageIndex = pageIndex, PageSize = pageSize, TotalRecords = totalCount };

            }

        }

        private string HandleQueryBySqlableAPI(Dictionary<string, object> parsms, out object pars)
        {
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            var sqlTable = new Sqlable();
            sqlTable.Sql = new StringBuilder();

            sqlTable = sqlTable.Where("status = @Status");
            paramsDictionary.Add("Status", (int)GoodsInfoStatus.Sale);

            if (parsms.ContainsKey("CategoryItemid"))
            {
                sqlTable = sqlTable.Where("categoryitemid = @CategoryItemid");
                paramsDictionary.Add("CategoryItemid", Convert.ToInt32(parsms["CategoryItemid"]));
            }
            if (parsms.ContainsKey("Brandsid"))
            {
                sqlTable = sqlTable.Where("brandsid = @Brandsid");
                paramsDictionary.Add("Brandsid", Convert.ToInt32(parsms["Brandsid"]));
            }
            if (parsms.ContainsKey("Charaid"))
            {
                sqlTable = sqlTable.Where("s2.charaid = @Charaid");
                paramsDictionary.Add("Charaid", Convert.ToInt32(parsms["Charaid"]));
            }
            if (parsms.ContainsKey("Max") && parsms.ContainsKey("Min"))
            {
                sqlTable = sqlTable.Where("goods_real_price between @Min and @Max");
                paramsDictionary.Add("Max", Convert.ToInt32(parsms["Max"]));
                paramsDictionary.Add("Min", Convert.ToInt32(parsms["Min"]));
            }

            pars = paramsDictionary;

            foreach (var item in sqlTable.Where)
            {
                sqlTable.Sql.Append(item);
            }
            return sqlTable.Sql.ToString().TrimStart(" AND".ToCharArray());
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateCache(GoodsInfo entity)
        {
            return base.UpdateByCache(entity);
        }

        /// <summary>
        /// 按照更新参数更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="obj"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public bool UpdateCache(GoodsInfo entity, object obj, Expression<Func<GoodsInfo, bool>> lambda)
        {
            var result = base.UpdateByCache(entity, obj, lambda);
            return result;
        }

        /// <summary>
        /// 处理排序
        /// </summary>
        /// <param name="value"></param>
        /// <param name="orderBy"></param>
        private void HandleOrderByEunm(GoodsInfoSortBy value, out string orderBy)
        {
            orderBy = string.Empty;
            switch (value)
            {
                case GoodsInfoSortBy.Keynum_ASC:
                    orderBy = "goodsid ASC";
                    break;
                case GoodsInfoSortBy.Keynum_DESC:
                    orderBy = "goodsid DESC";
                    break;
                case GoodsInfoSortBy.Price_ASC:
                    orderBy = "goods_real_price ASC";
                    break;
                case GoodsInfoSortBy.Price_DESC:
                    orderBy = "goods_real_price DESC";
                    break;
                case GoodsInfoSortBy.BuyCount_ASC:
                    orderBy = "buy_count ASC";
                    break;
                case GoodsInfoSortBy.BuyCount_DESC:
                    orderBy = "buy_count DESC";
                    break;
                case GoodsInfoSortBy.DateCreated_ASC:
                    orderBy = "date_created ASC";
                    break;
                case GoodsInfoSortBy.DateCreated_DESC:
                    orderBy = "date_created DESC";
                    break;
                case GoodsInfoSortBy.DisplayOrder_ASC:
                    orderBy = "display_order ASC";
                    break;
                case GoodsInfoSortBy.DisplayOrder_DESC:
                    orderBy = "display_order DESC";
                    break;
            }
        }

        /// <summary>
        /// 处理SQL
        /// </summary>
        /// <param name="goodsInfoQuery"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        private string HandleQueryBySqlable(GoodsInfoQuery goodsInfoQuery, out object pars)
        {
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            var sqlTable = new Sqlable();
            sqlTable.Sql = new StringBuilder();
 
            sqlTable = sqlTable.Where("status = @Status");
            paramsDictionary.Add("Status", (int)goodsInfoQuery.GoodsInfoStatus);

            if (!string.IsNullOrWhiteSpace(goodsInfoQuery.Keynum))
            {
                sqlTable = sqlTable.Where("goodsid = @Goodsid");
                paramsDictionary.Add("Goodsid", long.Parse(goodsInfoQuery.Keynum));
            }
            if (!string.IsNullOrWhiteSpace(goodsInfoQuery.Keyword))
            {
                sqlTable = sqlTable.Where("goods_name LIKE @GoodsName");
                paramsDictionary.Add("GoodsName", $"%{goodsInfoQuery.Keyword}%");
            }
 
            if (goodsInfoQuery.GoodsPriceLowerLimit.HasValue)
            {
                sqlTable = sqlTable.Where("goods_price >= @GoodsPriceLowerLimit");
                paramsDictionary.Add("GoodsPriceLowerLimit", goodsInfoQuery.GoodsPriceLowerLimit);
            }
            if (goodsInfoQuery.GoodsPriceUpperLimit.HasValue)
            {
                sqlTable = sqlTable.Where("goods_price <= @GoodsPriceUpperLimit");
                paramsDictionary.Add("GoodsPriceUpperLimit", goodsInfoQuery.GoodsPriceUpperLimit);
            }

            if (goodsInfoQuery.BuyCountLowerLimit.HasValue)
            {
                sqlTable = sqlTable.Where("goods_price >= @GoodsPriceLowerLimit");
                paramsDictionary.Add("BuyCountLowerLimit", goodsInfoQuery.BuyCountLowerLimit);
            }
            if (goodsInfoQuery.BuyCountUpperLimit.HasValue)
            {
                sqlTable = sqlTable.Where("goods_price <= @GoodsPriceUpperLimit");
                paramsDictionary.Add("BuyCountUpperLimit", goodsInfoQuery.BuyCountUpperLimit);
            }

            pars = paramsDictionary;

            foreach (var item in sqlTable.Where)
            {
                sqlTable.Sql.Append(item);
            }
            return sqlTable.Sql.ToString().TrimStart(" AND".ToCharArray());
        }

        /// <summary>
        /// 更新实体缓存
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCacheByEntity(GoodsInfo entity)
        {
            OnUpdated(entity);
        }

        /// <summary>
        /// 更新实体缓存（列表）
        /// </summary>
        /// <param name="entitys"></param>
        public void UpdateCacheByEntitys(List<GoodsInfo> entitys)
        {
            foreach (var entity in entitys)
            {
                OnInserted(entity);
            }
        }

        /// <summary>
        /// 发布商品
        /// </summary>
        /// <param name="infoEntity"></param>
        /// <param name="imgEntitys"></param>
        /// <returns></returns>
        public bool ReleaseGoods(GoodsInfo infoEntity, List<GoodsInRotationImage> imgEntitys, string[] detailsImgArr)
        {
            List<GoodsInDetailImage> detailEntitys = new List<GoodsInDetailImage>();
            if (detailsImgArr != null && detailsImgArr.Length > 0)
            {
                var index = 0;
                foreach (string detailImg in detailsImgArr)
                {
                    GoodsInDetailImage detailEntity = new GoodsInDetailImage();
                    detailEntity.Goodsid = infoEntity.Goodsid;
                    detailEntity.GoodsDetaiImage = detailImg;
                    detailEntity.Number = index + 1;
                    detailEntitys.Add(detailEntity);
                    index++;
                }
            }

            foreach (var img in imgEntitys)
            {
                img.Goodsid = infoEntity.Goodsid;
            }

            using (var db = DbService.GetInstance())
            {
                try
                {
                    db.BeginTran();
                    object objId = db.Insert(infoEntity, false);
                    var result1 = db.SqlBulkCopy(imgEntitys);
                    var result2 = true;
                    if (detailEntitys.Count > 0)
                        result2 = db.SqlBulkCopy(detailEntitys);
                    if (objId != null && result1 && result2)
                    {
                        OnInserted(infoEntity);
                        db.CommitTran();
                        return true;
                    }
                    else
                    {
                        db.RollbackTran();
                        return false;
                    }
                }
                catch
                {
                    db.RollbackTran();
                    return false;
                }
                
            }
        }

        /// <summary>
        /// 获取top条数的热门商品或不热门商品
        /// </summary>
        /// <param name="top"></param>
        /// <param name="isHot"></param>
        /// <returns></returns>
        public IEnumerable<GoodsInfo> GetGoodsTopNumberByHot(int top, bool isHot)
        {
            var cacheKey = string.Format("{0}GetGoodsTopNumberByHot:IsHot-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "IsHot", isHot), isHot);
            List<GoodsInfo> list = cacheService.Get<List<GoodsInfo>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<GoodsInfo>().Where(w => w.IsHot == isHot).OrderBy(w => w.Goodsid).Take(top).ToList();
                }
                if(list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }

       
    }
}
