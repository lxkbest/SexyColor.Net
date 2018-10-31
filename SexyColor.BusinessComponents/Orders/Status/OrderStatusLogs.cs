using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:订单状态变更记录
    /// </summary>
    [SugarMapping(TableName = "sc_order_status_logs")]
    [CacheSetting(true, PropertyNamesOfArea = "Orderid")]
    [Serializable]
    public class OrderStatusLogs : IEntity
    {
        #region	构造
        public OrderStatusLogs()
        {

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
            orderStatusLogs.OrderNumber = string.Empty;
            orderStatusLogs.Userid = 0;
            orderStatusLogs.Username = string.Empty;
            orderStatusLogs.Operation = string.Empty;
            orderStatusLogs.Status = 0;
            orderStatusLogs.StatusName = string.Empty;
            orderStatusLogs.DateCreated = DateTime.UtcNow;
            return orderStatusLogs;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 订单状态变更记录标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public long Orderid { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [SugarMapping(ColumnName = "order_number")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 系统操作或用户操作或管理人员操作
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 您的订单支付成功 ，用户确认签收，延迟配送，用户要求退款，未发货，取消订单 退款&退款金额$32.0  等等信息
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 状态   
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态说明
        /// </summary>
        [SugarMapping(ColumnName = "status_name")]
        public string StatusName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId
        {
            get { return this.Id; }
        }
        #endregion
    }
}
