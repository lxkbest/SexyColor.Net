using System;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public class PermissionItemsService : IPermissionItemsService
    {

        public IPermissionItemsRepository permissionItemsRepository { get; set; }
        public IPermissionItemsInRolesRepository permissionItemsInRolesRepository { get; set; }

        public PermissionItems CreatePermissionItems(PermissionItems entity, out PermissionItemsCreateStatus status)
        {
            //配置信息取出是否是使用36位的Guid作为主键
            bool IsGuidIdentity = false;
            status = PermissionItemsCreateStatus.Created;
            if (entity == null)
            {
                status = PermissionItemsCreateStatus.UnknownFailure;
                return null;
            }
            if (string.IsNullOrWhiteSpace(entity.Itemkey) || string.IsNullOrWhiteSpace(entity.Parentsid))
            {
                status = PermissionItemsCreateStatus.GuidFailure;
                return null;
            }
            
            if (!entity.Parentsid.Equals("0"))
            {

                var newGuid1 = entity.Parentsid.Substring(0, 8) + "-";
                var newGuid2 = entity.Parentsid.Substring(8 - 1, 4) + "-";
                var newGuid3 = entity.Parentsid.Substring(12 - 1, 4) + "-";
                var newGuid4 = entity.Parentsid.Substring(16 - 1, 4) + "-";
                var newGuid5 = entity.Parentsid.Substring(20 - 1, 12);
                var newGuid = $"{newGuid1}{newGuid2}{newGuid3}{newGuid4}{newGuid5}";

                if (!Guid.TryParse(newGuid, out var resultGuid))
                {
                    status = PermissionItemsCreateStatus.GuidFailure;
                    return null;
                }
            }
            if (!IsGuidIdentity && entity.Itemkey.Length > 32)
            {
                entity.Itemkey = entity.Itemkey.Replace("-", "");
            }
            if (permissionItemsRepository.AddPermissionItems(entity, false))
                return entity;
            else
                return null;
            
        }

        /// <summary>
        /// 根据权限项编号删除权限项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeletePermissionItems(PermissionItems entity, IEnumerable<string> strRoles)
        {
            var result = permissionItemsRepository.DeletePermissionItems(entity);
            permissionItemsInRolesRepository.DeletePermissionItemsInRolesByItemkey(entity.Itemkey, strRoles);
            return result;
        }

        /// <summary>
        /// 删除角色所拥有的权限项
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool DeleteRolesPermissionItems(string roleName)
        {
           
            IEnumerable<PermissionItemsInRoles> pii = permissionItemsInRolesRepository.GetPermissionItemsInRolesByRolesname(roleName);
            List<PermissionItemsInRoles> list = (List<PermissionItemsInRoles>)pii;
            if (list.Count <= 0)
                return true;
            return permissionItemsInRolesRepository.DeletePermissionItemsInRolesByRolesname(list, roleName);

        }

        /// <summary>
        /// 编辑权限项
        /// </summary>
        /// <param name="item"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool EditPermissionItems(PermissionItems entity, out PermissionItemsCreateStatus status)
        {
            status = PermissionItemsCreateStatus.Updated;
            if (entity == null)
            {
                status = PermissionItemsCreateStatus.UnknownFailure;
                return false;
            }

            if (string.IsNullOrWhiteSpace(entity.Itemkey) || string.IsNullOrWhiteSpace(entity.Parentsid))
            {
                status = PermissionItemsCreateStatus.GuidFailure;
                return false;
            }

            if (string.IsNullOrWhiteSpace(entity.ItemUrl))
            {
                status = PermissionItemsCreateStatus.GuidFailure;
                return false;
            }
            
            if (!entity.Parentsid.Equals("0"))
            {
                var newGuid1 = entity.Parentsid.Substring(0, 8) + "-";
                var newGuid2 = entity.Parentsid.Substring(8 - 1, 4) + "-";
                var newGuid3 = entity.Parentsid.Substring(12 - 1, 4) + "-";
                var newGuid4 = entity.Parentsid.Substring(16 - 1, 4) + "-";
                var newGuid5 = entity.Parentsid.Substring(20 - 1, 12);
                var newGuid = $"{newGuid1}{newGuid2}{newGuid3}{newGuid4}{newGuid5}";

                if (!Guid.TryParse(newGuid, out var resultGuid))
                {
                    status = PermissionItemsCreateStatus.GuidFailure;
                    return false;
                }
            }
            PermissionItems oldEntity = permissionItemsRepository.GetByCache(w => w.Itemkey == entity.Itemkey, entity.Itemkey);
            object result = permissionItemsRepository.UpdatePermissionItems(entity, oldEntity.Parentsid);
            return bool.TryParse(result.ToString(), out var isBool);

        }

        /// <summary>
        /// 获取全部权限项
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PermissionItems> GetPermissionItemsAll()
        {
            return permissionItemsRepository.GetAllByCache<string>("date_created asc", i => i.Itemkey);
        }

        /// <summary>
        /// 根据权限项ItemKey获取单个权限项
        /// </summary>
        /// <param name="itemKey">权限项编号</param>
        /// <returns></returns>
        public PermissionItems GetPermissionItemsByItemkey(string itemKey)
        {
            return permissionItemsRepository.GetByCache(w => w.Itemkey == itemKey, itemKey);
        }

        /// <summary>
        /// 根据父级项Parentsid获取权限集合
        /// </summary>
        /// <param name="parentsid"></param>
        /// <returns></returns>
        public string GetPermissionItemsByParentsid(string parentsid)
        {
            return permissionItemsRepository.GetJsonStringByParentsid(parentsid);
        }

        /// <summary>
        /// 根据角色名称获取权限项
        /// </summary>
        /// <param name="Rolesname">角色名</param>
        /// <returns></returns>
        public IEnumerable<PermissionItemsInRoles> GetPermissionItemsInRolesByRolesname(string rolesName)
        {
            return permissionItemsInRolesRepository.GetPermissionItemsInRolesByRolesname(rolesName);
        }


        /// <summary>
        /// 根据角色名称集合获取权限项
        /// </summary>
        /// <param name="Rolesname">角色名</param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<PermissionItemsInRoles>> GetPermissionItemsInRolesByRolesname(IEnumerable<string> rolesNames)
        {
            List<IEnumerable<PermissionItemsInRoles>> list = new List<IEnumerable<PermissionItemsInRoles>>();
            foreach (var str in rolesNames)
            {
                list.Add(permissionItemsInRolesRepository.GetPermissionItemsInRolesByRolesname(str));
            }
            return list;
        }

        /// <summary>
        /// 强制合并权限项
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public IEnumerable<string> Merge(IEnumerable<IEnumerable<PermissionItemsInRoles>> list)
        {
            List<string> permissionItemsInRoles = new List<string>();
            foreach (var items in list)
            {
                foreach (var info in items)
                {
                    if (!permissionItemsInRoles.Contains(info.Itemkey))
                    {
                        permissionItemsInRoles.Add(info.Itemkey);
                    }
                }
            }
            return permissionItemsInRoles;
        }


        /// <summary>
        /// 给角色添加一组权限
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="strPermissionItems">权限项组</param>
        /// <returns></returns>
        public bool AddRolesToPermissionItems(string roleName, string[] strPermissionItems)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;
            if (strPermissionItems.Length <= 0)
                return false;
            permissionItemsInRolesRepository.AddRolesToPermissionItems(roleName, strPermissionItems);
            return true;
        }

    }
}
