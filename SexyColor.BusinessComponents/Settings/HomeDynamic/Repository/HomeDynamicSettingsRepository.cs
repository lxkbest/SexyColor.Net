using System;
using SexyColor.Infrastructure;
using System.Linq;
using MySqlSugar;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public class HomeDynamicSettingsRepository : Repository<HomeDynamicSettings>, IHomeDynamicSettingsRepository
    {


        /// <summary>
        /// 首页动态设置分页
        /// </summary>
        /// <param name="typeQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<HomeDynamicSettings> GetPageHomeDynamic(int typeQuery, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            int totalPage = 0;
            string whereSql = " type = @type ";
            var whereObj = new { type = typeQuery };
            string orderBy = "display_order asc";
            return GetPageListByCache<long>(pageIndex, pageSize, out totalCount, out totalPage, whereSql, whereObj, orderBy, i => i.Id);
        }

        /// <summary>
        /// 更新排序顺序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateHomeDynamicByDisplayOrder(HomeDynamicSettings entity)
        {
            return base.UpdateByCache(entity, new { DisplayOrder = entity.DisplayOrder }, w => w.Id == entity.Id);
        }

        /// <summary>
        /// 根据标识和类型，动作。获取首页动态设置对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="isUp"></param>
        /// <returns></returns>
        public HomeDynamicSettings GetHomeDynamicTop(long id, int type, bool isUp)
        {
            using (var db = DbService.GetInstance())
            {
                Sqlable sable = db.Sqlable().From("sc_home_dynamic_settings", "s")
                  .Where("s.type=@type");
                if (isUp)
                    sable = sable.Where($"s.display_order < (select display_order from sc_home_dynamic_settings where id = @id)");
                else
                    sable = sable.Where($"s.display_order > (select display_order from sc_home_dynamic_settings where id = @id)");
                List<HomeDynamicSettings> list = sable.SelectToList<HomeDynamicSettings>("s.*", new { id = id, type = type });
                if (list != null && list.Count > 0)
                {
                    if (isUp)
                        return list.OrderByDescending(w => w.DisplayOrder).FirstOrDefault();
                    else
                        return list.OrderBy(w => w.DisplayOrder).FirstOrDefault();

                }
                return null;
            }
        }

        /// <summary>
        /// 更新首页动态设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateHomeDynamic(HomeDynamicSettings entity)
        {
            return base.UpdateByCache(entity);
        }

        /// <summary>
        /// 获取某个类型的排序最大值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetMaxDisplayOrder(int type)
        {
            using (var db = DbService.GetInstance())
            {
                int displayOrder = db.Queryable<HomeDynamicSettings>().Where(w => w.Type == type).Max<int>("display_order");
                return displayOrder;
            }
        }

        /// <summary>
        /// 根据类型获取首页动态列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<HomeDynamicSettings> GetHomeDynamicByType(int type)
        {

            var cacheKey = string.Format("{0}GetHomeDynamicByType:Type-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "Type", type), type);
            List<HomeDynamicSettings> list = cacheService.Get<List<HomeDynamicSettings>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<HomeDynamicSettings>().Where(w => w.Type == type).ToList();
                }
                if (list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }

        /// <summary>
        /// 根据父级获取首页动态列表
        /// </summary>
        /// <param name="parentsid"></param>
        /// <returns></returns>
        public IEnumerable<HomeDynamicSettings> GetHomeDynamicByParentsid(int parentsid)
        {
            var cacheKey = string.Format("{0}GetHomeDynamicByParentsid:ParentsId-{1}", RealTimeCacheHelper.GetListCacheKeyPrefix(CacheVersionType.AreaVersion, "ParentsId", parentsid), parentsid);
            List<HomeDynamicSettings> list = cacheService.Get<List<HomeDynamicSettings>>(cacheKey);
            if (list == null)
            {
                using (var db = DbService.GetInstance())
                {
                    list = db.Queryable<HomeDynamicSettings>().Where(w => w.ParentsId == parentsid.ToString()).ToList();
                }
                if (list != null)
                    cacheService.Add(cacheKey, list, CachingExpirationType.UsualObjectCollection);
            }
            return list;
        }
    }
}
