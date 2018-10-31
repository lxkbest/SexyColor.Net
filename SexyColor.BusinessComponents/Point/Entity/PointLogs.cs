using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    [SugarMapping(TableName = "sc_point_logs")]
    [Serializable]
    public class PointLogs : IEntity
    {
        #region	构造
        public PointLogs()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static PointLogs New()
        {

            PointLogs pointLogs = new PointLogs();
            pointLogs.Id = 0;
            pointLogs.Userid = 0;
            pointLogs.Itemskey = string.Empty;
            pointLogs.Itemsname = string.Empty;
            pointLogs.SexyPoints = 0;
            pointLogs.ExperiencePoints = 0;
            pointLogs.IsIncome = false;
            pointLogs.Description = string.Empty;
            pointLogs.DateCreated = DateTime.UtcNow;
            pointLogs.DateOverdue = DateTime.UtcNow;
            return pointLogs;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 用户积分标识
        /// </summary>
        //[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        //[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }

        /// <summary>
        /// 积分项目标识
        /// </summary>
        [SugarMapping(ColumnName = "itemskey")]
        public string Itemskey { get; set; }

        /// <summary>
        /// 积分项目名称
        /// </summary>
        [SugarMapping(ColumnName = "itemsname")]
        public string Itemsname { get; set; }

        /// <summary>
        /// 积分值   (概念为：币，豆，丸)
        /// </summary>
        [SugarMapping(ColumnName = "sexy_points")]
        public int SexyPoints { get; set; }

        /// <summary>
        /// 经验值
        /// </summary>
        [SugarMapping(ColumnName = "experience_points")]
        public int ExperiencePoints { get; set; }

        /// <summary>
        /// 是否收入  0.支出，1.收入
        /// </summary>
        [SugarMapping(ColumnName = "is_income")]
        public bool IsIncome { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarMapping(ColumnName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 获取时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        [SugarMapping(ColumnName = "date_overdue")]
        public System.DateTime DateOverdue { get; set; }



        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
