using System;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public class GoodsInDetailImageService : IGoodsInDetailImageService
    {
        public IGoodsInDetailImageRepository goodsInDetailImageRepository { get; set; }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="imgModel">实体</param>
        /// <returns></returns>
        public bool AddGoodsInDetailImage(GoodsInDetailImage entity)
        {
            long id = 0;
            long.TryParse(goodsInDetailImageRepository.AddByCache(entity, true).ToString(), out id);
            bool result = id > 0 ? true : false;
            if (result)
            {
                goodsInDetailImageRepository.UpdateCacheByAddEntity(entity);
            }
            return result;
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="detailImage"></param>
        /// <returns></returns>
        public bool DeleteGoodsInDetailImage(GoodsInDetailImage entity)
        {
            return goodsInDetailImageRepository.DeleteEntityByCache(entity);
        }

        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <param name="goodsInDetailImage">实体</param>
        /// <returns></returns>
        public bool EditGoodsInDetailImage(GoodsInDetailImage entity)
        {
            return goodsInDetailImageRepository.UpdateCacheByEntity(entity);
        }

        /// <summary>
        /// 根据goodid获取所有实体
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsInDetailImage> GetAllGoodsInDetailImage(long goodsId)
        {
            return goodsInDetailImageRepository.GetAllEntityByGoodsId(goodsId);
        }
        /// <summary>
        /// 根据id获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GoodsInDetailImage GetSingleGoodsInDetailImage(long id)
        {
            return goodsInDetailImageRepository.GetSingleEntity(id);
        }
    }
}
