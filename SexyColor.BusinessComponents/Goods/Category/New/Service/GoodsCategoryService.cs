using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class GoodsCategoryService : IGoodsCategoryService
    {
        public IGoodsCategoryRepository goodsCategoryRepository { get; set; }
        public IGoodsCategoryInItemRepository goodsCategoryInItemRepository { get; set; }
        public ICategoryItemRepository categoryItemRepository { get; set; }
        public ICategoryItemCharaRepository categoryItemCharaRepository { get; set; }
        public ICategoryItemBrandsRepository categoryItemBrandsRepository { get; set; }
        public ICategoryItemInCharaRepository categoryItemInCharaRepository { get; set; }
        public ICategoryItemInBrandsRepository categoryItemInBrandsRepository { get; set; }


        /// <summary>
        /// 获取全部商品分类
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GoodsCategory> GetGoodsCategoryAll()
        {
            return goodsCategoryRepository.GetAllByCache("display_order asc", w => w.CategoryId);
        }

        /// <summary>
        /// 根据商品分类获取分类项
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsCategoryInItem> GetGoodsCategoryInItemByCategoryId(int categoryId)
        {
            return goodsCategoryInItemRepository.GetGoodsCategoryInItemByCategoryId(categoryId);
        }

        /// <summary>
        /// 根据分类项编号集合获取分类项对象集合
        /// </summary>
        /// <param name="categoryItemIds"></param>
        /// <returns></returns>
        public IEnumerable<CategoryItem> GetCategoryItemByIds(IEnumerable<int> categoryItemIds)
        {
            return categoryItemRepository.CacheByEntityIds(categoryItemIds);
        }

        /// <summary>
        /// 获取全部品牌
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryItemBrands> GetCategoryItemBrands()
        {
            return categoryItemBrandsRepository.GetAllByCache("dete_created asc", w => w.BrandsId);
        }

        /// <summary>
        /// 根据品牌编号获取品牌集合
        /// </summary>
        /// <param name="brandsIds"></param>
        /// <returns></returns>
        public IEnumerable<CategoryItemBrands> GetCategoryItemBrandsIds(IEnumerable<int> brandsIds)
        {
            return categoryItemBrandsRepository.CacheByEntityIds(brandsIds);
        }

        /// <summary>
        /// 获取全部特征
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryItemChara> GetCategoryItemChara()
        {
            return categoryItemCharaRepository.GetAllByCache("dete_created asc", w => w.CharaId);
        }

        /// <summary>
        /// 根据特征编号获取特征集合
        /// </summary>
        /// <param name="brandsIds"></param>
        /// <returns></returns>
        public IEnumerable<CategoryItemChara> GetCategoryItemCharaIds(IEnumerable<int> charaIds)
        {
            return categoryItemCharaRepository.CacheByEntityIds(charaIds);
        }

        /// <summary>
        /// 根据商品分类项获取包含特征项集合
        /// </summary>
        /// <param name="categoryItemId"></param>
        /// <returns></returns>
        public IEnumerable<CategoryItemInChara> GetCategoryItemInCharaByCategoryItemId(int categoryItemId)
        {
            return categoryItemInCharaRepository.GetCategoryItemInCharaByCategoryItemId(categoryItemId);
        }

        /// <summary>
        /// 根据商品分类项获取包含品牌项集合
        /// </summary>
        /// <param name="categoryItemId"></param>
        /// <returns></returns>
        public IEnumerable<CategoryItemInBrands> GetCategoryItemInBrandsByCategoryItemId(int categoryItemId)
        {
            return categoryItemInBrandsRepository.GetCategoryItemInBrandsByCategoryItemId(categoryItemId);
        }

    }
}
