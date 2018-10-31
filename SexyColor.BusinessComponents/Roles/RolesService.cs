using MySqlSugar;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SexyColor.BusinessComponents
{
    public class RolesService
    {
        public IRolesRepository rolesRepository { get; set; }
        public IUsersInRolesRepository userInRolesRepository { get; set; }

        /// <summary>
        /// 获取角色集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Roles> GetRoles()
        {
            return rolesRepository.GetAllByCache<string>("is_builtin desc, rolename", i => i.Rolename);
        }

        /// <summary>
        /// 获取角色集合(根据角色名集合)
        /// </summary>
        /// <param name="roleNames">角色名集合</param>
        /// <returns></returns>
        public IEnumerable<Roles> GetRoles(IEnumerable<string> roleNames)
        {
            return rolesRepository.CacheByEntityIds(roleNames);
        }

        /// <summary>
        /// 根据筛选条件 （获取分页组件）
        /// </summary>
        /// <param name="userQuery">筛选条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        public PagingDataSet<Roles> GetRoles(RolesQuery rolesQuery, int pageIndex, int pageSize)
        {
            return rolesRepository.GetRoles(rolesQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取角色名集合(根据用户编号)
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="isPublic">是否仅获取对外公开的角色</param>
        /// <returns></returns>
        public IEnumerable<string> GetRolesNamesInUser(long userId, bool isPublic = false)
        {
            return userInRolesRepository.GetRolesNamesInUser(userId, isPublic);
        }

        /// <summary>
        /// 移除用户的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        public void RemoveRolesInUser(long userId)
        {
            userInRolesRepository.RemoveRolesInUser(userId);
        }

        /// <summary>
        /// 为用户添加一组角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="strRoles">一组角色名称</param>
        public void AddUserToRoles(long userId, List<string> strRoles)
        {
            if (userId <= 0 || strRoles == null)
                return;
            userInRolesRepository.AddUserToRoles(userId, strRoles);
        }

        /// <summary>
        /// 用户角色-批量处理启用，取消启用操作
        /// </summary>
        /// <param name="userIds">用户编号集合</param>
        /// <param name="isActivated">是否启用</param>
        /// <returns></returns>
        public void EnabledRole(IEnumerable<string> userIds, bool isActivated)
        {
            foreach (var userId in userIds)
            {
                Roles role = rolesRepository.GetRole(userId);
                if (role == null) continue;

                if (role.IsEnabled == isActivated) continue;

                role.IsEnabled = isActivated;

                rolesRepository.UpdateCache(role);
            }
        }

        /// <summary>
        /// 用户角色-批量公开用户
        /// </summary>
        /// <param name="isActivated">公开/取消公开</param>
        /// <returns></returns>
        public void IsPublic(IEnumerable<string> userIds, bool isActivated)
        {
            foreach (var userId in userIds)
            {
                Roles role = rolesRepository.GetRole(userId);
                if (role == null) continue;

                if (role.IsPublic == isActivated) continue;

                role.IsPublic = isActivated;

                rolesRepository.UpdateCache(role);
            }
        }

        /// <summary>
        /// 获取角色实体
        /// </summary>
        /// <param name="roleNmae"></param>
        /// <returns></returns>
        public Roles GetFullRoles(string roleNmae)
        {
            return rolesRepository.GetRoles(roleNmae);
        }

        /// <summary>
        /// 编辑用户角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditRoles(Roles entity)
        {
            return rolesRepository.UpdateCache(entity);
        }

        ///// <summary>
        ///// 删除用户角色
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        public bool DeleteRoles(string roleName)
        {
            Roles role = rolesRepository.GetRole(roleName);
            var result = rolesRepository.DeleteCache(role);
            return result;
        }

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddRoles(Roles entity)
        {
            return rolesRepository.AddRoles(entity);
        }

        /// <summary>
        ///  判断用户是否至少拥有一个用户角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roleNames">用户角色集合</param>
        /// <returns></returns>
        public bool IsUserInRoles(long userId, params string[] roleNames)
        {
            IEnumerable<string> userRoleNames = GetRolesNamesInUser(userId);
            return userRoleNames.Any(w => roleNames.Contains(w));
        }

        /// <summary>
        /// 获取角色集合(根据是否系统内置并且是否关联到用户)选出管理用户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Roles> GetRolesByConnectToUser(bool connectToUser)
        {
            return rolesRepository.GetRolesByConnectToUser(connectToUser);
        }
    }
}
