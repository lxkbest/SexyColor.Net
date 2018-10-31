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
    /// Description:短信发送记录
    /// </summary>
    [SugarMapping(TableName = "sc_sms_send_logs")]
    [Serializable]
    public class SmsSendLogs
    {
		#region	构造
		public SmsSendLogs(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static SmsSendLogs New()
        {
                
            SmsSendLogs smsSendLogs = new SmsSendLogs();
            smsSendLogs.Id = 0;
            smsSendLogs.Userid = 0;
            smsSendLogs.UserName = string.Empty;
            smsSendLogs.Type = false;
            smsSendLogs.Content = string.Empty;
            smsSendLogs.DateCreate = DateTime.UtcNow;
            smsSendLogs.PhoneNumber = string.Empty;
            smsSendLogs.Result = string.Empty;
            smsSendLogs.Remarks = string.Empty;
            return smsSendLogs;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 短信发送记录标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 发送者Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 发送者名称
        /// </summary>
		[SugarMapping(ColumnName = "user_name")]
        public string UserName { get; set; }
 
        /// <summary>
        /// 类型    0=普通短信   1=系统短信
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public bool Type { get; set; }
 
        /// <summary>
        /// 短信内容
        /// </summary>
		[SugarMapping(ColumnName = "content")]
        public string Content { get; set; }
 
        /// <summary>
        /// 发送日期
        /// </summary>
		[SugarMapping(ColumnName = "date_create")]
        public System.DateTime DateCreate { get; set; }
 
        /// <summary>
        /// 发送号码
        /// </summary>
		[SugarMapping(ColumnName = "phone_number")]
        public string PhoneNumber { get; set; }
 
        /// <summary>
        /// 返回结果
        /// </summary>
		[SugarMapping(ColumnName = "result")]
        public string Result { get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
		[SugarMapping(ColumnName = "remarks")]
        public string Remarks { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
