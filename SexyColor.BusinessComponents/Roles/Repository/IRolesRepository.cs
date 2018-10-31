using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IRolesRepository : IRepository<Roles>
    {
        bool DeleleRoles();

        /// <summary>
        /// 获取角色集合(根据角色名集合)
        /// </summary>
        /// <param name="roleNames">角色名集合</param>
        /// <returns></returns>
        PagingDataSet<Roles> GetRoles(RolesQuery rolesQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>Roles</returns>
        Roles GetRole(string roleName);

        /// <summary>
        /// 更新实体（带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool UpdateCache(Roles entity);

        /// <summary>
        /// 获取角色实体
        /// </summary>
        /// <param name="roleNmae"></param>
        /// <returns></returns>
        Roles GetRoles(string roleNmae);

        /// <summary>
        /// 删除实体（更新缓存）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteCache(Roles entity);
        /// <summary>
        ///新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool AddRoles(Roles entity);

        /// <summary>
        /// 获取角色集合(根据是否系统内置并且是否关联到用户)选出管理用户
        /// </summary>
        /// <returns></returns>
        IEnumerable<Roles> GetRolesByConnectToUser(bool connectToUser);
    }
}
