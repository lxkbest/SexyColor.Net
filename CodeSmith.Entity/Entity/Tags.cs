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
    /// Description:标签
    /// </summary>
    [SugarMapping(TableName = "sc_tags")]
    [Serializable]
    public class Tags
    {
		#region	构造
		public Tags(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Tags New()
        {
                
            Tags tags = new Tags();
            tags.Tagid = 0;
            tags.TagName = string.Empty;
            tags.TagType = false;
            tags.DateCreated = DateTime.UtcNow;
            return tags;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 标签Id
        /// </summary>
		[SugarMapping(ColumnName = "tagid")]
        public long Tagid { get; set; }
 
        /// <summary>
        /// 标签名称
        /// </summary>
		[SugarMapping(ColumnName = "tag_name")]
        public string TagName { get; set; }
 
        /// <summary>
        /// 标签类型   （枚举设定）
        /// </summary>
		[SugarMapping(ColumnName = "tag_type")]
        public bool TagType { get; set; }
 
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
