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
    /// Description:点赞记录
    /// </summary>
    [SugarMapping(TableName = "sc_give_logs")]
    [Serializable]
    public class GiveLogs
    {
		#region	构造
		public GiveLogs(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GiveLogs New()
        {
                
            GiveLogs giveLogs = new GiveLogs();
            giveLogs.Id = 0;
            giveLogs.Objectid = 0;
            giveLogs.Userid = 0;
            giveLogs.Type = false;
            giveLogs.DateCreated = DateTime.UtcNow;
            return giveLogs;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 回帖点赞标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 操作对象Id  1.回帖点赞，2.商品评论点赞，3.帖子点赞，4.活动点赞，5.圈子点赞，6. 用户点赞
        /// </summary>
		[SugarMapping(ColumnName = "objectid")]
        public long Objectid { get; set; }
 
        /// <summary>
        /// 用户Id 换纯字符串为匿名用户或未注册的用户
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 点赞类型
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public bool Type { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
