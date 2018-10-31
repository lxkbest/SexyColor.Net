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
    /// Description:广告位
    /// </summary>
    [SugarMapping(TableName = "sc_advertising_position")]
    [Serializable]
    public class AdvertisingPosition
    {
		#region	构造
		public AdvertisingPosition(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static AdvertisingPosition New()
        {
                
            AdvertisingPosition advertisingPosition = new AdvertisingPosition();
            advertisingPosition.Positionid = 0;
            advertisingPosition.PresentArea = string.Empty;
            advertisingPosition.Description = string.Empty;
            advertisingPosition.ImageFeatured = string.Empty;
            advertisingPosition.Width = 0;
            advertisingPosition.Height = 0;
            advertisingPosition.IsEnable = false;
            return advertisingPosition;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 广告位Id
        /// </summary>
		[SugarMapping(ColumnName = "positionid")]
        public int Positionid { get; set; }
 
        /// <summary>
        /// 投放区域
        /// </summary>
		[SugarMapping(ColumnName = "present_area")]
        public string PresentArea { get; set; }
 
        /// <summary>
        /// 描述
        /// </summary>
		[SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
 
        /// <summary>
        /// 示意图
        /// </summary>
		[SugarMapping(ColumnName = "image_featured")]
        public string ImageFeatured { get; set; }
 
        /// <summary>
        /// 宽度
        /// </summary>
		[SugarMapping(ColumnName = "width")]
        public int Width { get; set; }
 
        /// <summary>
        /// 高度
        /// </summary>
		[SugarMapping(ColumnName = "height")]
        public int Height { get; set; }
 
        /// <summary>
        /// 是否启用  0=否,1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_enable")]
        public bool IsEnable { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
