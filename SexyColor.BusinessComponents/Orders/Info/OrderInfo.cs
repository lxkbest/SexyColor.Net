using MySqlSugar;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:订单信息
    /// </summary>
    [SugarMapping(TableName = "sc_order_info")]
    [CacheSetting(true, PropertyNamesOfArea = "Orderid, OrderNumber")]
    [Serializable]
    public class OrderInfo : IEntity
    {
        #region	构造
        public OrderInfo()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static OrderInfo New()
        {

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.Orderid = 0;
            orderInfo.Userid = 0;
            orderInfo.OrderNumber = string.Empty;
            orderInfo.OriginalPrice = decimal.Zero;
            orderInfo.RealPrice = decimal.Zero;
            orderInfo.SavePrice = decimal.Zero;
            orderInfo.OrderRefundPrice = decimal.Zero;
            orderInfo.Status = 0;
            orderInfo.PayType = 0;
            orderInfo.PayDate = DateTime.UtcNow;
            orderInfo.DeteCreated = DateTime.UtcNow;
            orderInfo.SendDate = DateTime.UtcNow;
            orderInfo.ConfirmDate = DateTime.UtcNow;
            orderInfo.LogisticsType = 0;
            orderInfo.WaybillNumber = string.Empty;
            orderInfo.IsUse = false;
            orderInfo.Remark = string.Empty;
            return orderInfo;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 订单Id   
        /// </summary>
        public long Orderid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 订单号    比如：13964570771573416424 订单号需要由 订单Id+时间戳+随机数，丢给队列执行一定算法生成。以后应该是订单系统负责
        /// </summary>
        [SugarMapping(ColumnName = "order_number")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 订单原价
        /// </summary>
        [SugarMapping(ColumnName = "original_price")]
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 实际需要支付金额
        /// </summary>
        [SugarMapping(ColumnName = "real_price")]
        public decimal RealPrice { get; set; }

        /// <summary>
        /// 订单总优惠金额
        /// </summary>
        [SugarMapping(ColumnName = "save_price")]
        public decimal SavePrice { get; set; }

        /// <summary>
        /// 订单总退款金额
        /// </summary>
        [SugarMapping(ColumnName = "order_refund_price")]
        public decimal OrderRefundPrice { get; set; }

        /// <summary>
        /// 订单状态  1.已完成 2.已取消 3.待发货 4.待付款 5.待收货  默认0
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 支付方式  1支付宝 2微信支付  3 货到付款 4线下支付 5组合支付
        /// </summary>
        [SugarMapping(ColumnName = "pay_type")]
        public int PayType { get; set; }

        /// <summary>
        /// 支付成功时间
        /// </summary>
        [SugarMapping(ColumnName = "pay_date")]
        public DateTime PayDate { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        [SugarMapping(ColumnName = "dete_created")]
        public DateTime DeteCreated { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        [SugarMapping(ColumnName = "send_date")]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 确认收货时间
        /// </summary>
        [SugarMapping(ColumnName = "confirm_date")]
        public DateTime ConfirmDate { get; set; }

        /// <summary>
        /// 发货方式    1快递发货 2上门自提 3同城配送
        /// </summary>
        [SugarMapping(ColumnName = "logistics_type")]
        public int LogisticsType { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [SugarMapping(ColumnName = "waybill_number")]
        public string WaybillNumber { get; set; }

        /// <summary>
        /// 是否已使用优惠劵   0.否，1.是
        /// </summary>
        [SugarMapping(ColumnName = "is_use")]
        public bool IsUse { get; set; }

        /// <summary>
        /// 是否完成评价 0.否 ，1.是
        /// </summary>
        [SugarMapping(ColumnName = "is_complete_comment")]
        public bool IsCompleteComment { get; set; }

        /// <summary>
        /// 留言
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 买家名称
        /// </summary>
        [SugarMapping(ColumnName = "buyers_name")]
        public string BuyersName { get; set; }

        /// <summary>
        /// 买家手机号
        /// </summary>
        [SugarMapping(ColumnName = "buyers_phone")]
        public string BuyersPhone { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        [SugarMapping(ColumnName = "order_type")]
        public string OrderType { get; set; }

        /// <summary>
        /// 维权状态
        /// </summary>
        [SugarMapping(ColumnName = "rights_status")]
        public string RightsStatus { get; set; }

        /// <summary>
        /// 外部订单号
        /// </summary>
        [SugarMapping(ColumnName = "other_transaction_code")]
        public string OtherTransactionCode { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        [SugarMapping(ColumnName = "logistics_code")]
        public string LogisticsCode { get; set; }

        /// <summary>
        /// 收货地址编号
        /// </summary>
        public int Addressid { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId
        {
            get { return this.Orderid; }
        }
        #endregion
    }
}
