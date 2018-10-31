using MySqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SexyColor.Infrastructure
{
    public interface IRepository<T> where T : class, IEntity , new()
    {
        #region 基础方法

        T Add(T entity, bool isIdentity = true);

        void AddEntity(List<T> entitys, bool isIdentity = true);

        bool Update(T entity);

        bool Update(object model, Expression<Func<T, bool>> expression);

        bool Update(List<T> entitys);

        bool Delete(Expression<Func<T, bool>> predicate);

        bool Delete(List<T> entitys);

        T GetModel(Expression<Func<T, bool>> predicate);

        List<T> GetModelByIds<U>(Expression<Func<T, U>> predicate, IEnumerable<U> entityIds);

        bool Exist(Expression<Func<T, bool>> anyLambda);

        int Count(Expression<Func<T, bool>> predicate);

        Queryable<T> GetList(Expression<Func<T, bool>> whereLamdba);

        Queryable<T> GetPageList(int pageIndex, int pageSize, out int rows, out int totalPage, Expression<Func<T, bool>> whereLambda, bool isAsc,
            Expression<Func<T, Object>> orderBy);

        Queryable<T> GetPageList(int pageIndex, int pageSize, out int rows, out int totalPage, Expression<Func<T, bool>> whereLambda, string orderBy);

        Queryable<T> GetPageList(int pageIndex, int pageSize, out int rows, out int totalPage, string whereLambda, string orderBy);

        dynamic GetList(string strSql);

        string GetJson(string strSql);

        List<T> GetSqlList(string sql, object whereObj = null);

        List<T> GetSqlList(string sql);


        #endregion

        #region 实体带缓存
        T GetByCache(Expression<Func<T, bool>> expression, object entityId);
        IEnumerable<T> GetAllByCache<U>(Expression<Func<T, object>> expression, Expression<Func<T, U>> primaryKey);
        IEnumerable<T> GetAllByCache<U>(Expression<Func<T, object>> expression, Expression<Func<T, U>> primaryKey, OrderByType orderType);
        IEnumerable<T> GetAllByCache<U>(string expression, Expression<Func<T, U>> primaryKey);
        IEnumerable<T> CacheByEntityIds<U>(IEnumerable<U> entityIds);
        object AddByCache(T entity, bool isIdentity);
        bool AddByCache(List<T> entitys);
        //PagingDataSet<T> GetPageListByCache(int pageIndex, int pageSize, out int totalCount, out int totalPage, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, object>> orderBy, string columBy);
        //PagingDataSet<T> GetPageListByCache(int pageIndex, int pageSize, out int totalCount, out int totalPage, string whereLambda, string orderBy, string columBy);
        PagingDataSet<T> GetPageListByCache<U>(int pageIndex, int pageSize, out int totalCount, out int totalPage, string whereLambda, string orderBy, Expression<Func<T, U>> primaryKey);
        #endregion
    }
}
