using System;
using System.Collections.Generic;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    public class RolesRepository : Repository<Roles>, IRolesRepository
    {
        public bool DeleleRoles()
        {
            return true;
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
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleQueryByString(rolesQuery, out whereSql);
            HandleOrderByEunm(rolesQuery, out orderBy);
            return GetPageListByCache<string>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, orderBy, i => i.Rolename);
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="rolesQuery">查询参数</param>
        /// <param name="sql"></param>
        private void HandleQueryByString(RolesQuery rolesQuery, out string sql)
        {
            sql = "1=1 ";
            if (!string.IsNullOrWhiteSpace(rolesQuery.Keyword))
                sql += $"and (rolename like \"%{rolesQuery.Keyword}%\" or friendly_rolename like \"%{rolesQuery.Keyword}%\") ";
            if (rolesQuery.IsEnabled.HasValue)
                sql += $"and is_enabled = {rolesQuery.IsEnabled} ";
            if (rolesQuery.IsPublic.HasValue)
                sql += $"and is_public = {rolesQuery.IsPublic} ";
            if (rolesQuery.IsBuiltin.HasValue)
                sql += $"and is_builtin = {rolesQuery.IsBuiltin} ";
        }

        /// <summary>
        /// 根据枚举排序
        /// </summary>
        /// <param name="rolesQuery"></param>
        /// <param name="order"></param>
        private void HandleOrderByEunm(RolesQuery rolesQuery, out string order)
        {
            order = "";
            switch (rolesQuery.RoleSortBy)
            {
                case RoleSortBy.Rolename:
                    order = "rolename ASC";
                    break;
                case RoleSortBy.Rolename_Desc:
                    order = "rolename DESC";
                    break;
                case RoleSortBy.IsEnabled:
                    order = "is_enabled ASC";
                    break;
            }
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>Roles</returns>
        public Roles GetRole(string roleName)
        {
            return base.GetByCache(m=>m.Rolename==roleName,roleName);
        }

        /// <summary>
        /// 更新实体（带缓存）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool UpdateCache(Roles entity)
        {
            return base.UpdateByCache(entity);
        }

        /// <summary>
        /// 获取角色实体
        /// </summary>
        /// <param name="roleNmae"></param>
        /// <returns></returns>
        public Roles GetRoles(string roleNmae)
        {
            return base.GetByCache(w => w.Rolename == roleNmae, roleNmae);
        }

        /// <summary>
        /// 删除实体（更新缓存）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteCache(Roles entity)
        {
            return base.DeleteByCache(entity);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool AddRoles (Roles entity)
        {
            return bool.Parse(base.AddByCache(entity,false).ToString());
        }

        /// <summary>
        /// 获取角色集合(根据是否关联到用户)选出管理用户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Roles> GetRolesByConnectToUser(bool connectToUser)
        {
            var cacheKey = string.Format("{0}GetRolesByConnectToUser:ConnectToUser-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "ConnectToUser", connectToUser), connectToUser);
            List<Roles> list = cacheService.Get<List<Roles>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Sqlable().From("sc_roles", "s")
                                 .Where("s.connect_to_user=@connect_to_user")
                                 .SelectToList<Roles>("s.*", new { connect_to_user = connectToUser });
                }
                if(list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }
    }
}
