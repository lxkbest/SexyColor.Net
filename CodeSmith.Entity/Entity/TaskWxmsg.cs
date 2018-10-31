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
    /// Description:微信消息任务
    /// </summary>
    [SugarMapping(TableName = "sc_task_wxmsg")]
    [Serializable]
    public class TaskWxmsg
    {
		#region	构造
		public TaskWxmsg(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static TaskWxmsg New()
        {
                
            TaskWxmsg taskWxmsg = new TaskWxmsg();
            taskWxmsg.Id = 0;
            taskWxmsg.Openid = string.Empty;
            taskWxmsg.TypeMsg = false;
            taskWxmsg.TextText = string.Empty;
            taskWxmsg.TemplateId = string.Empty;
            taskWxmsg.Title = string.Empty;
            taskWxmsg.Url = string.Empty;
            taskWxmsg.Body = string.Empty;
            taskWxmsg.SendDate = DateTime.UtcNow;
            taskWxmsg.FailCount = 0;
            taskWxmsg.Status = false;
            taskWxmsg.ImageUrl = string.Empty;
            taskWxmsg.DeteCreated = DateTime.UtcNow;
            return taskWxmsg;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 任务消息标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
        /// <summary>
        /// 用户Openid
        /// </summary>
		[SugarMapping(ColumnName = "openid")]
        public string Openid { get; set; }
 
        /// <summary>
        /// 消息类型  1.退货消息，2.处理订单消息，3.手动任务消息，4.活动消息，5.其他消息
        /// </summary>
		[SugarMapping(ColumnName = "type_msg")]
        public bool TypeMsg { get; set; }
 
        /// <summary>
        /// 消息内容
        /// </summary>
		[SugarMapping(ColumnName = "text_text")]
        public string TextText { get; set; }
 
        /// <summary>
        /// 微信模板编号
        /// </summary>
		[SugarMapping(ColumnName = "template_id")]
        public string TemplateId { get; set; }
 
        /// <summary>
        /// 标题
        /// </summary>
		[SugarMapping(ColumnName = "title")]
        public string Title { get; set; }
 
        /// <summary>
        /// 链接地址
        /// </summary>
		[SugarMapping(ColumnName = "url")]
        public string Url { get; set; }
 
        /// <summary>
        /// 内容   varchar(max)
        /// </summary>
		[SugarMapping(ColumnName = "body")]
        public string Body { get; set; }
 
        /// <summary>
        /// 发送时间
        /// </summary>
		[SugarMapping(ColumnName = "send_date")]
        public System.DateTime SendDate { get; set; }
 
        /// <summary>
        /// 发送错误次数
        /// </summary>
		[SugarMapping(ColumnName = "fail_count")]
        public int FailCount { get; set; }
 
        /// <summary>
        /// 状态  1.发送成功，2.发送失败，3.用户未关注
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }
 
        /// <summary>
        /// 图片路径
        /// </summary>
		[SugarMapping(ColumnName = "image_url")]
        public string ImageUrl { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "dete_created")]
        public System.DateTime DeteCreated { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
