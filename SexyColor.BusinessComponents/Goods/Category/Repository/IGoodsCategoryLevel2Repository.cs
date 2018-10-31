using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsCategoryLevel2Repository : IRepository<GoodsCategoryLevel2>
    {
        /// <summary>
        /// 根据一级分类编号获取二级分类
        /// </summary>
        /// <param name="level1Id"></param>
        /// <returns></returns>
        IEnumerable<GoodsCategoryLevel2> GetCategoryLevel2ByLevel1Id(int level1Id);

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdataCategoryLevel(GoodsCategoryLevel2 entity, int oldCategoryLevel2Id);

        /// <summary>
        /// 根据一级分类编号获取二级分类JSON
        /// </summary>
        /// <returns></returns>
        string GetCategoryLevel2Json(int categoryLevelId);
    }
}
