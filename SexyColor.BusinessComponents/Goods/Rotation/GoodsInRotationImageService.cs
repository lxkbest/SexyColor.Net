using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public class GoodsInRotationImageService : IGoodsInRotationImageService
    {
        public IGoodsInRotationImageRepository goodsInRotationImageRepository { get; set; }

        public bool AddGoodsInRotationImage(GoodsInRotationImage imgModel)
        {
            long id = 0;
            long.TryParse(goodsInRotationImageRepository.AddByCache(imgModel, true).ToString(),out id);
            bool result = id > 0 ? true : false;
            if (result)
            {
                goodsInRotationImageRepository.UpdateCacheByAddEntity(imgModel);
            }
            return result;
        }
        /// <summary>
        /// 删除商品轮换图实体
        /// </summary>
        /// <param name="entity"></param>
        public bool DeleteGoodsRotationImage(GoodsInRotationImage entity)
        {
            return goodsInRotationImageRepository.DeleteEntityByCache(entity);
        }
        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <param name="rotationImg">实体</param>
        /// <returns></returns>
        public bool EditGoodsRotationImage(GoodsInRotationImage rotationImg)
        {
            return goodsInRotationImageRepository.UpdateCacheByEntity(rotationImg);
        }
        /// <summary>
        /// 根据goodsId获取所有实体
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsInRotationImage> GetAllGoodsRotationImage(long goodsId)
        {
            return goodsInRotationImageRepository.GetAllGoodsRotationImage(goodsId);
        }
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsInRotationImage GetGoodsInRotationImage(long id)
        {
            return goodsInRotationImageRepository.GetGoodsInRotationImage(id);
        }
    }
}
