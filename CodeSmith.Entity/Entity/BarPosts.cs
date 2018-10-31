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
    /// Description:回帖
    /// </summary>
    [SugarMapping(TableName = "sc_bar_posts")]
    [Serializable]
    public class BarPosts
    {
		#region	构造
		public BarPosts(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static BarPosts New()
        {
                
            BarPosts barPosts = new BarPosts();
            barPosts.Postid = 0;
            barPosts.Circleid = 0;
            barPosts.Threadid = 0;
            barPosts.Parentid = 0;
            barPosts.Userid = 0;
            barPosts.UserName = string.Empty;
            barPosts.Body = string.Empty;
            barPosts.IsLocked = false;
            barPosts.Status = false;
            barPosts.Ip = string.Empty;
            barPosts.GiveCount = 0;
            barPosts.ChildPostCount = 0;
            barPosts.DateCreated = DateTime.UtcNow;
            barPosts.DateLastModified = DateTime.UtcNow;
            return barPosts;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 回帖Id
        /// </summary>
		[SugarMapping(ColumnName = "postid")]
        public long Postid { get; set; }
 
        /// <summary>
        /// 所属圈子Id
        /// </summary>
		[SugarMapping(ColumnName = "circleid")]
        public long Circleid { get; set; }
 
        /// <summary>
        /// 所属帖子Id
        /// </summary>
		[SugarMapping(ColumnName = "threadid")]
        public long Threadid { get; set; }
 
        /// <summary>
        /// 父回帖
        /// </summary>
		[SugarMapping(ColumnName = "parentid")]
        public long Parentid { get; set; }
 
        /// <summary>
        /// 回帖楼主Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 回帖楼主
        /// </summary>
		[SugarMapping(ColumnName = "user_name")]
        public string UserName { get; set; }
 
        /// <summary>
        /// 回帖内容
        /// </summary>
		[SugarMapping(ColumnName = "body")]
        public string Body { get; set; }
 
        /// <summary>
        /// 是否锁定  
        /// </summary>
		[SugarMapping(ColumnName = "is_locked")]
        public bool IsLocked { get; set; }
 
        /// <summary>
        /// 状态   默认0  暂时不确定 怎么用， 留着
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }
 
        /// <summary>
        /// 发帖人Ip
        /// </summary>
		[SugarMapping(ColumnName = "ip")]
        public string Ip { get; set; }
 
        /// <summary>
        /// 点赞数
        /// </summary>
		[SugarMapping(ColumnName = "give_count")]
        public int GiveCount { get; set; }
 
        /// <summary>
        /// 子回复数
        /// </summary>
		[SugarMapping(ColumnName = "child_post_count")]
        public int ChildPostCount { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
        /// <summary>
        /// 最后更新日期
        /// </summary>
		[SugarMapping(ColumnName = "date_last_modified")]
        public System.DateTime DateLastModified { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
