using System;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.BusinessComponents
{
    public class OrderInfoQuery
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber = string.Empty;

        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber = string.Empty;

        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OtherOrderNumber = string.Empty;

        /// <summary>
        /// 买家名称
        /// </summary>
        public string Buyers = string.Empty;

        /// <summary>
        /// 买家手机号
        /// </summary>
        public string BuyersPhone = string.Empty;

        

        /// <summary>
        /// 是否使用优惠券
        /// </summary>
        public bool? IsUse = null;

        /// <summary>
        /// 是否完成评价
        /// </summary>
        public bool? IsCompleteComment = null;

        /// <summary>
        /// 实际支付订单价格下限
        /// </summary>
        public decimal? RealPriceLowerLimit = null;

        /// <summary>
        /// 实际支付订单价格上限
        /// </summary>
        public decimal? RealPriceUpperLimit = null;

        /// <summary>
        /// 下单时间下限
        /// </summary>
        public DateTime? OrderTimeLowerLimit = null;

        /// <summary>
        /// 下单时间上限
        /// </summary>
        public DateTime? OrderTimeUpperLimit = null;

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderModifyStatus? OrderInfoStatus = null;

        /// <summary>
        /// 支付方式
        /// </summary>
        public OrderPayType? OrderPayType = null;

        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType? OrderType = null;

        /// <summary>
        /// 维权状态
        /// </summary>
        public OrderRightsStatus? OrderRightsStatus = null;

        /// <summary>
        /// 物流方式
        /// </summary>
        public OrderLogisticsMode? OrderLogisticsMode = null;

        /// <summary>
        /// 订单排序
        /// </summary>
        public OrderInfoSortBy OrderInfoSortBy = OrderInfoSortBy.Keynum_DESC;

        /// <summary>
        /// 关键字
        /// </summary>
        public KeyWordEnum KeyWordEnum = KeyWordEnum.Buyers;


    }

    public enum KeyWordEnum
    {
        [Display(Name = "买家名称")]
        Buyers = 1,
        [Display(Name = "手机号码")]
        BuyersPhone = 2,
        [Display(Name = "订单号")]
        OrderNumber = 3,
        [Display(Name = "运单号")]
        WaybillNumber = 4,
        [Display(Name = "外部单号")]
        OtherOrderNumber = 5
    }

    public enum OrderInfoSortBy
    {
        /// <summary>
        /// 订单编号升序
        /// </summary>
        Keynum_ASC = 1,
        /// <summary>
        /// 订单编号降序
        /// </summary>
        Keynum_DESC = 2,
        /// <summary>
        /// 订单实际支付价格升序
        /// </summary>
        Price_ASC = 3,
        /// <summary>
        /// 订单实际支付价格降序
        /// </summary>
        Price_DESC = 4,
        /// <summary>
        /// 下单时间升序
        /// </summary>
        DateCreated_ASC = 5,
        /// <summary>
        /// 下单时间降序
        /// </summary>
        DateCreated_DESC = 6,
        /// <summary>
        /// 订单号升序
        /// </summary>
        OrderNumber_ASC = 7,
        /// <summary>
        /// 订单号降序
        /// </summary>
        OrderNumber_DESC = 8
    }
}
