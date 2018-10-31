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
    /// Description:帖子浏览记录
    /// </summary>
    [SugarMapping(TableName = "sc_thread_browse_logs")]
    [Serializable]
    public class ThreadBrowseLogs
    {
		#region	构造
		public ThreadBrowseLogs(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static ThreadBrowseLogs New()
        {
                
            ThreadBrowseLogs threadBrowseLogs = new ThreadBrowseLogs();
            threadBrowseLogs.Id = 0;
            threadBrowseLogs.Circleid = 0;
            threadBrowseLogs.Threadid = 0;
            threadBrowseLogs.Userid = 0;
            threadBrowseLogs.ThreadSubject = string.Empty;
            threadBrowseLogs.CircleName = string.Empty;
            threadBrowseLogs.DateCreated = DateTime.UtcNow;
            return threadBrowseLogs;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 帖子浏览记录标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 圈子Id
        /// </summary>
		[SugarMapping(ColumnName = "circleid")]
        public long Circleid { get; set; }
 
        /// <summary>
        /// 帖子Id
        /// </summary>
		[SugarMapping(ColumnName = "threadid")]
        public long Threadid { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 帖子标题
        /// </summary>
		[SugarMapping(ColumnName = "thread_subject")]
        public string ThreadSubject { get; set; }
 
        /// <summary>
        /// 圈子名称
        /// </summary>
		[SugarMapping(ColumnName = "circle_name")]
        public string CircleName { get; set; }
 
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
