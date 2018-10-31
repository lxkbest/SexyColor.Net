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
    /// Description:订单支付记录
    /// </summary>
    [SugarMapping(TableName = "sc_order_pay_logs")]
    [Serializable]
    public class OrderPayLogs
    {
		#region	构造
		public OrderPayLogs(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static OrderPayLogs New()
        {
                
            OrderPayLogs orderPayLogs = new OrderPayLogs();
            orderPayLogs.Id = 0;
            orderPayLogs.Userid = 0;
            orderPayLogs.Orderid = 0;
            orderPayLogs.PayType = false;
            orderPayLogs.PayMoney = decimal.Zero;
            orderPayLogs.IsSuccess = false;
            orderPayLogs.DeteCreate = DateTime.UtcNow;
            return orderPayLogs;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 订单支付记录标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 订单Id
        /// </summary>
		[SugarMapping(ColumnName = "orderid")]
        public long Orderid { get; set; }
 
        /// <summary>
        /// 支付方式     1支付宝 2微信支付  3 货到付款 4线下支付 5组合支付 默认0
        /// </summary>
		[SugarMapping(ColumnName = "pay_type")]
        public bool PayType { get; set; }
 
        /// <summary>
        /// 支付金额 
        /// </summary>
		[SugarMapping(ColumnName = "pay_money")]
        public decimal PayMoney { get; set; }
 
        /// <summary>
        /// 是否成功   0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_success")]
        public bool IsSuccess { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "dete_create")]
        public System.DateTime DeteCreate { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
