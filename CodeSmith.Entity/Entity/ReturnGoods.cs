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
    /// Description:退换货物申请
    /// </summary>
    [SugarMapping(TableName = "sc_return_goods")]
    [Serializable]
    public class ReturnGoods
    {
		#region	构造
		public ReturnGoods(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static ReturnGoods New()
        {
                
            ReturnGoods returnGoods = new ReturnGoods();
            returnGoods.Id = 0;
            returnGoods.Userid = 0;
            returnGoods.Orderid = 0;
            returnGoods.Status = false;
            returnGoods.Type = false;
            returnGoods.Description = string.Empty;
            returnGoods.DeteCreated = DateTime.UtcNow;
            returnGoods.ReturnImageUrl1 = string.Empty;
            returnGoods.ReturnImageUrl2 = string.Empty;
            returnGoods.ReturnImageUrl3 = string.Empty;
            returnGoods.ReturnImageUrl4 = string.Empty;
            return returnGoods;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 退换货物标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public int Id { get; set; }
 
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
        /// 状态   1.未处理，2.已处理
        /// </summary>
		[SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }
 
        /// <summary>
        /// 退货申请类型  1.与描述不符，2.更换货物，3.货物损坏
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public bool Type { get; set; }
 
        /// <summary>
        /// 退货描述
        /// </summary>
		[SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "dete_created")]
        public System.DateTime DeteCreated { get; set; }
 
        /// <summary>
        /// 截图图片1
        /// </summary>
		[SugarMapping(ColumnName = "return_image_url1")]
        public string ReturnImageUrl1 { get; set; }
 
        /// <summary>
        /// 截图图片2
        /// </summary>
		[SugarMapping(ColumnName = "return_image_url2")]
        public string ReturnImageUrl2 { get; set; }
 
        /// <summary>
        /// 截图图片3
        /// </summary>
		[SugarMapping(ColumnName = "return_image_url3")]
        public string ReturnImageUrl3 { get; set; }
 
        /// <summary>
        /// 截图图片4
        /// </summary>
		[SugarMapping(ColumnName = "return_image_url4")]
        public string ReturnImageUrl4 { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
