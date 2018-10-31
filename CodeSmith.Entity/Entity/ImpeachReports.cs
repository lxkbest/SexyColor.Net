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
    /// Description:用户举报
    /// </summary>
    [SugarMapping(TableName = "sc_impeach_reports")]
    [Serializable]
    public class ImpeachReports
    {
		#region	构造
		public ImpeachReports(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static ImpeachReports New()
        {
                
            ImpeachReports impeachReports = new ImpeachReports();
            impeachReports.Id = 0;
            impeachReports.Userid = 0;
            impeachReports.Username = string.Empty;
            impeachReports.Type = 0;
            impeachReports.ReporterUserid = 0;
            impeachReports.Email = string.Empty;
            impeachReports.Title = string.Empty;
            impeachReports.Phone = string.Empty;
            impeachReports.Reason = string.Empty;
            impeachReports.DateCreate = DateTime.UtcNow;
            impeachReports.DateLastupdate = DateTime.UtcNow;
            impeachReports.DisposerStatus = false;
            impeachReports.DisposerUserid = 0;
            impeachReports.DisposerUsername = string.Empty;
            impeachReports.ImageUrl = string.Empty;
            return impeachReports;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户举报标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 举报用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 举报用户名称
        /// </summary>
		[SugarMapping(ColumnName = "username")]
        public string Username { get; set; }
 
        /// <summary>
        /// 举报类型
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public int Type { get; set; }
 
        /// <summary>
        /// 被举报人Id
        /// </summary>
		[SugarMapping(ColumnName = "reporter_userid")]
        public long ReporterUserid { get; set; }
 
        /// <summary>
        /// 邮箱
        /// </summary>
		[SugarMapping(ColumnName = "email")]
        public string Email { get; set; }
 
        /// <summary>
        /// 举报内容标题
        /// </summary>
		[SugarMapping(ColumnName = "title")]
        public string Title { get; set; }
 
        /// <summary>
        /// 联系电话
        /// </summary>
		[SugarMapping(ColumnName = "phone")]
        public string Phone { get; set; }
 
        /// <summary>
        /// 举报原因
        /// </summary>
		[SugarMapping(ColumnName = "reason")]
        public string Reason { get; set; }
 
        /// <summary>
        /// 举报时间
        /// </summary>
		[SugarMapping(ColumnName = "date_create")]
        public System.DateTime DateCreate { get; set; }
 
        /// <summary>
        /// 处理时间
        /// </summary>
		[SugarMapping(ColumnName = "date_lastupdate")]
        public System.DateTime DateLastupdate { get; set; }
 
        /// <summary>
        /// 处理状态 0=未处理、1=已处理
        /// </summary>
		[SugarMapping(ColumnName = "disposer_status")]
        public bool DisposerStatus { get; set; }
 
        /// <summary>
        /// 处理人Id
        /// </summary>
		[SugarMapping(ColumnName = "disposer_userid")]
        public long DisposerUserid { get; set; }
 
        /// <summary>
        /// 处理人名称
        /// </summary>
		[SugarMapping(ColumnName = "disposer_username")]
        public string DisposerUsername { get; set; }
 
        /// <summary>
        /// 举报截图
        /// </summary>
		[SugarMapping(ColumnName = "image_url")]
        public string ImageUrl { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
