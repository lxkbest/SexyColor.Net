using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public interface ICategoryItemInCharaRepository : IRepository<CategoryItemInChara>
    {
        /// <summary>
        /// 根据商品分类项获取包含特征项集合
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IEnumerable<CategoryItemInChara> GetCategoryItemInCharaByCategoryItemId(int categoryItemId);
    }
}
