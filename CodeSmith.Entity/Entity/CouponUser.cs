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
    /// Description:分配用户优惠劵
    /// </summary>
    [SugarMapping(TableName = "sc_coupon_user")]
    [Serializable]
    public class CouponUser
    {
		#region	构造
		public CouponUser(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static CouponUser New()
        {
                
            CouponUser couponUser = new CouponUser();
            couponUser.CouponUserId = 0;
            couponUser.Couponid = 0;
            couponUser.Userid = 0;
            couponUser.CouponCode = string.Empty;
            couponUser.IsUse = false;
            couponUser.Status = false;
            couponUser.StartTime = DateTime.UtcNow;
            couponUser.EndTime = DateTime.UtcNow;
            couponUser.DeteCreated = DateTime.UtcNow;
            couponUser.Remark = string.Empty;
            return couponUser;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户优惠劵Id
        /// </summary>
		[SugarMapping(ColumnName = "coupon_user_id")]
        public long CouponUserId { get; set; }
 
        /// <summary>
        /// 优惠劵Id
        /// </summary>
		[SugarMapping(ColumnName = "couponid")]
        public long Couponid { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 优惠劵兑换码    简单的数字就好。4位
        /// </summary>
		[SugarMapping(ColumnName = "coupon_code")]
        public string CouponCode { get; set; }
 
        /// <summary>
        /// 是否可用   0.否，1.是
        /// </summary>
		[SugarMapping(ColumnName = "is_use")]
        public bool IsUse { get; set; }
 
        /// <summary>
        /// 状态  1.未使用，2.已过期，3,不可用，4.已使用
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }
 
        /// <summary>
        /// 开始时间
        /// </summary>
		[SugarMapping(ColumnName = "start_time")]
        public System.DateTime StartTime { get; set; }
 
        /// <summary>
        /// 结束时间
        /// </summary>
		[SugarMapping(ColumnName = "end_time")]
        public System.DateTime EndTime { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "dete_created")]
        public System.DateTime DeteCreated { get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
		[SugarMapping(ColumnName = "remark")]
        public string Remark { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
