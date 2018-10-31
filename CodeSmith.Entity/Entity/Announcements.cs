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
    /// Description:站点公告
    /// </summary>
    [SugarMapping(TableName = "sc_announcements")]
    [Serializable]
    public class Announcements
    {
		#region	构造
		public Announcements(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Announcements New()
        {
                
            Announcements announcements = new Announcements();
            announcements.Id = 0;
            announcements.Subject = string.Empty;
            announcements.Body = string.Empty;
            announcements.DateRelease = DateTime.UtcNow;
            announcements.DateExpired = DateTime.UtcNow;
            announcements.DateLastupdate = DateTime.UtcNow;
            announcements.IsEnabled = false;
            announcements.DisplayOrder = 0;
            announcements.Userid = 0;
            announcements.DateCreate = DateTime.UtcNow;
            return announcements;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 站点公告标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 公告主题
        /// </summary>
		[SugarMapping(ColumnName = "subject")]
        public string Subject { get; set; }
 
        /// <summary>
        /// 公告内容
        /// </summary>
		[SugarMapping(ColumnName = "body")]
        public string Body { get; set; }
 
        /// <summary>
        /// 发布时间
        /// </summary>
		[SugarMapping(ColumnName = "date_release")]
        public System.DateTime DateRelease { get; set; }
 
        /// <summary>
        /// 过期时间
        /// </summary>
		[SugarMapping(ColumnName = "date_expired")]
        public System.DateTime DateExpired { get; set; }
 
        /// <summary>
        /// 更新时间
        /// </summary>
		[SugarMapping(ColumnName = "date_lastupdate")]
        public System.DateTime DateLastupdate { get; set; }
 
        /// <summary>
        /// 是否启用   0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_enabled")]
        public bool IsEnabled { get; set; }
 
        /// <summary>
        /// 显示顺序
        /// </summary>
		[SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }
 
        /// <summary>
        /// 创建人Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "date_create")]
        public System.DateTime DateCreate { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
