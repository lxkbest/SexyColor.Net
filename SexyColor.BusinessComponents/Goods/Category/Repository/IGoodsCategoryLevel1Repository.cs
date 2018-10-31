using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsCategoryLevel1Repository : IRepository<GoodsCategoryLevel1>
    {

        

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdataCategoryLevel(GoodsCategoryLevel1 entity);

        /// <summary>
        /// 获取全部一级JSON
        /// </summary>
        /// <returns></returns>
        string GetCategoryLevel1AllJson();
    }
}
