using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public interface ICategoryItemInBrandsRepository : IRepository<CategoryItemInBrands>
    {
        /// <summary>
        /// 根据商品分类项获取包含品牌项集合
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IEnumerable<CategoryItemInBrands> GetCategoryItemInBrandsByCategoryItemId(int categoryItemId);
    }
}
