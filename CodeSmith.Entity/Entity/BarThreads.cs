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
    /// Description:帖子
    /// </summary>
    [SugarMapping(TableName = "sc_bar_threads")]
    [Serializable]
    public class BarThreads
    {
		#region	构造
		public BarThreads(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BarThreads New()
        {
                
            BarThreads barThreads = new BarThreads();
            barThreads.Threadid = 0;
            barThreads.Circleid = 0;
            barThreads.Userid = 0;
            barThreads.UserName = string.Empty;
            barThreads.Subject = string.Empty;
            barThreads.Body = string.Empty;
            barThreads.Type = false;
            barThreads.IsLocked = false;
            barThreads.IsEssential = false;
            barThreads.IsSticky = false;
            barThreads.DateSticky = DateTime.UtcNow;
            barThreads.IsHidden = false;
            barThreads.StyleHighlight = string.Empty;
            barThreads.DateHighlight = DateTime.UtcNow;
            barThreads.Status = false;
            barThreads.ReplyCount = 0;
            barThreads.Ip = string.Empty;
            barThreads.DateCreated = DateTime.UtcNow;
            barThreads.DateLastModified = DateTime.UtcNow;
            barThreads.IsVote = false;
            barThreads.VoteEndDate = DateTime.UtcNow;
            return barThreads;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 帖子Id
        /// </summary>
		[SugarMapping(ColumnName = "threadid")]
        public long Threadid { get; set; }
 
        /// <summary>
        /// 圈子Id
        /// </summary>
		[SugarMapping(ColumnName = "circleid")]
        public long Circleid { get; set; }
 
        /// <summary>
        /// 楼主Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 楼主名称
        /// </summary>
		[SugarMapping(ColumnName = "user_name")]
        public string UserName { get; set; }
 
        /// <summary>
        /// 帖子标题
        /// </summary>
		[SugarMapping(ColumnName = "subject")]
        public string Subject { get; set; }
 
        /// <summary>
        /// 
        /// </summary>
		[SugarMapping(ColumnName = "body")]
        public string Body { get; set; }
 
        /// <summary>
        /// 1.谈情，2.姿势，3.性爱
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public bool Type { get; set; }
 
        /// <summary>
        /// 是否锁定   0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_locked")]
        public bool IsLocked { get; set; }
 
        /// <summary>
        /// 是否精华  0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_essential")]
        public bool IsEssential { get; set; }
 
        /// <summary>
        /// 是否置顶   0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_sticky")]
        public bool IsSticky { get; set; }
 
        /// <summary>
        /// 置顶期限
        /// </summary>
		[SugarMapping(ColumnName = "date_sticky")]
        public System.DateTime DateSticky { get; set; }
 
        /// <summary>
        /// 是否仅回复可见  0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_hidden")]
        public bool IsHidden { get; set; }
 
        /// <summary>
        /// 高亮显示的样式
        /// </summary>
		[SugarMapping(ColumnName = "style_highlight")]
        public string StyleHighlight { get; set; }
 
        /// <summary>
        /// 高亮显示期限
        /// </summary>
		[SugarMapping(ColumnName = "date_highlight")]
        public System.DateTime DateHighlight { get; set; }
 
        /// <summary>
        /// 状态   
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }
 
        /// <summary>
        /// 回复数  默认0
        /// </summary>
		[SugarMapping(ColumnName = "reply_count")]
        public int ReplyCount { get; set; }
 
        /// <summary>
        /// 发帖人Ip  
        /// </summary>
		[SugarMapping(ColumnName = "ip")]
        public string Ip { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
        /// <summary>
        /// 最后更新时间
        /// </summary>
		[SugarMapping(ColumnName = "date_last_modified")]
        public System.DateTime DateLastModified { get; set; }
 
        /// <summary>
        /// 是否投票贴  0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_vote")]
        public bool IsVote { get; set; }
 
        /// <summary>
        /// 投票有效日期
        /// </summary>
		[SugarMapping(ColumnName = "vote_end_date")]
        public System.DateTime VoteEndDate { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
