using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
	/// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:关注圈子
    /// </summary>
    [SugarMapping(TableName = "sc_bar_follow")]
    [Serializable]
    public class BarFollow
    {
		#region	构造
		public BarFollow(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BarFollow New()
        {
                
            BarFollow barFollow = new BarFollow();
            barFollow.Id = 0;
            barFollow.Circleid = 0;
            barFollow.Userid = 0;
            barFollow.UserName = string.Empty;
            barFollow.DateCreated = DateTime.UtcNow;
            return barFollow;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 关注圈子标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 被关注的圈子Id
        /// </summary>
		[SugarMapping(ColumnName = "circleid")]
        public long Circleid { get; set; }
 
        /// <summary>
        /// 关注用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 关注用户名
        /// </summary>
		[SugarMapping(ColumnName = "user_name")]
        public string UserName { get; set; }
 
        /// <summary>
        /// 关注日期
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
