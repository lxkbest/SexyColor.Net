using System;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public class GoodsSkuInfoService : IGoodsSkuInfoService
    {
        public IGoodsInfoRepository goodsInfoRepository { get; set; }
        public IGoodsSkuInfoRepository goodsSkuInfoRepository { get; set; }
        public IGoodsSkuInoutRepository goodsSkuInoutRepository { get; set; }



        public GoodsSkuInfo GetGoodsSkuInfo(long skuid)
        {
            return goodsSkuInfoRepository.GetByCache(w => w.Skuid == skuid, skuid);
        }

        /// <summary>
        /// 根据商品编号获取商品属性列表
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IEnumerable<GoodsSkuInfo> GetGoodsSkuInfoByGoodsId(long goodsId)
        {
            return goodsSkuInfoRepository.GetGoodsSkuInfoByGoodsId(goodsId);
        }

        /// <summary>
        /// 修改商品属性状态
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ModifySkuStatus(int skuId, int checkSkuId, long userId, GoodsSkuInfoStatus status)
        {
            if (skuId <= 0)
                return false;
            //需要逻辑删除的商品属性
            GoodsSkuInfo skuEntity = goodsSkuInfoRepository.GetByCache(w => w.Skuid == skuId, skuId);
            if (skuEntity == null && skuEntity.Skuid > 0)
                return false;
            skuEntity.Status = (int)status;
            skuEntity.IsDefault = false;
            //商品属性对应的商品信息
            GoodsInfo infoEntity = goodsInfoRepository.GetByCache(w => w.Goodsid == skuEntity.Goodsid, skuEntity.Goodsid);
            if (infoEntity == null && infoEntity.Goodsid > 0)
                return false;

            //需要默认到商品信息上的商品属性
            GoodsSkuInfo checkEntity = goodsSkuInfoRepository.GetByCache(w => w.Skuid == checkSkuId, checkSkuId);
            if (checkEntity != null && checkEntity.Skuid > 0)
            {
                //将默认信息更新到商品上显示
                infoEntity.ImageUrl = checkEntity.SkuImage;
                infoEntity.GoodsPrice = checkEntity.SkuMaketPrice;
                infoEntity.GoodsRealPrice = checkEntity.SkuFactoryPrice;
            }
            //更新商品总库存
            infoEntity.Stock = infoEntity.Stock - skuEntity.Stock;
            //添加商品出库记录
            GoodsSkuInout inoutEntity = GoodsSkuInout.New();
            inoutEntity.Goodsid = skuEntity.Goodsid;
            inoutEntity.Skuid = skuEntity.Skuid;
            inoutEntity.InoutNumber = skuEntity.Stock;
            inoutEntity.IsOut = true;
            inoutEntity.DateCreated = DateTime.Now;
            //操作记录以后调整为配置数据获取模板方式
            inoutEntity.Operation = string.Format("管理用户:{0}、删除商品属性:“{1}”、规格:“{2}”、损耗库存{3}、时间：{4}", userId, infoEntity.GoodsName, skuEntity.SkuName, inoutEntity.InoutNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            var result = goodsSkuInfoRepository.ModifySkuStatus(skuEntity, checkEntity, infoEntity, inoutEntity);
            if (result)
            {
                //删除成功后更新缓存
                goodsInfoRepository.UpdateCacheByEntity(infoEntity);
                goodsSkuInoutRepository.InsertCacheByEntity(inoutEntity);
            }
            return result;
        }
    }
}
