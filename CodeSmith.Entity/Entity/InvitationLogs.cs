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
    /// Description:请求记录
    /// </summary>
    [SugarMapping(TableName = "sc_invitation_logs")]
    [Serializable]
    public class InvitationLogs
    {
		#region	构造
		public InvitationLogs(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static InvitationLogs New()
        {
                
            InvitationLogs invitationLogs = new InvitationLogs();
            invitationLogs.Id = 0;
            invitationLogs.Userid = 0;
            invitationLogs.UserName = string.Empty;
            invitationLogs.SenderUserid = 0;
            invitationLogs.SenderUserName = string.Empty;
            invitationLogs.Status = 0;
            invitationLogs.DateCreated = DateTime.UtcNow;
            return invitationLogs;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 请求记录标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 请求接受人Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 请求接受人
        /// </summary>
		[SugarMapping(ColumnName = "user_name")]
        public string UserName { get; set; }
 
        /// <summary>
        /// 请求发送人Id
        /// </summary>
		[SugarMapping(ColumnName = "sender_userid")]
        public long SenderUserid { get; set; }
 
        /// <summary>
        /// 请求发送人
        /// </summary>
		[SugarMapping(ColumnName = "sender_user_name")]
        public string SenderUserName { get; set; }
 
        /// <summary>
        /// 状态  1.未处理，2.接受，3.拒绝
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public int Status { get; set; }
 
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
