using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IPermissionItemsInRolesRepository : IRepository<PermissionItemsInRoles>
    {
        /// <summary>
        /// 根据角色名称获取权限项
        /// </summary>
        /// <param name="rolesName">角色名</param>
        /// <returns></returns>
        IEnumerable<PermissionItemsInRoles> GetPermissionItemsInRolesByRolesname(string rolesName);

        /// <summary>
        /// 根据角色名称删除角色所拥有的权限项
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rolesName">角色名</param>
        /// <returns></returns>
        bool DeletePermissionItemsInRolesByRolesname(List<PermissionItemsInRoles> list, string rolesName);

        /// <summary>
        ///  根据权限项删除所有角色所拥有的该权限项
        /// </summary>
        /// <param name="itemKey">权限项</param>
        /// <param name="strRoles">角色名集合</param>
        /// <returns></returns>
        void DeletePermissionItemsInRolesByItemkey(string itemKey, IEnumerable<string> strRoles);

        /// <summary>
        /// 给角色添加一组权限
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="strPermissionItems">权限项组</param>
        /// <returns></returns>
        void AddRolesToPermissionItems(string roleName, string[] strPermissionItems);
    }
}
