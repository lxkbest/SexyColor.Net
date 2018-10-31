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
    /// Description:优惠劵
    /// </summary>
    [SugarMapping(TableName = "sc_coupon")]
    [Serializable]
    public class Coupon
    {
		#region	构造
		public Coupon(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Coupon New()
        {
                
            Coupon coupon = new Coupon();
            coupon.Couponid = 0;
            coupon.Type = false;
            coupon.IsShare = false;
            coupon.StyleName = string.Empty;
            coupon.StyleContent = string.Empty;
            coupon.IsBuy = false;
            coupon.BuyIntegral = 0;
            coupon.CouponCount = 0;
            coupon.UseCount = 0;
            coupon.SendCount = 0;
            coupon.AttainMoney = decimal.Zero;
            coupon.ConditionStyle = false;
            coupon.SubMoney = decimal.Zero;
            coupon.IsFree = false;
            coupon.GivePoint = 0;
            coupon.GiveCustom = string.Empty;
            coupon.Userid = 0;
            coupon.DeteCreated = DateTime.UtcNow;
            return coupon;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 优惠劵Id
        /// </summary>
		[SugarMapping(ColumnName = "couponid")]
        public long Couponid { get; set; }
 
        /// <summary>
        /// 优惠劵类型   1.满减劵 ，2.代金劵，3.折扣劵
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public bool Type { get; set; }
 
        /// <summary>
        /// 是否可以分享  0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_share")]
        public bool IsShare { get; set; }
 
        /// <summary>
        /// 优惠劵名称
        /// </summary>
		[SugarMapping(ColumnName = "style_name")]
        public string StyleName { get; set; }
 
        /// <summary>
        /// 优惠劵内容
        /// </summary>
		[SugarMapping(ColumnName = "style_content")]
        public string StyleContent { get; set; }
 
        /// <summary>
        /// 是否可以购买   0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_buy")]
        public bool IsBuy { get; set; }
 
        /// <summary>
        /// 使用多少积分购买   默认0  0表示不能购买
        /// </summary>
		[SugarMapping(ColumnName = "buy_integral")]
        public int BuyIntegral { get; set; }
 
        /// <summary>
        /// 发行数量
        /// </summary>
		[SugarMapping(ColumnName = "coupon_count")]
        public int CouponCount { get; set; }
 
        /// <summary>
        /// 用户已使用数量
        /// </summary>
		[SugarMapping(ColumnName = "use_count")]
        public int UseCount { get; set; }
 
        /// <summary>
        /// 已派发用户数量
        /// </summary>
		[SugarMapping(ColumnName = "send_count")]
        public int SendCount { get; set; }
 
        /// <summary>
        /// 达到满足条件金额
        /// </summary>
		[SugarMapping(ColumnName = "attain_money")]
        public decimal AttainMoney { get; set; }
 
        /// <summary>
        /// 满足条件选择方式    1.满减，2.满免运，3.满送积分，4.满送赠品
        /// </summary>
		[SugarMapping(ColumnName = "condition_style")]
        public bool ConditionStyle { get; set; }
 
        /// <summary>
        /// 满足条件减免金额
        /// </summary>
		[SugarMapping(ColumnName = "sub_money")]
        public decimal SubMoney { get; set; }
 
        /// <summary>
        /// 满足条件免邮
        /// </summary>
		[SugarMapping(ColumnName = "is_free")]
        public bool IsFree { get; set; }
 
        /// <summary>
        /// 满足条件赠送积分
        /// </summary>
		[SugarMapping(ColumnName = "give_point")]
        public int GivePoint { get; set; }
 
        /// <summary>
        /// 满足条件赠送自定义
        /// </summary>
		[SugarMapping(ColumnName = "give_custom")]
        public string GiveCustom { get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
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
