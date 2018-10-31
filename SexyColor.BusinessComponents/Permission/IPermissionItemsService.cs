using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IPermissionItemsService
    {
        /// <summary>
        /// 获取全部权限项
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionItems> GetPermissionItemsAll();

        /// <summary>
        /// 根据权限项ItemKey获取单个权限项
        /// </summary>
        /// <returns></returns>
        PermissionItems GetPermissionItemsByItemkey(string itemKey);

        /// <summary>
        /// 根据父级项Parentsid获取JsonString
        /// </summary>
        /// <param name="parentsid"></param>
        /// <returns></returns>
        string GetPermissionItemsByParentsid(string parentsid);

        /// <summary>
        /// 根据角色名称获取用户拥有权限项
        /// </summary>
        /// <param name="rolesName">角色名</param>
        /// <returns></returns>
        IEnumerable<PermissionItemsInRoles> GetPermissionItemsInRolesByRolesname(string rolesName);

        /// <summary>
        /// 根据角色名称集合获取权限项
        /// </summary>
        /// <param name="rolesNames">角色名集合</param>
        /// <returns></returns>
        IEnumerable<IEnumerable<PermissionItemsInRoles>> GetPermissionItemsInRolesByRolesname(IEnumerable<string> rolesNames);

        /// <summary>
        /// 合并权限项
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        IEnumerable<string> Merge(IEnumerable<IEnumerable<PermissionItemsInRoles>> list);

        /// <summary>
        /// 创建权限项
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        PermissionItems CreatePermissionItems(PermissionItems entity, out PermissionItemsCreateStatus status);

        /// <summary>
        /// 删除角色所拥有的权限项
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool DeleteRolesPermissionItems(string roleName);

        /// <summary>
        /// 编辑权限项
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool EditPermissionItems(PermissionItems entity, out PermissionItemsCreateStatus status);

        /// <summary>
        /// 删除权限项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeletePermissionItems(PermissionItems entity, IEnumerable<string> strRoles);

        /// <summary>
        /// 给角色添加一组权限
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="strPermissionItems">权限项组</param>
        /// <returns></returns>
        bool AddRolesToPermissionItems(string roleName, string[] strPermissionItems);
    }
}
