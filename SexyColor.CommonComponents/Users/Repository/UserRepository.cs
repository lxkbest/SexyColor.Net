
using System;
using System.Linq;
using System.Collections.Generic;
using MySqlSugar;
using SexyColor.Infrastructure;
using SexyColor.BusinessComponents;
using System.Text;

namespace SexyColor.CommonComponents
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        //private IUserService _userService = DIContainer.Resolve<IUserService>();
        //private IMembershipService _membershipService = DIContainer.Resolve<IMembershipService>();
        #region 公共方法

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="ignoreUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        /// <returns></returns>
        public User CreateUser(User user, bool ignoreUsername, out UserCreateStatus userCreateStatus)
        {
            userCreateStatus = UserCreateStatus.UnknownFailure;
            if (!ignoreUsername)
            {
                if ("*,$,#,%,^,@".Split(new char[] { ',', '，' }).Any(n => n.Equals(user.UserName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    //用户输入字段为禁用字段
                    userCreateStatus = UserCreateStatus.DisallowedUsername;
                    return null;
                }
            }
            if (GetUserIdByUserName(user.UserName) > 0)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return null;
            }

            if (GetUser(user.UserId) != null)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return null;
            }

            if (!string.IsNullOrEmpty(user.AccountEmail) && GetUserIdByEmail(user.AccountEmail) > 0)
            {
                userCreateStatus = UserCreateStatus.DuplicateEmailAddress;
                return null;
            }

            if (!string.IsNullOrEmpty(user.AccountMobile))
            {
                if (GetUserIdByMobile(user.AccountMobile) > 0)
                {
                    userCreateStatus = UserCreateStatus.DuplicateMobile;
                    return null;
                }
            }
            user.UserId = IdGenerator.Next();
            object result = base.AddByCache(user, false);
            bool value = false;
            bool.TryParse(result.ToString(), out value);
            if (value)
                userCreateStatus = UserCreateStatus.Created;
            return user;
        }

        /// <summary>
        /// 用户名验证
        /// </summary>
        /// <param name="userName">待创建的用户名</param>
        /// <param name="ignoreDisallowedUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        public void RegisterValidate(string userName, bool ignoreUsername, out UserCreateStatus userCreateStatus)
        {
            userCreateStatus = UserCreateStatus.UnknownFailure;
            if (!ignoreUsername)
            {
                if ("*,$,#,%,^,@".Split(new char[] { ',', '，' }).Any(n => n.Equals(userName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    //用户输入字段为禁用字段
                    userCreateStatus = UserCreateStatus.DisallowedUsername;
                    return;
                }
            }
            //判断用户名是否唯一
            if (GetUserIdByUserName(userName) > 0)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return;
            }
            if (GetUser(GetUserIdByUserName(userName)) != null)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return;
            }
            else
            {
                userCreateStatus = UserCreateStatus.Created;
                return;
            }
        }


        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user">更新缓存使用的实体对象</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public bool ResetPassword(User user, string newPassword)
        {
            if (user == null)
                return false;
            user.Password = newPassword;
            var result = base.UpdateByCache(user, new { password = newPassword }, w => w.UserId == user.UserId);
            return result;
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="user"></param>
        /// <param name="headImage"></param>
        /// <returns></returns>
        public bool SetHeadIimage(User user, string headImage)
        {
            if (user == null)
                return false;
            user.HeadImg = headImage;
            var result = base.UpdateByCache(user, new { head_img = headImage }, w => w.UserId == user.UserId);
            return result;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateCache(User entity)
        {
            return base.UpdateByCache(entity);
        }

        /// <summary>
        ///删除用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteCache(User entity)
        {
            return base.DeleteByCache(entity);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param> 
        public User GetUser(long userId)
        {
            return base.GetByCache(w => w.UserId == userId, userId);
        }
        /// <summary>
        /// 根据帐号邮箱获取用户
        /// </summary>
        /// <param name="accountEmail">帐号邮箱</param>
        /// <returns>用户Id</returns>
        public long GetUserIdByEmail(string accountEmail)
        {

            using (var db = DbService.GetInstance())
            {
                User user = db.Queryable<User>().Where(w => w.AccountEmail == accountEmail).FirstOrDefault();
                return user == null ? 0 : user.UserId;
            }

        }
        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="accountMobile">手机号</param>
        /// <returns>用户Id</returns>
        public long GetUserIdByMobile(string accountMobile)
        {

            using (var db = DbService.GetInstance())
            {
                List<User> list = db.SqlQuery<User>("select * from sc_user where account_mobile=@account_mobile", new { account_mobile = accountMobile });
                if (list != null && list.Count > 0)
                {
                    return list[0].UserId;
                }
                return 0;
            }

        }
        /// <summary>
        /// 获取用户id根据用户昵称
        /// </summary>
        /// <param name="nickName">用户昵称</param>
        /// <returns>用户id</returns>
        public long GetUserIdByNickName(string nickName)
        {
            using (var db = DbService.GetInstance())
            {
                User user = db.Queryable<User>().FirstOrDefault(w => w.NickName == nickName);
                return user == null ? 0 : user.UserId;
            }

        }
        /// <summary>
        /// 根据用户名获取用户Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户Id</returns>
        public long GetUserIdByUserName(string userName)
        {
            using (var db = DbService.GetInstance())
            {
                List<User> list = db.Sqlable().From("sc_user", "s")
               .Where("s.username=@username")
               .OrderBy("date_created")
               .SelectToList<User>("s.*", new { username = userName });
                if (list != null && list.Count > 0)
                {
                    return list[0].UserId;
                }
                return 0;
            }
        }

        public PagingDataSet<User> GetUsers(UserQuery userQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string whereSql1 = string.Empty;
            HandleQueryByString(userQuery, out whereSql);
            HandleQueryByParameter(userQuery, out whereSql1);

            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, "userid ASC", i => i.UserId);
        }
        /// <summary>
        /// 根据用户id集合获取用户集合（分页）
        /// </summary>
        /// <param name="ids">用户id集合</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        public PagingDataSet<User> GetUsers(IEnumerable<long> ids, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            StringBuilder builder = new StringBuilder();
            ids.Each(delegate (long id)
            {
                builder.Append(id + ",");
            });
            string whereStr = builder.ToString().TrimEnd(',');

            string whereSql = $"userid in ({ whereStr })";

            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, "userid ASC", i => i.UserId);
        }

        public void HandleQueryByString(UserQuery userQuery, out string sql)
        {
            sql = "1=1 ";
            if (userQuery.RoleName.IsNotNullAndWhiteSpace())
                sql += $"and userid in (select userid from sc_users_in_roles where rolename = \"{userQuery.RoleName}\") ";
            if (userQuery.AccountEmailFilter.IsNotNullAndWhiteSpace())
                sql += $"and account_email like \"%{userQuery.AccountEmailFilter}%\" ";
            if (userQuery.IsActivated.HasValue)
                sql += $"and is_activated = {userQuery.IsActivated} ";
            if (userQuery.IsBanned.HasValue)
                sql += $"and is_banned = {userQuery.IsBanned} ";
            if (userQuery.IsModerated.HasValue)
                sql += $"and is_moderated = {userQuery.IsModerated} ";

            if (userQuery.Keyword.IsNotNullAndWhiteSpace())
                sql += $"and (username like \"%{userQuery.Keyword}%\" or nickname like \"%{userQuery.Keyword}%\") ";
            

            if (userQuery.RegisterTimeLowerLimit.HasValue)
                sql += $"and date_created >= \"{userQuery.RegisterTimeLowerLimit.Value.ToUniversalTime()}\" ";
            if (userQuery.RegisterTimeUpperLimit.HasValue)
                sql += $"and date_created <= \"{userQuery.RegisterTimeUpperLimit.Value.AddDays(1).ToUniversalTime()}\" ";
 
            if (userQuery.UserRankLowerLimit.HasValue)
                sql += $"and rank >= {userQuery.UserRankLowerLimit} ";
 
            if (userQuery.UserRankUpperLimit.HasValue)
                sql += $"and rank <= {userQuery.UserRankUpperLimit} ";
        }

        public object HandleQueryByParameter(UserQuery userQuery, out string sql)
        {
            sql = "1=1";
            if (userQuery.AccountEmailFilter.IsNotNullAndWhiteSpace())
                sql += $"and account_email like %@account_email% ";
            if (userQuery.IsActivated.HasValue)
                sql += $"and is_activated = @is_activated ";
            if (userQuery.IsBanned.HasValue)
                sql += $"and is_banned = @is_banned ";
            if (userQuery.IsModerated.HasValue)
                sql += $"and is_moderated = @is_moderated ";

            if (userQuery.Keyword.IsNotNullAndWhiteSpace())
                sql += $"and username like %@username% or nickname like %@nickname% ";
            if (userQuery.RoleName.IsNotNullAndWhiteSpace())
                sql += $"and userid in (select userid from sc_users_in_roles where rolename = @rolename ";

            if (userQuery.RegisterTimeLowerLimit.HasValue)
                sql += $"and date_created >= @date_created_lower";
            if (userQuery.RegisterTimeUpperLimit.HasValue)
                sql += $"and date_created <= @date_created_upper ";

            if (userQuery.UserRankLowerLimit.HasValue)
                sql += $"and rank >= @rank_lower ";

            if (userQuery.UserRankUpperLimit.HasValue)
                sql += $"and rank <= @rank_upper ";
            return new
            {
                account_email = userQuery.AccountEmailFilter,
                is_activated = userQuery.IsActivated.HasValue ? 1 : 0,
                is_banned = userQuery.IsBanned.HasValue ? 1 : 0,
                is_moderated = userQuery.IsModerated.HasValue ? 1 : 0,
                username = userQuery.Keyword,
                nickname = userQuery.Keyword,
                rolename = userQuery.RoleName,
                date_created_lower = userQuery.RegisterTimeLowerLimit,
                date_created_upper = userQuery.RegisterTimeUpperLimit,
                rank_lower = userQuery.UserRankLowerLimit,
                rank_upper = userQuery.UserRankUpperLimit
            };
        }

        public Sqlable HandleQueryBySqlable(UserQuery userQuery)
        {
            var db = DbService.GetInstance();
            var sqlTable = db.Sqlable();
            if (userQuery.AccountEmailFilter.IsNotNullAndWhiteSpace())
                sqlTable = sqlTable.Where("account_email like %@AccountEmail%");
            if (userQuery.IsActivated.HasValue)
                sqlTable = sqlTable.Where("is_activated = @IsActivated");
            if (userQuery.IsBanned.HasValue)
                sqlTable = sqlTable.Where("is_banned = @IsBanned");
            if (userQuery.IsModerated.HasValue)
                sqlTable = sqlTable.Where("is_moderated = @IsModerated");
            if (userQuery.Keyword.IsNotNullAndWhiteSpace())
                sqlTable = sqlTable.Where("username like %@Keyword% or nickname like %@Keyword%");
            if (userQuery.RoleName.IsNotNullAndWhiteSpace())
                sqlTable = sqlTable.Where("userid in (select userid from sc_users_in_roles where rolename = @RoleName");
            if (userQuery.RegisterTimeLowerLimit.HasValue)
                sqlTable = sqlTable.Where("date_created >= @RegisterTimeLowerLimit");
            if (userQuery.RegisterTimeUpperLimit.HasValue)
                sqlTable = sqlTable.Where("date_created <= @RegisterTimeUpperLimit");
            if (userQuery.UserRankLowerLimit.HasValue)
                sqlTable = sqlTable.Where("rank >= @UserRankLowerLimit");
            if (userQuery.UserRankUpperLimit.HasValue)
                sqlTable = sqlTable.Where("rank <= @UserRankUpperLimit");
            return sqlTable;
        }

      
        #endregion
    }
}
