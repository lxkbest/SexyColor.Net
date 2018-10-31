using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SexyColor.BusinessComponents
{
    public static class UserExtension
    {
        /// <summary>
        /// 判断用户是否是超级管理员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsSuperAdministrator(this User user)
        {
            RolesService roleService = DIContainer.Resolve<RolesService>();
            return roleService.IsUserInRoles(user.UserId, RoleNames.Instance().SuperAdministrator());
        }

        /// <summary>
        /// 判断用户是否是内容管理员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsContentAdministrator(this User user)
        {
            RolesService roleService = DIContainer.Resolve<RolesService>();
            return roleService.IsUserInRoles(user.UserId, RoleNames.Instance().ContentAdministrator());
        }

        /// <summary>
        /// 获取用户拥有的角色集合
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="isPublic">是否仅获取对外公开的角色</param>
        /// <returns></returns>
        public static IEnumerable<string> GetUserInRoles(this User user, bool isPublic = false)
        {
            RolesService rolesService = DIContainer.Resolve<RolesService>();
            return rolesService.GetRolesNamesInUser(user.UserId, isPublic);
        }


        /// <summary>
        /// 判断用户是否至少含有一个角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="requiredRoleNames">待检测用户角色集合</param>
        /// <returns></returns>
        public static bool IsInRoles(this User user, params string[] requiredRoleNames)
        {
            if (user == null)
                return false;

            RolesService rolesService = DIContainer.Resolve<RolesService>();
            return rolesService.IsUserInRoles(user.UserId, requiredRoleNames);
        }

        /// <summary>
        /// 判断是否可以登录后台
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsAllowEntryManager(this User user)
        {
            RolesService rolesService = DIContainer.Resolve<RolesService>();
            IEnumerable<Roles> roleNames =  rolesService.GetRolesByConnectToUser(true);
            foreach (ApplicationidEnum item in Enum.GetValues(typeof(ApplicationidEnum)))
            {
                IEnumerable<Roles> list = roleNames.Where(w => w.Applicationid == (int)item);
                if(list != null && list.Count() > 0)
                    ApplicationRoleNames.Add((int)item, list.Select(w => w.Rolename));
            }
            if (user.IsSuperAdministrator() || user.IsContentAdministrator())
                return true;
            return user.IsInRoles(ApplicationRoleNames.GetAll().ToArray());
        }
    }
}
