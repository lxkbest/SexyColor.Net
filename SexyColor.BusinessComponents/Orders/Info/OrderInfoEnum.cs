using System.ComponentModel.DataAnnotations;

namespace SexyColor.BusinessComponents
{
    public enum OrderPayType
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Display(Name = "支付宝")]
        AliPay = 1,

        /// <summary>
        /// 微信支付
        /// </summary>
        [Display(Name = "微信支付")]
        WxPay = 2,

        /// <summary>
        /// 苹果支付
        /// </summary>
        [Display(Name = "苹果支付")]
        ApplePay = 3,

        /// <summary>
        /// 银行卡/信用卡支付
        /// </summary>
        [Display(Name = "银行卡支付")]
        CardPay = 4,

        /// <summary>
        /// 货到付款/到店付款
        /// </summary>
        [Display(Name = "货到付款")]
        ToPay = 5 
    }

    public enum OrderModifyStatus
    {
        /// <summary>
        /// 等待付款
        /// </summary>
        [Display(Name = "待付款")]
        Pay = 1,
        /// <summary>
        /// 等待发货 
        /// </summary>
        [Display(Name = "待发货 ")]
        Send = 2,
        /// <summary>
        /// 等待收货
        /// </summary>
        [Display(Name = "待收货")]
        Collect = 3,
        /// <summary>
        /// 完成
        /// </summary>
        [Display(Name = "已完成")]
        Complete = 4,
        /// <summary>
        /// 取消
        /// </summary>
        [Display(Name = "已取消")]
        Cancel = 5,
        /// <summary>
        /// 结束
        /// </summary>
        [Display(Name = "已结束")]
        Flish = 6,
        /// <summary>
        /// 申请退货、退款
        /// </summary>
        [Display(Name = "退款中")]
        Retreat = 7
    }

    /// <summary>
    /// 物流方式
    /// </summary>
    public enum OrderLogisticsMode
    {
        /// <summary>
        /// 快递发货
        /// </summary>
        [Display(Name = "快递发货")]
        ExpressSend = 1,

        /// <summary>
        /// 同城配送
        /// </summary>
        [Display(Name = "同城配送")]
        CityDelivery = 2,

        /// <summary>
        /// 上门自提
        /// </summary>
        [Display(Name = "上门自提")]
        DoorTODoor = 3,

    }

    /// <summary>
    /// 维权状态
    /// </summary>
    public enum OrderRightsStatus
    {
        /// <summary>
        /// 退款中
        /// </summary>
        [Display(Name = "退款中")]
        Retreating = 1,
        /// <summary>
        /// 退款结束
        /// </summary>
        [Display(Name = "退款结束")]
        Retreated = 2,
    }

    /// <summary>
    /// 订单类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 普通订单
        /// </summary>
        [Display(Name = "普通订单")]
        OrdinaryOrder = 1,

        /// <summary>
        /// 代付订单
        /// </summary>
        [Display(Name = "代付订单")]
        ReplacePayOrder = 2,

        /// <summary>
        /// 送礼订单
        /// </summary>
        [Display(Name = "送礼订单")]
        GiftsOrder = 3,

        /// <summary>
        /// 心愿订单
        /// </summary>
        [Display(Name = "心愿订单")]
        WishOrder = 4,

        /// <summary>
        /// 多人拼团订单
        /// </summary>
        [Display(Name = "多人拼团订单")]
        ConnectOrder = 5,

        /// <summary>
        /// 积分兑换订单
        /// </summary>
        [Display(Name = "积分兑换订单")]
        IntegralOrder = 6
    }

    /// <summary>
    /// 订单变更状态
    /// </summary>
    public enum OrderChangeStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownFailure = 0,

        /// <summary>
        /// 更新成功
        /// </summary>
        Updated = 1,

        /// <summary>
        /// 订单状态异常
        /// </summary>
        StatusException = 2,

        /// <summary>
        /// 操作用户丢失
        /// </summary>
        OperationNULL = 3,

        /// <summary>
        /// 物流代码或运单号未填写
        /// </summary>
        LogisticsCodeOrWaybillNumberNULL = 4
    }
}
