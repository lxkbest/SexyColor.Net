using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsCategoryLevelService
    {
        /// <summary>
        /// 获取商品一级分类对象
        /// </summary>
        /// <param name="categoryLevelId"></param>
        /// <returns></returns>
        GoodsCategoryLevel1 GetCategoryLevel1(int categoryLevelId);

        /// <summary>
        /// 获取商品二级分类对象
        /// </summary>
        /// <param name="categoryLevelId"></param>
        /// <returns></returns>
        GoodsCategoryLevel2 GetCategoryLevel2(int categoryLevelId);

        /// <summary>
        /// 获取全部一级分类列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<GoodsCategoryLevel1> GetCategoryLevel1All();

        /// <summary>
        /// 获取全部一级分类JSON
        /// </summary>
        /// <returns></returns>
        string GetCategoryLevel1AllJson();

        /// <summary>
        /// 根据一级分类编号获取二级分类JSON
        /// </summary>
        /// <param name="categoryLevelId"></param>
        /// <returns></returns>
        string GetCategoryLevel2Json(int categoryLevelId);

        /// <summary>
        /// 根据一级分类编号获取二级分类列表
        /// </summary>
        /// <param name="level1Id"></param>
        /// <returns></returns>
        IEnumerable<GoodsCategoryLevel2> GetCategoryLevel2ByLevel1Id(int level1Id);

        /// <summary>
        /// 更新一级分类或二级分类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="category"></param>
        /// <returns></returns>
        T UpdataCategoryLevel<T>(T category, int oldCategoryLevel2Id = 0) where T : ICategoryLevelConstraint;

        /// <summary>
        /// 创建一级分类或二级分类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="category"></param>
        /// <returns></returns>
        T CreateCategoryLevel<T>(T category) where T : ICategoryLevelConstraint;
    }
}
