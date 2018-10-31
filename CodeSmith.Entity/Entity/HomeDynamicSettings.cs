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
    /// Description:首页动态设置
    /// </summary>
    [SugarMapping(TableName = "sc_home_dynamic_settings")]
    [Serializable]
    public class HomeDynamicSettings
    {
		#region	构造
		public HomeDynamicSettings(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static HomeDynamicSettings New()
        {
                
            HomeDynamicSettings homeDynamicSettings = new HomeDynamicSettings();
            homeDynamicSettings.Id = 0;
            homeDynamicSettings.Type = false;
            homeDynamicSettings.ImageUrl = string.Empty;
            homeDynamicSettings.LabelText = string.Empty;
            homeDynamicSettings.Userid = 0;
            homeDynamicSettings.DateCreated = DateTime.UtcNow;
            homeDynamicSettings.DateLastModified = DateTime.UtcNow;
            return homeDynamicSettings;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 首页动态设置标识
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 展位类型  1.轮换，2分类，3，其他
        /// </summary>
		[SugarMapping(ColumnName = "type")]
        public bool Type { get; set; }
 
        /// <summary>
        /// 图片url
        /// </summary>
		[SugarMapping(ColumnName = "image_url")]
        public string ImageUrl { get; set; }
 
        /// <summary>
        /// 文本文字
        /// </summary>
		[SugarMapping(ColumnName = "label_text")]
        public string LabelText { get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
        /// <summary>
        /// 最后更新时间
        /// </summary>
		[SugarMapping(ColumnName = "date_last_modified")]
        public System.DateTime DateLastModified { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
