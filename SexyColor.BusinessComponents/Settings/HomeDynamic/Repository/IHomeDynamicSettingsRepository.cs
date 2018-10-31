using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IHomeDynamicSettingsRepository : IRepository<HomeDynamicSettings>
    {
        /// <summary>
        /// 首页动态设置分页
        /// </summary>
        /// <param name="typeQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagingDataSet<HomeDynamicSettings> GetPageHomeDynamic(int typeQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 更新排序顺序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateHomeDynamicByDisplayOrder(HomeDynamicSettings entity);

        /// <summary>
        /// 根据标识和类型，动作。获取首页动态设置对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="isUp"></param> 
        /// <returns></returns>
        HomeDynamicSettings GetHomeDynamicTop(long id, int type, bool isUp);

        /// <summary>
        /// 更新首页动态设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateHomeDynamic(HomeDynamicSettings entity);

        /// <summary>
        /// 获取某个类型的排序最大值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetMaxDisplayOrder(int type);

        /// <summary>
        /// 根据类型获取首页动态列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<HomeDynamicSettings> GetHomeDynamicByType(int type);

        /// <summary>
        /// 根据父级获取首页动态列表
        /// </summary>
        /// <param name="parentsid"></param>
        /// <returns></returns>
        IEnumerable<HomeDynamicSettings> GetHomeDynamicByParentsid(int parentsid);
    }
}
