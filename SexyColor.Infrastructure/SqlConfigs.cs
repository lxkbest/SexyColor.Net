namespace SexyColor.Infrastructure
{
    public class SqlConfigs
    {
        private SqlConfigs()
        {

        }

        public static string SqlConnString = "server=.;uid=sa;pwd=~!@147asd369;database=SqlSugarTest";
        public static string MySqlConnString = "server=localhost;Database=sexycolor;Uid=root;Pwd=123456";
        public static string PlSqlConnString = "Data Source=localhost/orcl;User ID=system;Password=JHL52771jhl;";
        public static string SqliteSqlConnString = @"DataSource=F:\MyOpenSource\SqlSugarRepository\OrmTest\OrmTest\Database\demo.sqlite";

    }
}
