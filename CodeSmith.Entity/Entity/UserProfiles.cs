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
    /// Description:用户基本资料
    /// </summary>
    [SugarMapping(TableName = "sc_user_profiles")]
    [Serializable]
    public class UserProfiles
    {
		#region	构造
		public UserProfiles(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static UserProfiles New()
        {
                
            UserProfiles userProfiles = new UserProfiles();
            userProfiles.Userid = 0;
            userProfiles.Sex = false;
            userProfiles.Age = 0;
            userProfiles.Birthday = DateTime.UtcNow;
            userProfiles.SexualOrientation = false;
            userProfiles.IsSexualorientationSecrecy = false;
            userProfiles.Marriage = false;
            userProfiles.IsMarriageSecrecy = false;
            userProfiles.Provinces = string.Empty;
            userProfiles.City = string.Empty;
            userProfiles.IsNowareaSecrecy = false;
            userProfiles.Integrity = false;
            userProfiles.Propertynames = string.Empty;
            userProfiles.Propertyvalues = string.Empty;
            return userProfiles;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 性别 0=男、1=女
        /// </summary>
		[SugarMapping(ColumnName = "sex")]
        public bool Sex { get; set; }
 
        /// <summary>
        /// 年龄 
        /// </summary>
		[SugarMapping(ColumnName = "age")]
        public int Age { get; set; }
 
        /// <summary>
        /// 生日
        /// </summary>
		[SugarMapping(ColumnName = "birthday")]
        public System.DateTime Birthday { get; set; }
 
        /// <summary>
        /// 性取向 1:男性、2:女性、3:双性
        /// </summary>
		[SugarMapping(ColumnName = "sexual_orientation")]
        public bool SexualOrientation { get; set; }
 
        /// <summary>
        /// 性取向是否保密 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_sexualorientation_secrecy")]
        public bool IsSexualorientationSecrecy { get; set; }
 
        /// <summary>
        /// 婚姻状况 0=未婚、1=已婚
        /// </summary>
		[SugarMapping(ColumnName = "marriage")]
        public bool Marriage { get; set; }
 
        /// <summary>
        /// 婚姻状况是否保密 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_marriage_secrecy")]
        public bool IsMarriageSecrecy { get; set; }
 
        /// <summary>
        /// 所属省
        /// </summary>
		[SugarMapping(ColumnName = "provinces")]
        public string Provinces { get; set; }
 
        /// <summary>
        /// 所属市
        /// </summary>
		[SugarMapping(ColumnName = "city")]
        public string City { get; set; }
 
        /// <summary>
        /// 所在地是否保密 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_nowarea_secrecy")]
        public bool IsNowareaSecrecy { get; set; }
 
        /// <summary>
        /// 资料完整度
        /// </summary>
		[SugarMapping(ColumnName = "integrity")]
        public bool Integrity { get; set; }
 
        /// <summary>
        /// 可序列化属性名称
        /// </summary>
		[SugarMapping(ColumnName = "propertynames")]
        public string Propertynames { get; set; }
 
        /// <summary>
        /// 可序列化属性内容
        /// </summary>
		[SugarMapping(ColumnName = "propertyvalues")]
        public string Propertyvalues { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
