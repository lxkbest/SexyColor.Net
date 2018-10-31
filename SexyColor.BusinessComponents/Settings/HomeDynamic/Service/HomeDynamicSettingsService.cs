using System;
using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class HomeDynamicSettingsService : IHomeDynamicSettingsService
    {
        public IHomeDynamicSettingsRepository homeDynamicSettingsRepository { get; set; }

        /// <summary>
        /// 首页动态设置分页
        /// </summary>
        /// <param name="typeQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<HomeDynamicSettings> GetPageHomeDynamic(int typeQuery, int pageIndex, int pageSize)
        {
            return homeDynamicSettingsRepository.GetPageHomeDynamic(typeQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据标识获取首页动态设置对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HomeDynamicSettings GetHomeDynamicSingle(long id)
        {
            return homeDynamicSettingsRepository.GetByCache(w => w.Id == id, id);
        }

        /// <summary>
        /// 根据标识和类型，动作。获取首页动态设置对象
        /// </summary>
        /// <returns></returns>
        public HomeDynamicSettings GetHomeDynamicTop(long id, int type, bool isUp)
        {
            if (id > 0 && type > 0)
                return homeDynamicSettingsRepository.GetHomeDynamicTop(id, type, isUp);
            return null;
        }


        /// <summary>
        /// 更新排序顺序
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateHomeDynamicByDisplayOrder(HomeDynamicSettings entity, out string message)
        {
            message = string.Empty;
            var result = homeDynamicSettingsRepository.UpdateHomeDynamicByDisplayOrder(entity);
            if (result)
                message = "操作成功";
        }

        /// <summary>
        /// 添加首页动态设置
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        public void AddHomeDynamic(HomeDynamicSettings entity, out string message)
        {
            message = string.Empty;
            var obj = homeDynamicSettingsRepository.AddByCache(entity, true);
            if (int.TryParse(obj.ToString(), out var result))
                message = "操作成功";
            else
                message = "操作失败";
        }

        /// <summary>
        /// 更新首页动态设置
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        public void UpdateHomeDynamic(HomeDynamicSettings entity, out string message)
        {
            message = string.Empty;
            var result = homeDynamicSettingsRepository.UpdateHomeDynamic(entity);
            if (result)
                message = "操作成功";
            else
                message = "操作失败";
        }

        /// <summary>
        /// 获取某个类型的排序最大值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetMaxDisplayOrder(int type)
        {
           return homeDynamicSettingsRepository.GetMaxDisplayOrder(type) + 1;
        }

        /// <summary>
        /// 根据类型获取首页动态列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<HomeDynamicSettings> GetHomeDynamicByType(int type)
        {
            return homeDynamicSettingsRepository.GetHomeDynamicByType(type);
        }

        /// <summary>
        /// 根据父级获取首页动态列表
        /// </summary>
        /// <param name="parentsid"></param>
        /// <returns></returns>
        public IEnumerable<HomeDynamicSettings> GetHomeDynamicByParentsid(int parentsid)
        {
            return homeDynamicSettingsRepository.GetHomeDynamicByParentsid(parentsid);
        }
    }
}
