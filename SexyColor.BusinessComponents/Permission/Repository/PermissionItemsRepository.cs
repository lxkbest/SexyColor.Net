using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class PermissionItemsRepository : Repository<PermissionItems>, IPermissionItemsRepository
    {
        /// <summary>
        /// 根据父级项查询节点数据
        /// </summary>
        /// <param name="parentsid"></param>
        /// <returns></returns>
        public string GetJsonStringByParentsid(string parentsid)
        {
            string cacheKey = RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "parentsid", parentsid);
            string itemKeys = cacheService.Get<string>(cacheKey);
            if (itemKeys == null)
            {
                using (var db = DbService.GetInstance())
                {
                    itemKeys = db.Sqlable().From("sc_permission_items", "s")
                                 .Where("s.parentsid=@parentsid")
                                 .SelectToJson("s.itemkey,s.itemname", new { parentsid = parentsid });
                }
                if (!string.IsNullOrEmpty(itemKeys))
                    cacheService.Add(cacheKey, itemKeys, CachingExpirationType.UsualObjectCollection);
            }
            return itemKeys;
        }

        /// <summary>
        /// 添加权限项
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isIdentity"></param>
        /// <returns></returns>
        public bool AddPermissionItems(PermissionItems entity, bool isIdentity = true)
        {
            object obj = base.AddByCache(entity, isIdentity);
            if (obj != null)
                RealTimeCacheHelper.IncreaseAreaVersion("parentsid", entity.Parentsid);
            if (!isIdentity)
            {
                var result = bool.TryParse(obj.ToString(), out var resultBool);
            }
            return long.TryParse(obj.ToString(), out var resultLoog);

        }

        /// <summary>
        /// 更新权限项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdatePermissionItems(PermissionItems entity, string oldParentsid)
        {
            var result = base.UpdateByCache(entity);
            if (result)
            {
                RealTimeCacheHelper.IncreaseAreaVersion("parentsid", entity.Parentsid);
                if (entity.Parentsid != oldParentsid)
                    RealTimeCacheHelper.IncreaseAreaVersion("parentsid", oldParentsid);
            }
            return result;

        }

        /// <summary>
        /// 删除权限项，同时删除所有角色所拥有的该权限项
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public bool DeletePermissionItems(PermissionItems entity)
        {
            var result = base.DeleteByCache(entity);
            if (result)
                RealTimeCacheHelper.IncreaseAreaVersion("parentsid", entity.Parentsid);
            return result;
        }
    }
}
