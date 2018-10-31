using System;
using System.Linq;
using System.Collections.Generic;
using MySqlSugar;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;

namespace SexyColor.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public ICacheService cacheService;
        protected static RealTimeCacheHelper RealTimeCacheHelper {
            get { return EntityData.ForType(typeof(T)).RealTimeCacheHelper; }
        }
        public Repository()
        {
            cacheService = DIContainer.Resolve<ICacheService>();
        }

        #region 基础方法

        public T Add(T entity, bool isIdentity = true)
        {
            using (var db = DbService.GetInstance())
            {
                return db.Insert<T>(entity, isIdentity) as T;
            }
        }

        public void AddEntity(List<T> entitys, bool isIdentity = true)
        {
            using (var db = DbService.GetInstance())
            {
                try
                {
                    db.BeginTran();
                    db.SqlBulkCopy<T>(entitys);
                }
                catch (Exception ex)
                {
                    db.RollbackTran();
                    throw ex;
                }
            }
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            using (var db = DbService.GetInstance())
            {
                return db.Queryable<T>().Where(predicate).Count();
            }
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            using (var db = DbService.GetInstance())
            {
                var result = db.Delete<T>(predicate);
                if (result)
                {

                }
                return result;
            }
        }

        public bool Delete(List<T> entitys)
        {
            using (var db = DbService.GetInstance())
            {
                try
                {
                    db.BeginTran();
                    return db.Delete<T>(entitys);
                }
                catch (Exception ex)
                {
                    db.RollbackTran();
                    throw ex;
                }
            }
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            using (var db = DbService.GetInstance())
            {
                return db.Queryable<T>().Any(anyLambda);
            }
        }

        public string GetJson(string strSql)
        {
            using (var db = DbService.GetInstance())
            {
                return db.SqlQueryJson(strSql);
            }
        }

        public Queryable<T> GetList(Expression<Func<T, bool>> whereLamdba)
        {
            using (var db = DbService.GetInstance())
            {
                return db.Queryable<T>().Where(whereLamdba);
            }
        }

        public dynamic GetList(string strSql)
        {
            using (var db = DbService.GetInstance())
            {
                return db.SqlQueryDynamic(strSql);
            }
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <param name="predicate">拉姆达表达式</param>
        /// <returns></returns>
        public T GetModel(Expression<Func<T, bool>> predicate)
        {
            using (var db = DbService.GetInstance())
            {
                return db.Queryable<T>().FirstOrDefault(predicate);
            }
        }
        /// <summary>
        /// 查询一组Id数据
        /// </summary>
        /// <param name="entityIds">Id编号</param>
        /// <returns></returns>
        public List<T> GetModelByIds<U>(Expression<Func<T, U>> predicate, IEnumerable<U> entityIds)
        {
            using (var db = DbService.GetInstance())
            {
                return db.Queryable<T>().In(predicate, entityIds).ToList();
            }
        }

        public Queryable<T> GetPageList(int pageIndex, int pageSize, out int rows, out int totalPage, Expression<Func<T, bool>>

whereLambda, bool isAsc, Expression<Func<T, object>> orderBy)
        {
            using (var db = DbService.GetInstance())
            {
                var temp = db.Queryable<T>().Where(whereLambda);
                rows = temp.Count();
                totalPage = rows % pageSize == 0 ? rows / pageSize : rows / pageSize + 1;
                temp = isAsc ? temp.OrderBy(orderBy) : temp.OrderBy(orderBy, OrderByType.desc);
                return temp.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
            }
        }

        public Queryable<T> GetPageList(int pageIndex, int pageSize, out int rows, out int totalPage, Expression<Func<T, bool>>

whereLambda, string orderBy)
        {
            using (var db = DbService.GetInstance())
            {
                var temp = db.Queryable<T>().Where(whereLambda);
                rows = temp.Count();
                totalPage = rows % pageSize == 0 ? rows / pageSize : rows / pageSize + 1;
                temp = temp.OrderBy(orderBy);
                return temp.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
            }
        }

        public Queryable<T> GetPageList(int pageIndex, int pageSize, out int rows, out int totalPage, string whereLambda, string

orderBy)
        {
            using (var db = DbService.GetInstance())
            {
                var temp = db.Queryable<T>().Where(whereLambda);
                rows = temp.Count();
                totalPage = rows % pageSize == 0 ? rows / pageSize : rows / pageSize + 1;
                temp = temp.OrderBy(orderBy);
                return temp.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
            }
        }

        public List<T> GetSqlList(string sql, object whereObj = null)
        {
            using (var db = DbService.GetInstance())
            {
                return db.SqlQuery<T>(sql, whereObj);
            }
        }

        public List<T> GetSqlList(string sql)
        {
            using (var db = DbService.GetInstance())
            {
                return db.SqlQuery<T>(sql);
            }
        }


        public bool Update(T entity)
        {
            using (var db = DbService.GetInstance())
            {
                return db.Update(entity);
            }
        }

        public bool Update(object model, Expression<Func<T, bool>> expression)
        {
            using (var db = DbService.GetInstance())
            {
                try
                {
                    db.BeginTran();
                    return db.Update<T>(model, expression);
                }
                catch (Exception ex)
                {
                    db.RollbackTran();
                    throw ex;
                }
            }
        }

        public bool Update(List<T> entitys)
        {
            using (var db = DbService.GetInstance())
            {
                try
                {
                    db.BeginTran();
                    return db.SqlBulkReplace(entitys);
                }
                catch (Exception ex)
                {
                    db.RollbackTran();
                    throw ex;
                }
            }
        }

        #endregion

        #region 新添加基础方法
        public int GetInt(string sql, object pars)
        {
            using (var db = DbService.GetInstance())
            {
                return db.GetInt(sql, pars);
            }
        }
        #endregion


        #region 实体带缓存

        public virtual T GetByCache(Expression<Func<T, bool>> expression, object entityId)
        {
            T local = default(T);
            if (Repository<T>.RealTimeCacheHelper.EnableCache)
            {
                local = cacheService.Get<T>(Repository<T>.RealTimeCacheHelper.GetCacheKeyOfEntity(entityId));
            }
            if (local == null)
            {
                using (var db = DbService.GetInstance())
                {
                    local = DbService.GetInstance().Queryable<T>().FirstOrDefault(expression);
                }
                if (Repository<T>.RealTimeCacheHelper.EnableCache && local != null)
                {
                    cacheService.Add(Repository<T>.RealTimeCacheHelper.GetCacheKeyOfEntity(local.EntityId), local, Repository<T>.RealTimeCacheHelper.CachingExpirationType);
                }
            }
            if ((local != null))
            {
                return local;
            }
            return default(T);
        }

        public IEnumerable<T> GetAllByCache<U>(Expression<Func<T, object>> expression, Expression<Func<T, U>> primaryKey)
        {
            return GetAllByCache<U>(expression, primaryKey, OrderByType.asc);
        }

        public IEnumerable<T> GetAllByCache<U>(Expression<Func<T, object>> expression, Expression<Func<T, U>> primaryKey, OrderByType orderType)
        {
            IEnumerable<U> enumerable = null;
            string cacheKey = null;
            string orderBy = orderType.ToString();
            if (Repository<T>.RealTimeCacheHelper.EnableCache)
            {
                cacheKey = Repository<T>.RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.GlobalVersion);
                if (!string.IsNullOrEmpty(orderBy))
                {
                    cacheKey = cacheKey + "SB-" + orderBy;
                }
                enumerable = this.cacheService.Get<IEnumerable<U>>(cacheKey);
            }
            if (enumerable == null)
            {
                using (var db = DbService.GetInstance())
                {
                    enumerable = db.Queryable<T>().OrderBy(expression, orderType).Select(primaryKey).ToList();
                }
                if (Repository<T>.RealTimeCacheHelper.EnableCache)
                {
                    this.cacheService.Add(cacheKey, enumerable, Repository<T>.RealTimeCacheHelper.CachingExpirationType);
                }
            }
            return this.CacheByEntityIds<U>(enumerable);
        }

        /// <summary>
        /// 查询全部（带缓存）
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="expression"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllByCache<U>(string expression, Expression<Func<T, U>> primaryKey)
        {
            IEnumerable<U> enumerable = null;
            string cacheKey = null;
            string orderBy = expression;
            if (Repository<T>.RealTimeCacheHelper.EnableCache)
            {
                cacheKey = Repository<T>.RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.GlobalVersion);
                if (!string.IsNullOrEmpty(orderBy))
                {
                    cacheKey = cacheKey + "SB-" + orderBy;
                }
                enumerable = this.cacheService.Get<IEnumerable<U>>(cacheKey);
            }
            if (enumerable == null)
            {
                using (var db = DbService.GetInstance())
                {
                    enumerable = db.Queryable<T>().Select(primaryKey).OrderBy(expression).ToList();
                }
                if (Repository<T>.RealTimeCacheHelper.EnableCache)
                {
                    this.cacheService.Add(cacheKey, enumerable, Repository<T>.RealTimeCacheHelper.CachingExpirationType);
                }
            }
            return this.CacheByEntityIds<U>(enumerable);
        }

        public virtual IEnumerable<T> CacheByEntityIds<U>(IEnumerable<U> entityIds)
        {
            T[] localArray = new T[entityIds.Count<U>()];
            Dictionary<object, int> dictionary = new Dictionary<object, int>();
            for (int i = 0; i < entityIds.Count<U>(); i++)
            {
                T local = this.cacheService.Get<T>(Repository<T>.RealTimeCacheHelper.GetCacheKeyOfEntity(entityIds.ElementAt<U>(i)));
                if (local != null)
                {
                    localArray[i] = local;
                }
                else
                {
                    localArray[i] = default(T);
                    dictionary[entityIds.ElementAt<U>(i)] = i;
                }
            }
            if (dictionary.Count > 0)
            {
                using (var db = DbService.GetInstance())
                {
                    foreach (T local2 in db.Queryable<T>().In(dictionary.Keys.ToArray()).ToList())
                    {
                        localArray[dictionary[local2.EntityId]] = local2;
                        if (Repository<T>.RealTimeCacheHelper.EnableCache && (local2 != null))
                            this.cacheService.Set(Repository<T>.RealTimeCacheHelper.GetCacheKeyOfEntity(local2.EntityId), local2, Repository<T>.RealTimeCacheHelper.CachingExpirationType);
                    }
                }
            }
            List<T> list = new List<T>();
            foreach (T local3 in localArray)
            {
                if (local3 != null)
                {
                    list.Add(local3);
                }
            }
            return list;
        }
        /// <summary>
        /// 添加实体到缓存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isIdentity">是否是自增长</param>
        /// <returns>如果是自增长返回bool，否则返回插入成功后的id</returns>
        public virtual object AddByCache(T entity, bool isIdentity = true)
        {
            using (var db = DbService.GetInstance())
            {
                var result = db.Insert<T>(entity, isIdentity);
                OnInserted(entity);
                return result;
            }
        }

        /// <summary>
        /// 大数据插入
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual bool AddByCache(List<T> entitys)
        {
            using (var db = DbService.GetInstance())
            {
                try
                {
                    db.BeginTran();
                    var resutl = db.SqlBulkCopy(entitys);
                    if (resutl)
                    {
                        foreach (T entity in entitys)
                        {
                            OnInserted(entity);
                        }
                        db.CommitTran();
                    }
                    else
                    {
                        db.RollbackTran();
                    }
                    return resutl;
                }
                catch(Exception ex)
                {
                    db.RollbackTran();
                    return false;
                }

            }
        }



        public virtual bool UpdateByCache(T entity)
        {
            bool result = false;
            using (var db = DbService.GetInstance())
            {
                result = db.Update<T>(entity);
            }
            if (result)
            {
                OnUpdated(entity);
            }
            return result;
        }

        public virtual bool UpdateByCache(T entity, object rowObj, Expression<Func<T, bool>> whereLambda)
        {
            bool result = false;
            using (var db = DbService.GetInstance())
            {
                result = db.Update<T>(rowObj, whereLambda);
            }
            if (result)
            {
                OnUpdated(entity);
            }
            return result;
        }


        public virtual bool DeleteByCache(T entity)
        {
            bool result = false;
            using (var db = DbService.GetInstance())
            {
                result = db.Delete<T>(entity);
            }
            if (result)
            {
                OnDeleted(entity);
            }
            return result;
        }

        public virtual PagingDataSet<T> GetPageListByCache(int pageIndex, int pageSize, out int totalCount, out int totalPage, Expression<Func<T, bool>>

whereLambda, bool isAsc, Expression<Func<T, object>> orderBy, string columBy = "*")
        {
            using (var db = DbService.GetInstance())
            {
                var temp = db.Queryable<T>().Select(columBy).Where(whereLambda);
                totalCount = temp.Count();
                totalPage = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                temp = isAsc ? temp.OrderBy(orderBy) : temp.OrderBy(orderBy, OrderByType.desc);
                IEnumerable<object> enumerable = temp.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).ToList();
                return new PagingDataSet<T>(CacheByEntityIds<object>(enumerable)) { PageIndex = pageIndex, PageSize = pageSize, TotalRecords = totalCount };
            }
        }

        public virtual PagingDataSet<T> GetPageListByCache<U>(int pageIndex, int pageSize, out int totalCount, out int totalPage, string whereLambda, string

orderBy, Expression<Func<T, U>> primaryKey)
        {
            using (var db = DbService.GetInstance())
            {
                var temp = db.Queryable<T>().Where(whereLambda);
                var list = temp.ToList();
                totalCount = temp.Count();
                totalPage = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                temp = temp.OrderBy(orderBy);
                IEnumerable<U> enumerable = temp.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).Select(primaryKey).ToList();
                return new PagingDataSet<T>(CacheByEntityIds<U>(enumerable)) { PageIndex = pageIndex, PageSize = pageSize, TotalRecords = totalCount };
            }
        }

        public virtual PagingDataSet<T> GetPageListByCache<U>(int pageIndex, int pageSize, out int totalCount, out int totalPage, string whereSql, object whereObj, string

orderBy, Expression<Func<T, U>> primaryKey)
        {
            using (var db = DbService.GetInstance())
            {
                Queryable<T> temp = null;
                if (!string.IsNullOrWhiteSpace(whereSql))
                    temp = db.Queryable<T>().Where(whereSql, whereObj);
                else
                    temp = db.Queryable<T>();
                var list = temp.ToList();
                totalCount = temp.Count();
                totalPage = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                temp = temp.OrderBy(orderBy);
                IEnumerable<U> enumerable = temp.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).Select(primaryKey).ToList();
                return new PagingDataSet<T>(CacheByEntityIds<U>(enumerable)) { PageIndex = pageIndex, PageSize = pageSize, TotalRecords = totalCount };
            }
        }

        public virtual PagingDataSet<T> GetPageListByCache(int pageIndex, int pageSize, out int totalCount, out int totalPage, string  whereLambda, object param, string

orderBy, string columBy = "*")
        {
            using (var db = DbService.GetInstance())
            {
                var temp = db.Queryable<T>().Select(columBy).Where(whereLambda, param);
                totalCount = temp.Count();
                totalPage = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                temp = temp.OrderBy(orderBy);
                IEnumerable<object> enumerable = temp.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).ToList();
                return new PagingDataSet<T>(CacheByEntityIds<object>(enumerable)) { PageIndex = pageIndex, PageSize = pageSize, TotalRecords = totalCount };
            }
        }

        #endregion

        protected virtual void OnInserted(T entity)
        {
            if (Repository<T>.RealTimeCacheHelper.EnableCache)
            {
                Repository<T>.RealTimeCacheHelper.IncreaseListCacheVersion(entity);
                cacheService.Add(Repository<T>.RealTimeCacheHelper.GetCacheKeyOfEntity(entity.EntityId), entity, Repository<T>.RealTimeCacheHelper.CachingExpirationType);
            }
        }

        protected virtual void OnUpdated(T entity)
        {
            if (Repository<T>.RealTimeCacheHelper.EnableCache)
            {
                Repository<T>.RealTimeCacheHelper.IncreaseEntityCacheVersion(entity.EntityId);
                Repository<T>.RealTimeCacheHelper.IncreaseListCacheVersion(entity);
                cacheService.Set(Repository<T>.RealTimeCacheHelper.GetCacheKeyOfEntity(entity.EntityId), entity, Repository<T>.RealTimeCacheHelper.CachingExpirationType);
            }
        }

        protected virtual void OnDeleted(T entity)
        {
            if (Repository<T>.RealTimeCacheHelper.EnableCache)
            {
                cacheService.Remove(Repository<T>.RealTimeCacheHelper.GetCacheKeyOfEntity(entity.EntityId));
            }
        }

        
    }
}
