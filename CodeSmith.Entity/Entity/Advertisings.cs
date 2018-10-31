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
    /// Description:广告
    /// </summary>
    [SugarMapping(TableName = "sc_advertisings")]
    [Serializable]
    public class Advertisings
    {
		#region	构造
		public Advertisings(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Advertisings New()
        {
                
            Advertisings advertisings = new Advertisings();
            advertisings.Advertisingid = 0;
            advertisings.AdvertisingName = string.Empty;
            advertisings.AdvertisingType = false;
            advertisings.Body = string.Empty;
            advertisings.ImageUrl = string.Empty;
            advertisings.Url = string.Empty;
            advertisings.IsEnable = false;
            advertisings.StartDate = DateTime.UtcNow;
            advertisings.EndDate = DateTime.UtcNow;
            advertisings.DisplayOrder = 0;
            advertisings.DateCreated = DateTime.UtcNow;
            advertisings.LastModifiedDate = DateTime.UtcNow;
            advertisings.PositionCount = 0;
            return advertisings;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 广告Id
        /// </summary>
		[SugarMapping(ColumnName = "advertisingid")]
        public int Advertisingid { get; set; }
 
        /// <summary>
        /// 广告名称
        /// </summary>
		[SugarMapping(ColumnName = "advertising_name")]
        public string AdvertisingName { get; set; }
 
        /// <summary>
        /// 呈现方式   1.轮换图片，2,横幅图片，3.横向滚动文字，4.纵向滚动文字
        /// </summary>
		[SugarMapping(ColumnName = "advertising_type")]
        public bool AdvertisingType { get; set; }
 
        /// <summary>
        /// 广告内容
        /// </summary>
		[SugarMapping(ColumnName = "body")]
        public string Body { get; set; }
 
        /// <summary>
        /// 图片地址
        /// </summary>
		[SugarMapping(ColumnName = "image_url")]
        public string ImageUrl { get; set; }
 
        /// <summary>
        /// 链接地址
        /// </summary>
		[SugarMapping(ColumnName = "url")]
        public string Url { get; set; }
 
        /// <summary>
        /// 是否启用    0=否,1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_enable")]
        public bool IsEnable { get; set; }
 
        /// <summary>
        /// 开始时间
        /// </summary>
		[SugarMapping(ColumnName = "start_date")]
        public System.DateTime StartDate { get; set; }
 
        /// <summary>
        /// 结束时间
        /// </summary>
		[SugarMapping(ColumnName = "end_date")]
        public System.DateTime EndDate { get; set; }
 
        /// <summary>
        /// 排序序号
        /// </summary>
		[SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }
 
        /// <summary>
        /// 创建日期
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
        /// <summary>
        /// 修改时间
        /// </summary>
		[SugarMapping(ColumnName = "last_modified_date")]
        public System.DateTime LastModifiedDate { get; set; }
 
        /// <summary>
        /// 投放量
        /// </summary>
		[SugarMapping(ColumnName = "position_count")]
        public int PositionCount { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
