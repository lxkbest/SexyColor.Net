using SexyColor.Infrastructure;
using MySqlSugar;
using System.Linq;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public class UsersInRolesRepository : Repository<UsersInRoles>, IUsersInRolesRepository
    {
        /// <summary>
        /// 获取角色名集合(根据用户编号)
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="isPublic">是否仅获取对外公开的角色</param>
        /// <returns></returns>
        public IEnumerable<string> GetRolesNamesInUser(long userId, bool isPublic = false)
        {
            string cacheKeyUserInRole = RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "UserId", userId) + isPublic;
            List<string> roleNames = cacheService.Get<List<string>>(cacheKeyUserInRole);
            if (roleNames == null)
            {
                using (var db = DbService.GetInstance())
                {
                    roleNames = db.Sqlable().From("sc_users_in_roles", "s")
                                 .Where("s.userid=@userid")
                                 .SelectToList<string>("s.rolename", new { userid = userId });
                }
                if(roleNames != null)
                    cacheService.Add(cacheKeyUserInRole, roleNames, CachingExpirationType.UsualObjectCollection);
            }
            return roleNames;
        }

        /// <summary>
        /// 移除用户所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        public void RemoveRolesInUser(long userId)
        {
            using (var db = DbService.GetInstance())
            {
                var result = db.ExecuteCommand("delete from sc_users_in_roles where userid = @userid", new { userid = userId });
                if (result > 0)
                    RealTimeCacheHelper.IncreaseAreaVersion("UserId", userId);
            }
        }

        /// <summary>
        /// 为用户添加一组角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="strRoles">一组角色名称</param>
        public void AddUserToRoles(long userId, List<string> strRoles)
        {
            UsersInRoles entity = new UsersInRoles();
            entity.Userid = userId;
            foreach (var roleName in strRoles)
            {
                entity.Rolename = roleName;
                base.AddByCache(entity);
            }
            RealTimeCacheHelper.IncreaseAreaVersion("UserId", userId);
            foreach (var roleName in strRoles)
            {
                RealTimeCacheHelper.IncreaseAreaVersion("RoleName", roleName);
            }
        }
    }
}
