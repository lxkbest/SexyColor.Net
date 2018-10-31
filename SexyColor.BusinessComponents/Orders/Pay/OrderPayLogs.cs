using MySqlSugar;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    [SugarMapping(TableName = "sc_order_pay_logs")]
    [CacheSetting(true, PropertyNamesOfArea = "Orderid")]
    [Serializable]
    public class OrderPayLogs : IEntity
    {
        #region	构造
        public OrderPayLogs()
        {

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
            orderPayLogs.PayType = 0;
            orderPayLogs.PayMoney = decimal.Zero;
            orderPayLogs.IsSuccess = false;
            orderPayLogs.DateCreated = DateTime.UtcNow;
            orderPayLogs.OtherSerialCode = string.Empty;
            orderPayLogs.OtherTransactionCode = string.Empty;
            return orderPayLogs;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 订单支付记录标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long Userid { get; set; }

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
        /// 支付方式     1支付宝 2微信支付  3 货到付款 4线下支付 5组合支付 默认0
        /// </summary>
        [SugarMapping(ColumnName = "pay_type")]
        public int PayType { get; set; }

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
        [SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 第三方流水号
        /// </summary>
        [SugarMapping(ColumnName = "other_serial_code")]
        public string OtherSerialCode { get; set; }

        /// <summary>
        /// 第三方交易号
        /// </summary>
        [SugarMapping(ColumnName = "other_transaction_code")]
        public string OtherTransactionCode { get; set; }


        #endregion

        #region 扩展属性
        public object EntityId
        {
            get { return this.Id; }
        }
        #endregion
    }
}
