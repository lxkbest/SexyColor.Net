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
    /// Description:商品关联轮换图片
    /// </summary>
    [SugarMapping(TableName = "sc_goods_in_rotation_image")]
    [Serializable]
    public class GoodsInRotationImage
    {
		#region	构造
		public GoodsInRotationImage(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsInRotationImage New()
        {
                
            GoodsInRotationImage goodsInRotationImage = new GoodsInRotationImage();
            goodsInRotationImage.Id = 0;
            goodsInRotationImage.Goodsid = 0;
            goodsInRotationImage.GoodsRotationImage = string.Empty;
            goodsInRotationImage.Number = 0;
            return goodsInRotationImage;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 商品关联
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 商品Id
        /// </summary>
		[SugarMapping(ColumnName = "goodsid")]
        public long Goodsid { get; set; }
 
        /// <summary>
        /// 商品轮换图
        /// </summary>
		[SugarMapping(ColumnName = "goods_rotation_image")]
        public string GoodsRotationImage { get; set; }
 
        /// <summary>
        /// 序号
        /// </summary>
		[SugarMapping(ColumnName = "number")]
        public int Number { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
