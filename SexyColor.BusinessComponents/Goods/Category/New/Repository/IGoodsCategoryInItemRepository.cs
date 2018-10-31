using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsCategoryInItemRepository : IRepository<GoodsCategoryInItem>
    {
        /// <summary>
        /// 根据商品分类获取分类项
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IEnumerable<GoodsCategoryInItem> GetGoodsCategoryInItemByCategoryId(int categoryId);
    }
}
