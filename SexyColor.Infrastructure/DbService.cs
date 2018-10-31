using MySqlSugar;
using System;

namespace SexyColor.Infrastructure
{
    public class DbService : IDisposable
    {
        public SqlSugarClient db;

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        public DbService(string connectionString = null)
        {
            db = GetConnectionClient(DbType.MySql, string.IsNullOrEmpty(connectionString) ? SqlConfigs.MySqlConnString : connectionString);
            db.IsEnableAttributeMapping = true;//启用属性映射
            db.IsIgnoreErrorColumns = true;
            db.IsEnableLogEvent = true;//启用日志事件
            db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };
        }

        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient dbClient = GetConnectionClient(DbType.MySql, SqlConfigs.MySqlConnString);
            dbClient.IsEnableAttributeMapping = true;//启用属性映射
            dbClient.IsIgnoreErrorColumns = true;
            dbClient.IsEnableLogEvent = true;//启用日志事件
            dbClient.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };
            return dbClient;
        }

        private static SqlSugarClient GetConnectionClient(DbType type, string connectionString)
        {
            SqlSugarClient db = null;
            switch (type)
            {
                case DbType.SqlServer:
                    db = new SqlSugarClient(connectionString);
                    break;
 
                case DbType.MySql:
                    db = new SqlSugarClient(connectionString);
                    break;
            }
            return db;
        }

    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// SqlServer
        /// </summary>
        SqlServer = 0,
        /// <summary>
        /// Sqlite
        /// </summary>
        Sqlite = 1,
        /// <summary>
        /// MySql
        /// </summary>
        MySql = 2,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle = 3
    }
}
