using MySqlSugar;
using SexyColor.Infrastructure;
using System;


namespace SexyColor.CommonComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:用户基本资料
    /// </summary>
    [CacheSetting(true, ExpirationPolicy = CachingExpirationType.UsualSingleObject)]
    [SugarMapping(TableName = "sc_user_profiles")]
    [Serializable]
    public class UserProfiles : IEntity
    {
        #region	构造
        public UserProfiles()
        {

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
            userProfiles.Sex = -1;
            userProfiles.Age = 0;
            userProfiles.Birthday = DateTime.UtcNow;
            userProfiles.SexualOrientation = -1;
            userProfiles.IsSexualorientationSecrecy = -1;
            userProfiles.Marriage = -1;
            userProfiles.IsMarriageSecrecy = -1;
            userProfiles.Provinces = string.Empty;
            userProfiles.City = string.Empty;
            userProfiles.IsNowareaSecrecy = -1;
            userProfiles.Integrity = 0;
            userProfiles.Propertynames = string.Empty;
            userProfiles.Propertyvalues = string.Empty;
            return userProfiles;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 用户Id
        /// </summary>
        //[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }

        /// <summary>
        /// 性别 0=男、1=女、-1:未填
        /// </summary>
        [SugarMapping(ColumnName = "sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 年龄 
        /// </summary>
        [SugarMapping(ColumnName = "age")]
        public int Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [SugarMapping(ColumnName = "birthday")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性取向 1:男性、2:女性、3:双性、-1:未填
        /// </summary>
        [SugarMapping(ColumnName = "sexual_orientation")]
        public int SexualOrientation { get; set; }

        /// <summary>
        /// 性取向是否保密 0=否、1=是、-1:未填
        /// </summary>
        [SugarMapping(ColumnName = "is_sexualorientation_secrecy")]
        public int IsSexualorientationSecrecy { get; set; }

        /// <summary>
        /// 婚姻状况 0=未婚、1=已婚、-1:未填
        /// </summary>
        [SugarMapping(ColumnName = "marriage")]
        public int Marriage { get; set; }

        /// <summary>
        /// 婚姻状况是否保密 0=否、1=是、-1:未填
        /// </summary>
        [SugarMapping(ColumnName = "is_marriage_secrecy")]
        public int IsMarriageSecrecy { get; set; }

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
        /// 所在地是否保密 0=否、1=是、-1:未填
        /// </summary>
        [SugarMapping(ColumnName = "is_nowarea_secrecy")]
        public int IsNowareaSecrecy { get; set; }

        /// <summary>
        /// 资料完整度
        /// </summary>
        [SugarMapping(ColumnName = "integrity")]
        public int Integrity { get; set; }

        /// <summary>
        /// 可序列化属性名称
        /// </summary>
        public string Propertynames { get; set; }

        /// <summary>
        /// 可序列化属性内容
        /// </summary>
        public string Propertyvalues { get; set; }



        #endregion

        #region 扩展属性
        public object EntityId { get => Userid; }

        public bool IsSex
        {
            get
            {
                if (Sex >= 0)
                    return true;
                else
                    return false;
            }
        }

        public bool IsAge
        {
            get
            {
                if (Age > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool IsArea
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Provinces) && !string.IsNullOrWhiteSpace(City))
                    return true;
                else
                    return false;
            }
        }

        public bool IsSexualOrientation
        {
            get
            {
                if (SexualOrientation > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool IsMarriage
        {
            get
            {
                if (Marriage >= 0)
                    return true;
                else
                    return false;
            }
        }




        #endregion
    }
}
