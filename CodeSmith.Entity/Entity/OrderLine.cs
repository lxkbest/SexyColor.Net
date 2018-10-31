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
    /// Description:订单行信息
    /// </summary>
    [SugarMapping(TableName = "sc_order_line")]
    [Serializable]
    public class OrderLine
    {
		#region	构造
		public OrderLine(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static OrderLine New()
        {
                
            OrderLine orderLine = new OrderLine();
            orderLine.Orderlineid = 0;
            orderLine.Orderid = 0;
            orderLine.Userid = 0;
            orderLine.Goodsid = 0;
            orderLine.GoodsName = string.Empty;
            orderLine.Skuid = 0;
            orderLine.BuyCount = 0;
            orderLine.GoodsLinePrice = decimal.Zero;
            orderLine.RealLinePrice = decimal.Zero;
            orderLine.RefundLinePrice = decimal.Zero;
            orderLine.SaveLinePrice = decimal.Zero;
            orderLine.SaveLineRealPrice = decimal.Zero;
            orderLine.IsComment = false;
            orderLine.DeteCreated = DateTime.UtcNow;
            return orderLine;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 订单行标识
        /// </summary>
		[SugarMapping(ColumnName = "orderlineid")]
        public long Orderlineid { get; set; }
 
        /// <summary>
        /// 订单Id
        /// </summary>
		[SugarMapping(ColumnName = "orderid")]
        public long Orderid { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 商品Id
        /// </summary>
		[SugarMapping(ColumnName = "goodsid")]
        public long Goodsid { get; set; }
 
        /// <summary>
        /// 商品名称
        /// </summary>
		[SugarMapping(ColumnName = "goods_name")]
        public string GoodsName { get; set; }
 
        /// <summary>
        /// 商品规格Id
        /// </summary>
		[SugarMapping(ColumnName = "skuid")]
        public long Skuid { get; set; }
 
        /// <summary>
        /// 购买数量
        /// </summary>
		[SugarMapping(ColumnName = "buy_count")]
        public int BuyCount { get; set; }
 
        /// <summary>
        /// 商品价格
        /// </summary>
		[SugarMapping(ColumnName = "goods_line_price")]
        public decimal GoodsLinePrice { get; set; }
 
        /// <summary>
        /// 订单行总金额   数量*单价
        /// </summary>
		[SugarMapping(ColumnName = "real_line_price")]
        public decimal RealLinePrice { get; set; }
 
        /// <summary>
        /// 订单行总退款金额
        /// </summary>
		[SugarMapping(ColumnName = "refund_line_price")]
        public decimal RefundLinePrice { get; set; }
 
        /// <summary>
        /// 订单行总优惠价格
        /// </summary>
		[SugarMapping(ColumnName = "save_line_price")]
        public decimal SaveLinePrice { get; set; }
 
        /// <summary>
        /// 优惠后实付金额总价格
        /// </summary>
		[SugarMapping(ColumnName = "save_line_real_price")]
        public decimal SaveLineRealPrice { get; set; }
 
        /// <summary>
        /// 是否评价 0.否，1.是   需要根据业务需求 是否是每个行都评价还是一笔订单评价。产品经理决定   所以订单和订单行都设计有此字段，默认为1，已评价
        /// </summary>
		[SugarMapping(ColumnName = "is_comment")]
        public bool IsComment { get; set; }
 
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
