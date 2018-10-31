using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsCategoryService
    {
        /// <summary>
        /// 获取全部商品分类
        /// </summary>
        /// <returns></returns>
        IEnumerable<GoodsCategory> GetGoodsCategoryAll();

        /// <summary>
        /// 根据商品分类获取分类项
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IEnumerable<GoodsCategoryInItem> GetGoodsCategoryInItemByCategoryId(int categoryId);

        /// <summary>
        /// 根据分类项编号集合获取分类项对象集合
        /// </summary>
        /// <param name="categoryItemIds"></param>
        /// <returns></returns>
        IEnumerable<CategoryItem> GetCategoryItemByIds(IEnumerable<int> categoryItemIds);

        /// <summary>
        /// 根据商品分类项获取包含特征项集合
        /// </summary>
        /// <param name="categoryItemId"></param>
        /// <returns></returns>
        IEnumerable<CategoryItemInChara> GetCategoryItemInCharaByCategoryItemId(int categoryItemId);

        /// <summary>
        /// 根据商品分类项获取包含品牌项集合
        /// </summary>
        /// <param name="categoryItemId"></param>
        /// <returns></returns>
        IEnumerable<CategoryItemInBrands> GetCategoryItemInBrandsByCategoryItemId(int categoryItemId);

        /// <summary>
        /// 根据品牌编号获取品牌集合
        /// </summary>
        /// <param name="brandsIds"></param>
        /// <returns></returns>
        IEnumerable<CategoryItemBrands> GetCategoryItemBrandsIds(IEnumerable<int> brandsIds);

        /// <summary>
        /// 根据特征编号获取特征集合
        /// </summary>
        /// <param name="charaIds"></param>
        /// <returns></returns>
        IEnumerable<CategoryItemChara> GetCategoryItemCharaIds(IEnumerable<int> charaIds);

    }
}
