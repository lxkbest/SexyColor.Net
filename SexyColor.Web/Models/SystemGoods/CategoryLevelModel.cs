using SexyColor.BusinessComponents;
using System;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class CategoryLevelModel
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// 父级名称
        /// </summary>
        public string ParentCategoryName { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Display(Name = "分类名称")]
        [Required(ErrorMessage = "分类名称不能为空")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 分类级别数字
        /// </summary>
        public int? LevelNumber { get; set; }


        /// <summary>
        /// 懒加载
        /// </summary>
        public CategoryLevel LevelEnum
        {
            get
            {
                CategoryLevel level = CategoryLevel.NULL;
                if (LevelNumber.HasValue)
                {
                    Enum.TryParse<CategoryLevel>(LevelNumber.Value.ToString(), out level);
                }
                return level;
            }
        }




        public GoodsCategoryLevel1 AsGoodsCategoryLevel1()
        {
            GoodsCategoryLevel1 categoryLevel = GoodsCategoryLevel1.New();
            categoryLevel.CategoryName = CategoryName;
            categoryLevel.Description = Description;
            categoryLevel.CategoryLevel1ImageUrl = ImageUrl;
            return categoryLevel;
        }

        public GoodsCategoryLevel2 AsGoodsCategoryLevel2()
        {
            GoodsCategoryLevel2 categoryLevel = GoodsCategoryLevel2.New();
            categoryLevel.CategoryName = CategoryName;
            categoryLevel.Description = Description;
            categoryLevel.CategoryLevel2ImageUrl = ImageUrl;
            return categoryLevel;
        }

        /// <summary>
        /// 将GoodsCategoryLevel1转换CategoryLevelModel 采用链式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public CategoryLevelModel ToCategoryLevel1Model(GoodsCategoryLevel1 item)
        {
            CategoryId = item.CategoryLevel1Id;
            CategoryName = item.CategoryName;
            Description = item.Description;
            ImageUrl = item.CategoryLevel1ImageUrl;
            LevelNumber = (int)CategoryLevel.Level1;
            return this;
        }

        /// <summary>
        /// 将GoodsCategoryLevel2转换CategoryLevelModel 采用链式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public CategoryLevelModel ToCategoryLevel2Model(GoodsCategoryLevel2 item)
        {
            CategoryId = item.CategoryLevel2Id;
            CategoryName = item.CategoryName;
            Description = item.Description;
            ImageUrl = item.CategoryLevel2ImageUrl;
            LevelNumber = (int)CategoryLevel.Level2;
            ParentCategoryId = item.CategoryLevel1Id;
            return this;
        }
    }
}
