using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IUsersInRolesRepository : IRepository<UsersInRoles>
    {
        /// <summary>
        /// 获取角色名集合(根据用户编号)
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="isPublic">是否仅获取对外公开的角色</param>
        /// <returns></returns>
        IEnumerable<string> GetRolesNamesInUser(long userId, bool isPublic = false);

        /// <summary>
        /// 移除用户所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        void RemoveRolesInUser(long userId);

        /// <summary>
        /// 为用户添加一组角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="strRoles">一组角色名称</param>
        void AddUserToRoles(long userId, List<string> strRoles);

        
    }
}
