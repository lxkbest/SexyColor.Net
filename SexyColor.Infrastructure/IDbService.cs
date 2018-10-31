using MySqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    public interface IDbService : IDisposable
    {
        SqlSugarClient GetDb();
    }
}
