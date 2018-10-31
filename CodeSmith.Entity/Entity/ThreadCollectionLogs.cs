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
    /// Description:帖子收藏记录
    /// </summary>
    [SugarMapping(TableName = "sc_thread_collection_logs")]
    [Serializable]
    public class ThreadCollectionLogs
    {
		#region	构造
		public ThreadCollectionLogs(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static ThreadCollectionLogs New()
        {
                
            ThreadCollectionLogs threadCollectionLogs = new ThreadCollectionLogs();
            threadCollectionLogs.Id = 0;
            threadCollectionLogs.Circleid = 0;
            threadCollectionLogs.Threadid = 0;
            threadCollectionLogs.Userid = 0;
            threadCollectionLogs.ThreadSubject = string.Empty;
            threadCollectionLogs.CircleName = string.Empty;
            threadCollectionLogs.ReplyCount = 0;
            threadCollectionLogs.DateCreated = DateTime.UtcNow;
            return threadCollectionLogs;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 帖子收藏记录标识
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
        /// 回帖数
        /// </summary>
		[SugarMapping(ColumnName = "reply_count")]
        public int ReplyCount { get; set; }
 
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
