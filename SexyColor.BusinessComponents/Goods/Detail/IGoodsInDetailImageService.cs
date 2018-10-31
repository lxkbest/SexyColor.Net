using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsInDetailImageService
    {
        /// <summary>
        /// 根据goodid获取所有实体
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        IEnumerable<GoodsInDetailImage> GetAllGoodsInDetailImage(long goodsId);
        /// <summary>
        /// 根据id获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GoodsInDetailImage GetSingleGoodsInDetailImage(long id);
        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <param name="goodsInDetailImage">实体</param>
        /// <returns></returns>
        bool EditGoodsInDetailImage(GoodsInDetailImage goodsInDetailImage);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="imgModel">实体</param>
        /// <returns></returns>
        bool AddGoodsInDetailImage(GoodsInDetailImage imgModel);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="detailImage"></param>
        /// <returns></returns>
        bool DeleteGoodsInDetailImage(GoodsInDetailImage detailImage);
    }
}
