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
    /// Description:用户通知设置
    /// </summary>
    [SugarMapping(TableName = "sc_user_notice_settings")]
    [Serializable]
    public class UserNoticeSettings
    {
		#region	构造
		public UserNoticeSettings(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static UserNoticeSettings New()
        {
                
            UserNoticeSettings userNoticeSettings = new UserNoticeSettings();
            userNoticeSettings.Id = 0;
            userNoticeSettings.Userid = 0;
            userNoticeSettings.Type = false;
            userNoticeSettings.IsAllowable = false;
            userNoticeSettings.NoticeType = false;
            return userNoticeSettings;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户通知设置标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 类型Id 1.帖子被回复  2.评论被回复 3.系统消息
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public bool Type { get; set; }
 
        /// <summary>
        /// 是否接受  0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_allowable")]
        public bool IsAllowable { get; set; }
 
        /// <summary>
        /// 通知类型  1.微信，2.短信，3.智能手机通知
        /// </summary>
		[SugarMapping(ColumnName = "notice_type")]
        public bool NoticeType { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
