using SexyColor.BusinessComponents;
using System.Collections.Generic;

namespace SexyColor.Web
{
    public class LeftMenuModel
    {
        /// <summary>
        /// 权限项集合
        /// </summary>
        public IEnumerable<PermissionItems> PermissionItemsList { get; set; }

        /// <summary>
        /// 权限项角色集合
        /// </summary>
        public IEnumerable<string> PermissionItemsInRolesList { get; set; }

        public bool IsSuperAdministrator { get; set; }

        public bool IsContentAdministrator { get; set; }
    }
}
