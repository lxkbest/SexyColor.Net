using System;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using MySqlSugar;

namespace SexyColor.CommonComponents
{
    public class UserAddressRepository : Repository<UserAddress>, IUserAddressRepository
    {

        /// <summary>
        /// 根据筛选条件 （获取分页组件）
        /// </summary>
        /// <param name="userQuery">筛选条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns></returns>
        public PagingDataSet<UserAddress> GetUserAddress(UserAddressQuery userAddressQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = string.Empty;
            string orderBy = string.Empty;
            HandleQueryByString(userAddressQuery, out whereSql);//查询
            HandleOrderByEunm(userAddressQuery, out orderBy);//排序
            return GetPageListByCache<int>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, orderBy, i => i.Id);
        }
        /// <summary>
        /// 获取用户地址实体
        /// </summary>
        /// <param name="userAddressId"></param>
        /// <returns></returns>
        public UserAddress GetUserAddress(long userAddressId)
        {
            return base.GetByCache(m => m.Id == userAddressId, userAddressId);
        }
         
        /// <summary>
        /// 更新实体带缓存
        /// </summary>
        /// <param name="userAddress"></param>
        public bool UpdateCache(UserAddress userAddress)
        {
            return base.UpdateByCache(userAddress);
        }

        /// <summary>
        /// 根据枚举排序
        /// </summary>
        /// <param name="rolesQuery"></param>
        /// <param name="order"></param>
        private void HandleOrderByEunm(UserAddressQuery userAddressQuery, out string order)
        {
            order = "";
            switch (userAddressQuery.UserAddressSortBy)
            {
                case UserAddressSortBy.Name:
                    order = "name ASC";
                    break;
                case UserAddressSortBy.Name_Desc:
                    order = "name DESC";
                    break;
                case UserAddressSortBy.Phone:
                    order = "phone ASC";
                    break;
                case UserAddressSortBy.Phone_Desc:
                    order = "phone DESC";
                    break;
            }
        }

        /// <summary>
        /// 查询用户地址
        /// </summary>
        /// <param name="rolesQuery">查询参数</param>
        /// <param name="sql"></param>
        private void HandleQueryByString(UserAddressQuery userAddressQuery, out string sql)
        {
            sql = "1=1 ";
            if (userAddressQuery.UserId.HasValue)
                sql += $"and userid in (select userid from sc_user where userid = \"{userAddressQuery.UserId}\") ";
            if (userAddressQuery.UserName.IsNotNullAndWhiteSpace())
                sql += $"and userid in (select userid from sc_user where username = \"{userAddressQuery.UserName}\") ";
            if (!string.IsNullOrWhiteSpace(userAddressQuery.Name))
                sql += $"and (name like \"%{userAddressQuery.Name}%\")";
            if (!string.IsNullOrWhiteSpace(userAddressQuery.Phone))
                sql += $"and (phone like \"%{userAddressQuery.Phone}%\")";
            if (!string.IsNullOrWhiteSpace(userAddressQuery.Address))
                sql += $"and (address like \"%{userAddressQuery.Address}%\")";
            if (userAddressQuery.Provinceid.HasValue)
                sql += $"and provinceid = {userAddressQuery.Provinceid} ";
            if (userAddressQuery.Cityid.HasValue)
                sql += $"and cityid = {userAddressQuery.Cityid} ";
            if (userAddressQuery.Areaid.HasValue)
                sql += $"and areaid = {userAddressQuery.Areaid} ";
            if (userAddressQuery.IsDefault.HasValue)
                sql += $"and is_default = {userAddressQuery.IsDefault} ";
            if (userAddressQuery.IsDeleted.HasValue)
                sql += $"and is_deleted = {userAddressQuery.IsDeleted} ";
        }

        /// <summary>
        /// 根据用户编号获取用户地址列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserAddress> GetUserAddressEntitysByUserid(long userId)
        {
            var cacheKey = string.Format("{0}GetUserAddressEntitysByUserid:Userid-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Userid", userId), userId);
            List<UserAddress> list = cacheService.Get<List<UserAddress>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<UserAddress>().Where(w => w.Userid == userId).ToList();
                }
                if(list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }

        /// <summary>
        /// 根据用户编号获取用户默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserAddress GetDefaultAddressByUserid(long userId)
        {
            var cacheKey = string.Format("{0}GetDefaultAddressByUserid:Userid-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Userid", userId), userId);
            UserAddress entity = cacheService.Get<UserAddress>(cacheKey);
            if (entity == null)
            {
                using (var db = DbService.GetInstance())
                {
                    entity = db.Queryable<UserAddress>().Where(w => w.Userid == userId).Where(w => w.IsDefault == true).FirstOrDefault();
                }
                if (entity != null)
                    cacheService.Add(cacheKey, entity, CachingExpirationType.UsualSingleObject);
            }
            return entity;
        }
    }
}
