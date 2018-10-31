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
    /// Description:标签用户关联
    /// </summary>
    [SugarMapping(TableName = "sc_tag_in_groups")]
    [Serializable]
    public class TagInGroups
    {
		#region	构造
		public TagInGroups(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static TagInGroups New()
        {
                
            TagInGroups tagInGroups = new TagInGroups();
            tagInGroups.Id = 0;
            tagInGroups.Tagid = 0;
            tagInGroups.Userid = 0;
            return tagInGroups;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 标签用户关联
        /// </summary>
		[SugarMapping(ColumnName = "id")]
        public long Id { get; set; }
 
        /// <summary>
        /// 标签Id
        /// </summary>
		[SugarMapping(ColumnName = "tagid")]
        public long Tagid { get; set; }
 
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
