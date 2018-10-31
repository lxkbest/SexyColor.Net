using SexyColor.Infrastructure;
using System.Collections.Generic;
using MySqlSugar;
using System;

namespace SexyColor.BusinessComponents
{
    public class PermissionItemsInRolesRepository : Repository<PermissionItemsInRoles>, IPermissionItemsInRolesRepository
    {
        /// <summary>
        /// 根据角色名称获取权限项
        /// </summary>
        /// <param name="rolesName">角色名</param>
        /// <returns></returns>
        public IEnumerable<PermissionItemsInRoles> GetPermissionItemsInRolesByRolesname(string rolesName)
        {
            string cacheKey = RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Rolename", rolesName);
            List<PermissionItemsInRoles> list = cacheService.Get<List<PermissionItemsInRoles>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Sqlable().From("sc_permission_items_in_roles", "s")
                                 .Where("s.rolename=@rolename")
                                 .SelectToList<PermissionItemsInRoles>("s.*", new { rolename = rolesName });
                }
                if(list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }

        /// <summary>
        /// 根据角色名称删除角色所拥有的权限项
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rolesName"></param>
        /// <returns></returns>
        public bool DeletePermissionItemsInRolesByRolesname(List<PermissionItemsInRoles> list, string rolesName)
        {
            var result = base.Delete(list);
            if (result)
                RealTimeCacheHelper.IncreaseAreaVersion("Rolename", rolesName);
            return result;
        }

        /// <summary>
        /// 根据权限项删除所有角色所拥有的该权限项
        /// </summary>
        /// <param name="itemKey"></param>
        /// <param name="strRoles"></param>
        /// <returns></returns>
        public void DeletePermissionItemsInRolesByItemkey(string itemKey, IEnumerable<string> strRoles)
        {
            using (var db = DbService.GetInstance())
            {
                var result = db.ExecuteCommand("delete from sc_permission_items_in_roles where itemkey = @itemkey", new { itemkey = itemKey });
                if (result > 0)
                {
                    foreach (var roleName in strRoles)
                    {
                        RealTimeCacheHelper.IncreaseAreaVersion("RoleName", roleName);
                    }
                }
            }
        }

        /// <summary>
        /// 给角色添加一组权限
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="strPermissionItems">权限项组</param>
        /// <returns></returns>
        public void AddRolesToPermissionItems(string roleName, string[] strPermissionItems)
        {
            foreach (var item in strPermissionItems)
            {
                PermissionItemsInRoles entity = new PermissionItemsInRoles();
                entity.Itemkey = item;
                entity.Rolename = roleName;
                base.AddByCache(entity, true);
            }
        }
    }
}
