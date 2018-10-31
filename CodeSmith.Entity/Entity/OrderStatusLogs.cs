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
    /// Description:订单状态变更记录
    /// </summary>
    [SugarMapping(TableName = "sc_order_status_logs")]
    [Serializable]
    public class OrderStatusLogs
    {
		#region	构造
		public OrderStatusLogs(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static OrderStatusLogs New()
        {
                
            OrderStatusLogs orderStatusLogs = new OrderStatusLogs();
            orderStatusLogs.Id = 0;
            orderStatusLogs.Orderid = 0;
            orderStatusLogs.Userid = string.Empty;
            orderStatusLogs.Operation = string.Empty;
            orderStatusLogs.Status = false;
            orderStatusLogs.StatusName = string.Empty;
            orderStatusLogs.DateCreated = DateTime.UtcNow;
            return orderStatusLogs;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 订单状态变更记录标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 订单Id
        /// </summary>
		[SugarMapping(ColumnName = "orderid")]
        public long Orderid { get; set; }
 
        /// <summary>
        /// 系统操作或用户操作或管理人员操作
            
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public string Userid { get; set; }
 
        /// <summary>
        /// 您的订单支付成功 ，用户确认签收，延迟配送，用户要求退款，未发货，取消订单 退款&退款金额$32.0  等等信息
        /// </summary>
		[SugarMapping(ColumnName = "operation")]
        public string Operation { get; set; }
 
        /// <summary>
        /// 状态   
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }
 
        /// <summary>
        /// 状态说明
        /// </summary>
		[SugarMapping(ColumnName = "status_name")]
        public string StatusName { get; set; }
 
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
