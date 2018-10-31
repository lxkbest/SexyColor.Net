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
    /// Description:优惠劵订单关联
    /// </summary>
    [SugarMapping(TableName = "sc_coupon_in_order")]
    [Serializable]
    public class CouponInOrder
    {
		#region	构造
		public CouponInOrder(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static CouponInOrder New()
        {
                
            CouponInOrder couponInOrder = new CouponInOrder();
            couponInOrder.Id = 0;
            couponInOrder.Orderid = 0;
            couponInOrder.CouponUserId = 0;
            couponInOrder.DateCreated = DateTime.UtcNow;
            return couponInOrder;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 优惠劵订单关联标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 订单Id
        /// </summary>
		[SugarMapping(ColumnName = "orderid")]
        public long Orderid { get; set; }
 
        /// <summary>
        /// 用户优惠劵Id
        /// </summary>
		[SugarMapping(ColumnName = "coupon_user_id")]
        public long CouponUserId { get; set; }
 
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
