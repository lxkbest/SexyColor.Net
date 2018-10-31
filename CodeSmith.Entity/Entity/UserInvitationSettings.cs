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
    /// Description:用户请求设置
    /// </summary>
    [SugarMapping(TableName = "sc_user_invitation_settings")]
    [Serializable]
    public class UserInvitationSettings
    {
		#region	构造
		public UserInvitationSettings(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static UserInvitationSettings New()
        {
                
            UserInvitationSettings userInvitationSettings = new UserInvitationSettings();
            userInvitationSettings.Id = 0;
            userInvitationSettings.Userid = 0;
            userInvitationSettings.IsAllow = false;
            return userInvitationSettings;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户请求设置标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 是否允许接受  0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_allow")]
        public bool IsAllow { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
