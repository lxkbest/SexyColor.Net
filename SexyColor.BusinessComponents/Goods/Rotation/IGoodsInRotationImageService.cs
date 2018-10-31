using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IGoodsInRotationImageService
    {
        IEnumerable<GoodsInRotationImage> GetAllGoodsRotationImage(long goodsId);
        GoodsInRotationImage GetGoodsInRotationImage(long goodsId);
        bool EditGoodsRotationImage(GoodsInRotationImage rotationImg);
        bool AddGoodsInRotationImage(GoodsInRotationImage imgModel);
        /// <summary>
        /// 删除商品轮换图
        /// </summary>
        /// <param name="rotationImg"></param>
        bool DeleteGoodsRotationImage(GoodsInRotationImage rotationImg);
    }
}
