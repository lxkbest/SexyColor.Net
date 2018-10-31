using System.Collections.Generic;
using System;

namespace SexyColor.BusinessComponents
{
    public class GoodsCategoryLevelService : IGoodsCategoryLevelService
    {
        public IGoodsCategoryLevel1Repository goodsCategoryLevel1Repository { get; set; }
        public IGoodsCategoryLevel2Repository goodsCategoryLevel2Repository { get; set; }

        /// <summary>
        /// 获取商品一级分类对象
        /// </summary>
        /// <param name="categoryLevelId"></param>
        /// <returns></returns>
        public GoodsCategoryLevel1 GetCategoryLevel1(int categoryLevelId)
        {
            return goodsCategoryLevel1Repository.GetByCache(w => w.CategoryLevel1Id == categoryLevelId, categoryLevelId);
        }

        /// <summary>
        /// 获取一级分类JSON
        /// </summary>
        /// <returns></returns>
        public string GetCategoryLevel1AllJson()
        {
            return goodsCategoryLevel1Repository.GetCategoryLevel1AllJson();
        }

        /// <summary>
        /// 获取一级分类JSON
        /// </summary>
        /// <returns></returns>
        public string GetCategoryLevel2Json(int categoryLevelId)
        {
            return goodsCategoryLevel2Repository.GetCategoryLevel2Json(categoryLevelId);
        }

        /// <summary>
        /// 获取商品二级分类对象
        /// </summary>
        /// <param name="categoryLevelId"></param>
        /// <returns></returns>
        public GoodsCategoryLevel2 GetCategoryLevel2(int categoryLevelId)
        {
            return goodsCategoryLevel2Repository.GetByCache(w => w.CategoryLevel2Id == categoryLevelId, categoryLevelId);
        }

        /// <summary>
        /// 获取全部一级分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GoodsCategoryLevel1> GetCategoryLevel1All()
        {
            return goodsCategoryLevel1Repository.GetAllByCache("display_order asc", w => w.CategoryLevel1Id);
        }

        /// <summary>
        /// 根据一级分类编号获取二级分类列表
        /// </summary>
        /// <param name="level1Id"></param>
        /// <returns></returns>
        public IEnumerable<GoodsCategoryLevel2> GetCategoryLevel2ByLevel1Id(int level1Id)
        {
            return goodsCategoryLevel2Repository.GetCategoryLevel2ByLevel1Id(level1Id);
        }

        /// <summary>
        /// 更新一级分类或二级分类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="category"></param>
        public T UpdataCategoryLevel<T>(T category, int oldCategoryLevel2Id = 0) where T : ICategoryLevelConstraint
        {
            var resutl = false;
            if (category is GoodsCategoryLevel1)
            {
                resutl = goodsCategoryLevel1Repository.UpdataCategoryLevel(category as GoodsCategoryLevel1);
                if (resutl)
                    return category;
                else
                    return default(T);
            }
            else if (category is GoodsCategoryLevel2)
            {
                resutl = goodsCategoryLevel2Repository.UpdataCategoryLevel(category as GoodsCategoryLevel2, oldCategoryLevel2Id);
                if (resutl)
                    return category;
                else
                    return default(T);
            }
            else
                return default(T);
        }

        /// <summary>
        /// 创建一级分类或二级分类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="category"></param>
        /// <returns></returns>
        public T CreateCategoryLevel<T>(T category) where T : ICategoryLevelConstraint
        {
            object resutl = null;
            if (category is GoodsCategoryLevel1)
            {
                resutl = goodsCategoryLevel1Repository.AddByCache(category as GoodsCategoryLevel1, true);
                var resultInt = 0;
                if (resutl != null && int.TryParse(resutl.ToString(), out resultInt))
                {
                    (category as GoodsCategoryLevel1).CategoryLevel1Id = resultInt;
                    return category;
                }
            }
            else if (category is GoodsCategoryLevel2)
            {
                resutl = goodsCategoryLevel2Repository.AddByCache(category as GoodsCategoryLevel2, true);
                var resultInt = 0;
                if (resutl != null && int.TryParse(resutl.ToString(), out resultInt))
                {
                    (category as GoodsCategoryLevel2).CategoryLevel2Id = resultInt;
                    return category;
                }
            }
            return default(T);
        }
    }
}
