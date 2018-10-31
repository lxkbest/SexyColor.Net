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
    /// Description:用户帐号
    /// </summary>
    [SugarMapping(TableName = "sc_user")]
    [Serializable]
    public class User
    {
		#region	构造
		public User(){
		
		}
		#endregion
		
        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static User New()
        {
                
            User user = new User();
            user.Userid = 0;
            user.Openid = string.Empty;
            user.Username = string.Empty;
            user.Password = string.Empty;
            user.PasswordFormat = 0;
            user.PasswordQuestion = string.Empty;
            user.PasswordAnswer = string.Empty;
            user.AccountEmail = string.Empty;
            user.IsEmailVerified = false;
            user.AccountMobile = string.Empty;
            user.IsMobileVerified = false;
            user.Nickname = string.Empty;
            user.ForceLogin = false;
            user.IsActivated = false;
            user.DateCreated = DateTime.UtcNow;
            user.IpCreated = string.Empty;
            user.UserType = false;
            user.LastActivityTime = DateTime.UtcNow;
            user.LastAction = string.Empty;
            user.IpLastActivity = string.Empty;
            user.IsBanned = false;
            user.BanReason = string.Empty;
            user.BanDeadLine = DateTime.UtcNow;
            user.IsModerated = false;
            user.IsForceModerated = false;
            user.HeadImg = string.Empty;
            user.FollowedCount = 0;
            user.FollowerCount = 0;
            user.Rank = 0;
            user.SexyPoints = 0;
            user.FrozenSexyPoints = 0;
            user.IsAuthentication = false;
            return user;
        }
		#endregion
 
		#region	属性
        /// <summary>
        /// 用户Id
        /// </summary>
		[SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }
 
        /// <summary>
        /// 用户OpenId
        /// </summary>
		[SugarMapping(ColumnName = "openid")]
        public string Openid { get; set; }
 
        /// <summary>
        /// 用户名    创建唯一索引
        /// </summary>
		[SugarMapping(ColumnName = "username")]
        public string Username { get; set; }
 
        /// <summary>
        /// 密码
        /// </summary>
		[SugarMapping(ColumnName = "password")]
        public string Password { get; set; }
 
        /// <summary>
        /// 加密方式 1=Clear（明文）、2=标准MD5、3.其他
        /// </summary>
		[SugarMapping(ColumnName = "password_format")]
        public int PasswordFormat { get; set; }
 
        /// <summary>
        /// 密码问题
        /// </summary>
		[SugarMapping(ColumnName = "password_question")]
        public string PasswordQuestion { get; set; }
 
        /// <summary>
        /// 密码答案
        /// </summary>
		[SugarMapping(ColumnName = "password_answer")]
        public string PasswordAnswer { get; set; }
 
        /// <summary>
        /// 帐号邮箱    (增加索引)
        /// </summary>
		[SugarMapping(ColumnName = "account_email")]
        public string AccountEmail { get; set; }
 
        /// <summary>
        /// 帐号邮箱是否通过验证 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_email_verified")]
        public bool IsEmailVerified { get; set; }
 
        /// <summary>
        /// 手机号码  增加索引
        /// </summary>
		[SugarMapping(ColumnName = "account_mobile")]
        public string AccountMobile { get; set; }
 
        /// <summary>
        /// 帐号手机是否通过验证  0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_mobile_verified")]
        public bool IsMobileVerified { get; set; }
 
        /// <summary>
        /// 昵称
        /// </summary>
		[SugarMapping(ColumnName = "nickname")]
        public string Nickname { get; set; }
 
        /// <summary>
        /// 是否强制用户登录 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "force_login")]
        public bool ForceLogin { get; set; }
 
        /// <summary>
        /// 帐户是否激活 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_activated")]
        public bool IsActivated { get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
		[SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }
 
        /// <summary>
        /// 创建用户时的IP
        /// </summary>
		[SugarMapping(ColumnName = "ip_created")]
        public string IpCreated { get; set; }
 
        /// <summary>
        /// 用户类别 1=普通用户、2=管理用户
        /// </summary>
		[SugarMapping(ColumnName = "user_type")]
        public bool UserType { get; set; }
 
        /// <summary>
        /// 上次活动时间
        /// </summary>
		[SugarMapping(ColumnName = "last_activity_time")]
        public System.DateTime LastActivityTime { get; set; }
 
        /// <summary>
        /// 上次操作
        /// </summary>
		[SugarMapping(ColumnName = "last_action")]
        public string LastAction { get; set; }
 
        /// <summary>
        /// 上次活动时的IP
        /// </summary>
		[SugarMapping(ColumnName = "ip_last_activity")]
        public string IpLastActivity { get; set; }
 
        /// <summary>
        /// 是否封禁 0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_banned")]
        public bool IsBanned { get; set; }
 
        /// <summary>
        /// 封禁原因
        /// </summary>
		[SugarMapping(ColumnName = "ban_reason")]
        public string BanReason { get; set; }
 
        /// <summary>
        /// 封禁截止日期
        /// </summary>
		[SugarMapping(ColumnName = "ban_dead_line")]
        public System.DateTime BanDeadLine { get; set; }
 
        /// <summary>
        /// 用户是否被管制   0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_moderated")]
        public bool IsModerated { get; set; }
 
        /// <summary>
        /// 强制用户管制    0=否、1=是
        /// </summary>
		[SugarMapping(ColumnName = "is_force_moderated")]
        public bool IsForceModerated { get; set; }
 
        /// <summary>
        /// 头像
        /// </summary>
		[SugarMapping(ColumnName = "head_img")]
        public string HeadImg { get; set; }
 
        /// <summary>
        /// 关注用户数 增加索引
        /// </summary>
		[SugarMapping(ColumnName = "followed_count")]
        public int FollowedCount { get; set; }
 
        /// <summary>
        /// 粉丝数 增加索引
        /// </summary>
		[SugarMapping(ColumnName = "follower_count")]
        public int FollowerCount { get; set; }
 
        /// <summary>
        /// 用户等级
        /// </summary>
		[SugarMapping(ColumnName = "rank")]
        public int Rank { get; set; }
 
        /// <summary>
        /// 积分值
        /// </summary>
		[SugarMapping(ColumnName = "sexy_points")]
        public int SexyPoints { get; set; }
 
        /// <summary>
        /// 冻结的积分值
        /// </summary>
		[SugarMapping(ColumnName = "frozen_sexy_points")]
        public int FrozenSexyPoints { get; set; }
 
        /// <summary>
        /// 是否通过认证
        /// </summary>
		[SugarMapping(ColumnName = "is_authentication")]
        public bool IsAuthentication { get; set; }
 
		#endregion
		
		#region 扩展属性
        
        #endregion
    }
}
