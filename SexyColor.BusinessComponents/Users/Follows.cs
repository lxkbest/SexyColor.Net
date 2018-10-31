using System;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:关注用户
    /// </summary>
    //[SugarMapping(TableName = "sc_follows")]
    [Serializable]
    public class Follows : IEntity
    {
        #region	构造
        public Follows()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static Follows New()
        {
            Follows follows = new Follows();
            follows.Id = 0;
            follows.Userid = 0;
            follows.Followeduserid = 0;
            follows.isMutual = false;
            follows.isQuietly = false;
            follows.DateCreated = DateTime.UtcNow;
            return follows;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 关注用户标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 关注用户Id
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 被关注用户Id
        /// </summary>
        //[SugarMapping(ColumnName = "followed_userid")]
        public long Followeduserid { get; set; }

        /// <summary>
        /// 是否相互关注 0=否、1=是
        /// </summary>
        //[SugarMapping(ColumnName = "is_mutual")]
        public bool isMutual { get; set; }

        /// <summary>
        /// 是否悄悄关注 0=否、1=是
        /// </summary>
        ///[SugarMapping(ColumnName = "is_quietly")]
        public bool isQuietly { get; set; }

        /// <summary>
        /// 关注日期
        /// </summary>
        //[SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
