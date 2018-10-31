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
    /// Description:积分项目
    /// </summary>
    [SugarMapping(TableName = "sc_point_items")]
    [Serializable]
    public class PointItems
    {
		#region	构造
		public PointItems(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static PointItems New()
        {
                
            PointItems pointItems = new PointItems();
            pointItems.Itemskey = 0;
            pointItems.Itemsname = string.Empty;
            pointItems.Itemstyle = string.Empty;
            pointItems.Applicationid = 0;
            pointItems.SexyPoints = 0;
            pointItems.ExperiencePoints = 0;
            pointItems.DisplayOrder = 0;
            pointItems.Description = string.Empty;
            return pointItems;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 积分项目标识
        /// </summary>
		[SugarMapping(ColumnName = "itemskey")]
        public int Itemskey { get; set; }
 
        /// <summary>
        /// 积分项目名称
        /// </summary>
		[SugarMapping(ColumnName = "itemsname")]
        public string Itemsname { get; set; }
 
        /// <summary>
        /// 积分类型标识   
        /// </summary>
		[SugarMapping(ColumnName = "itemstyle")]
        public string Itemstyle { get; set; }
 
        /// <summary>
        /// 模块Id
        /// </summary>
		[SugarMapping(ColumnName = "applicationid")]
        public int Applicationid { get; set; }
 
        /// <summary>
        /// 积分值
        /// </summary>
		[SugarMapping(ColumnName = "sexy_points")]
        public int SexyPoints { get; set; }
 
        /// <summary>
        /// 经验值
        /// </summary>
		[SugarMapping(ColumnName = "experience_points")]
        public int ExperiencePoints { get; set; }
 
        /// <summary>
        /// 排序序号
        /// </summary>
		[SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }
 
        /// <summary>
        /// 描述
        /// </summary>
		[SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
