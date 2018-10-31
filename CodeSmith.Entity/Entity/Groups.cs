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
    /// Description:群组
    /// </summary>
    [SugarMapping(TableName = "sc_groups")]
    [Serializable]
    public class Groups
    {
		#region	构造
		public Groups(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Groups New()
        {
                
            Groups groups = new Groups();
            groups.Groupid = 0;
            groups.Groupname = string.Empty;
            groups.Description = string.Empty;
            groups.AreaCode = string.Empty;
            groups.Userid = 0;
            groups.Announcement = string.Empty;
            groups.Logo = string.Empty;
            groups.IsPublic = false;
            groups.JoinWay = false;
            groups.IsEnableMember = false;
            groups.AuditStatus = false;
            groups.MemberCount = 0;
            groups.Ip = string.Empty;
            groups.DateCreated = DateTime.UtcNow;
            return groups;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 群组Id
        /// </summary>
		[SugarMapping(ColumnName = "groupid")]
        public long Groupid { get; set; }
 
        /// <summary>
        /// 群组名称
        /// </summary>
		[SugarMapping(ColumnName = "groupname")]
        public string Groupname { get; set; }
 
        /// <summary>
        /// 群组介绍
        /// </summary>
		[SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
 
        /// <summary>
        /// 所在地区
        /// </summary>
		[SugarMapping(ColumnName = "area_code")]
        public string AreaCode { get; set; }
 
        /// <summary>
        /// 群主
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 公告
        /// </summary>
		[SugarMapping(ColumnName = "announcement")]
        public string Announcement { get; set; }
 
        /// <summary>
        /// Logo
        /// </summary>
		[SugarMapping(ColumnName = "logo")]
        public string Logo { get; set; }
 
        /// <summary>
        /// 是否公开  0：否，1：是 默认1
        /// </summary>
		[SugarMapping(ColumnName = "is_public")]
        public bool IsPublic { get; set; }
 
        /// <summary>
        /// 加入方式   1.扫码加入，2.邀请加入  默认0
        /// </summary>
		[SugarMapping(ColumnName = "join_way")]
        public bool JoinWay { get; set; }
 
        /// <summary>
        /// 是否允许成员邀请  0：否，1：是
        /// </summary>
		[SugarMapping(ColumnName = "is_enable_member")]
        public bool IsEnableMember { get; set; }
 
        /// <summary>
        /// 审核状态  1：通过，2：不通过
        /// </summary>
		[SugarMapping(ColumnName = "audit_status")]
        public bool AuditStatus { get; set; }
 
        /// <summary>
        /// 成员数
        /// </summary>
		[SugarMapping(ColumnName = "member_count")]
        public int MemberCount { get; set; }
 
        /// <summary>
        /// Ip地址
        /// </summary>
		[SugarMapping(ColumnName = "ip")]
        public string Ip { get; set; }
 
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
