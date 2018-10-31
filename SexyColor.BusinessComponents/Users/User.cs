using MySqlSugar;
using System;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// 用户
    /// </summary>
    [CacheSetting(true)]
    [SugarMapping(TableName = "sc_user")]
    [Serializable]
    public class User : IEntity
    {
        public User()
        {
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="username">用户名称</param>
        /// <param name="accountEmail">账户Email</param>
        /// <param name="accountMobile">账户手机号</param>
        public static User New(long userId, string username, string accountEmail, string accountMobile)
        {
            User user = New();
            user.UserId = userId;
            user.UserName = username;
            user.AccountEmail = accountEmail;
            user.AccountMobile = accountMobile;
            user.HeadImg = string.Empty;
            user.Rank = 1;
            return user;
        }

        /// <summary>
        /// 新建使用
        /// </summary>
        public static User New()
        {
            User user = new User();
            user.UserName = string.Empty;
            user.Password = string.Empty;
            user.PasswordQuestion = string.Empty;
            user.PasswordAnswer = string.Empty;
            user.AccountEmail = string.Empty;
            user.AccountMobile = string.Empty;
            user.NickName = string.Empty;
            user.DateCreated = DateTime.UtcNow;
            user.IpCreated = string.Empty;
            user.LastActivityTime = DateTime.UtcNow;
            user.LastAction = string.Empty;
            user.IpLastActivity = string.Empty;
            user.BanReason = string.Empty;
            user.BanDeadLine = DateTime.UtcNow;
            user.FollowedCount = 0;
            user.FollowerCount = 0;
            user.HeadImg = string.Empty;
            user.Rank = 1;
            return user;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarMapping(ColumnName = "userid")]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarMapping(ColumnName = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarMapping(ColumnName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// 加密方式
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
        /// 昵称
        /// </summary>
        [SugarMapping(ColumnName = "nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 帐号邮箱
        /// </summary>
        [SugarMapping(ColumnName = "account_email")]
        public string AccountEmail { get; set; }

        /// <summary>
        /// 帐号邮箱是否通过验证
        /// </summary>
        [SugarMapping(ColumnName = "is_email_verified")]
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarMapping(ColumnName = "account_mobile")]
        public string AccountMobile { get; set; }

        /// <summary>
        /// 帐号手机是否通过验证
        /// </summary>
        [SugarMapping(ColumnName = "is_mobile_verified")]
        public bool IsMobileVerified { get; set; }

        /// <summary>
        /// 是否强制用户登录
        /// </summary>
        [SugarMapping(ColumnName = "force_login")]
        public bool ForceLogin { get; set; }

        /// <summary>
        /// 帐户是否激活
        /// </summary>
        [SugarMapping(ColumnName = "is_activated")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 创建用户时的IP
        /// </summary>
        [SugarMapping(ColumnName = "ip_created")]
        public string IpCreated { get; set; }

        /// <summary>
        /// 用户类别
        /// </summary>
        [SugarMapping(ColumnName = "user_type")]
        public int UserType { get; set; }

        /// <summary>
        /// 上次活动时间
        /// </summary>
        [SugarMapping(ColumnName = "last_activity_time")]
        public DateTime LastActivityTime { get; set; }

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
        /// 是否封禁
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
        public DateTime BanDeadLine { get; set; }

        /// <summary>
        /// 用户是否被管制
        /// </summary>
        [SugarMapping(ColumnName = "is_moderated")]
        public bool IsModerated { get; set; }

        /// <summary>
        /// 强制用户管制（不会自动解除）
        /// </summary>
        [SugarMapping(ColumnName = "is_force_moderated")]
        public bool IsForceModerated { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [SugarMapping(ColumnName = "head_img")]
        public string HeadImg { get; set; }

        /// <summary>
        /// 关注用户数
        /// </summary>
        [SugarMapping(ColumnName = "followed_count")]
        public int FollowedCount { get; set; }

        /// <summary>
        /// 粉丝数
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

        public object EntityId
        {
            get { return this.UserId; }
        } 

        /// <summary>
        /// 判断是否有头像
        /// </summary>
        public bool IsHeadImg
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(HeadImg) && HeadImg.Length > 0) 
                    return true;
                else
                    return false;
            }
        }
    }
}

