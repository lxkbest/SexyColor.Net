using System;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace SexyColor.BusinessComponents
{
    public class GoodsInfoService : IGoodsInfoService
    {
        public IGoodsInfoRepository goodsInfoRepository { get; set; }
        public IGoodsSkuInfoRepository goodsSkuInfoRepository { get; set; }
        public IGoodsInRotationImageRepository goodsInRotationImageRepository { get; set; }
        public IGoodsSkuInoutRepository goodsSkuInoutRepository { get; set; }

        /// <summary>
        /// 后台商品信息分页
        /// </summary>
        /// <param name="goodsInfoQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsInfo> GetPageGoodsInfo(GoodsInfoQuery goodsInfoQuery, int pageIndex, int pageSize)
        {
            return goodsInfoRepository.GetPageGoodsInfo(goodsInfoQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// API商品信息分页数据
        /// </summary>
        /// <param name="parsms"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<GoodsInfo> GetPageGoodsInfoAPI(Dictionary<string, object> parsms, string orderBy, int pageIndex, int pageSize)
        {
            return goodsInfoRepository.GetPageGoodsInfoAPI(parsms, orderBy, pageIndex, pageSize);
        }


        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public GoodsInfo GetGoodsInfo(long goodsId)
        {
            return goodsInfoRepository.GetByCache(w => w.Goodsid == goodsId, goodsId);
        }

        /// <summary>
        /// 批量回收商品
        /// </summary>
        /// <param name="goodsIds"></param>
        public void RecycleGoods(IEnumerable<long> goodsIds)
        {
            foreach (var goodsId in goodsIds)
            {
                GoodsInfo goodsInfo = goodsInfoRepository.GetByCache(w => w.Goodsid == goodsId, goodsId);
                if (goodsInfo == null)
                    continue;
                goodsInfo.IsHot = false;
                goodsInfo.IsEnable = false;
                goodsInfo.IsVisible = false;
                goodsInfo.Status = (int)GoodsInfoStatus.RecycleBin;
                goodsInfoRepository.UpdateCache(goodsInfo);
            }
        }

        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <param name="goodsId"></param>
        public void RecoveryGoods(long goodsId)
        {
            GoodsInfo entity =  GetGoodsInfo(goodsId);
            if (entity != null && entity.Goodsid > 0)
            {
                entity.IsHot = false;
                entity.IsEnable = false;
                entity.IsVisible = true;
                entity.Status = (int)GoodsInfoStatus.Shelves;
                goodsInfoRepository.UpdateCache(entity);
            }
        }

        /// <summary>
        /// 上下架商品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public GoodsInfo UpDownShelvesGoods(GoodsInfo entity, bool isUpDown)
        {
            if (entity == null && entity.Goodsid > 0)
                return null;

            if (!isUpDown)
            {
                entity.IsEnable = false;
                entity.IsVisible = false;
                entity.Status = (int)GoodsInfoStatus.Shelves;
            }
            else
            {
                entity.IsEnable = true;
                entity.IsVisible = true;
                entity.Status = (int)GoodsInfoStatus.Sale;
            }
            var result = goodsInfoRepository.UpdateCache(entity);
            if (result)
                return entity;
            return null;
        }

        /// <summary>
        /// 发布商品
        /// </summary>
        /// <param name="infoEntity"></param>
        /// <param name="skuEntitys"></param>
        /// <param name="imgEntitys"></param>
        /// <returns></returns>
        public bool ReleaseGoods(GoodsInfo infoEntity, List<GoodsSkuInfo> skuEntitys, List<GoodsInRotationImage> imgEntitys, string[] detailsImgArr, long userId)
        {
            if (infoEntity == null || skuEntitys == null || imgEntitys == null)
                return false;
            if (skuEntitys.Count <= 0 || imgEntitys.Count <= 0)
                return false;


            //infoEntity.Goodsid = long.Parse($"{new Random().Next(1001, 9999)}{IdGenerator.Next()}");
            infoEntity.Goodsid = long.Parse($"{new Random().Next(1001, 9999)}{DateTime.Now.ToString("yyMMdd")}");

            infoEntity.Stock = skuEntitys.Sum(w => w.Stock);

            var result = goodsInfoRepository.ReleaseGoods(infoEntity, imgEntitys, detailsImgArr);
            if (result)
            {
                goodsInRotationImageRepository.InsertCacheByEntitys(imgEntitys);
                InoutGoodsLog(skuEntitys, infoEntity, userId);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sku规格入库
        /// </summary>
        /// <param name="skuEntitys"></param>
        /// <param name="goodsId"></param>
        private void InoutGoodsLog(List<GoodsSkuInfo> skuEntitys, GoodsInfo infoEntity, long userId)
        {
            if (skuEntitys != null && infoEntity.Goodsid > 0)
            {
                List<GoodsSkuInout> inoutEntitys = new List<GoodsSkuInout>();
                foreach (var entity in skuEntitys)
                {
                    GoodsSkuInout inoutEntity = GoodsSkuInout.New();
                    entity.Goodsid = infoEntity.Goodsid;
                    object objId = goodsSkuInfoRepository.AddByCache(entity, true);
                    var skuId = 0;
                    int.TryParse(objId.ToString(), out skuId);
                    inoutEntity.Goodsid = infoEntity.Goodsid;
                    inoutEntity.Skuid = skuId;
                    inoutEntity.InoutNumber = entity.Stock;
                    inoutEntity.IsOut = false;
                    inoutEntity.DateCreated = DateTime.Now;
                    inoutEntity.Operation = string.Format("管理用户:{0}、发布商品属性:“{1}”、规格:“{2}”、入库{3}、 时间：{4}", userId, infoEntity.GoodsName, entity.SkuName, inoutEntity.InoutNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    inoutEntitys.Add(inoutEntity);
                }
                goodsSkuInoutRepository.AddByCache(inoutEntitys);
            }
        }

        /// <summary>
        /// 设置商品基本信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        public void SetGoodsInfo(GoodsInfo entity, out SetGoodsInfoStatus status)
        {
            status = SetGoodsInfoStatus.UnknownFailure;
            if (entity == null || entity.Goodsid > 0)
                status = SetGoodsInfoStatus.DataCatch;

            var result = goodsInfoRepository.UpdateCache(entity, 
                new {
                    goods_name = entity.GoodsName,
                    category_level2_id = entity.CategoryLevel2Id,
                    subject_title = entity.SubjectTitle,
                    buy_count = entity.BuyCount,
                    real_buy_count = entity.RealBuyCount,
                    place_origin = entity.PlaceOrigin,
                    freight = entity.Freight,
                    stock = entity.Stock,
                    status = entity.Status,
                    is_enable = entity.IsEnable,
                    is_visible = entity.IsVisible,
                    limit_purchase_count = entity.LimitPurchaseCount,
                    display_order = entity.DisplayOrder,
                    is_hot = entity.IsHot,
                    goods_price = entity.GoodsPrice,
                    goods_real_price = entity.GoodsRealPrice
                },
                w => w.Goodsid == entity.Goodsid);
            if (result)
                status = SetGoodsInfoStatus.Success;

        }

        /// <summary>
        /// 设置商品属性
        /// </summary>
        public bool SetGoodsSkuInfo(GoodsInfo entity, List<GoodsSkuInfo> skuEntitys, long userId)
        {
            var errorCount = 0;
            List<GoodsSkuInout> inoutEntitys = new List<GoodsSkuInout>();
            var result = goodsInfoRepository.UpdateCache(entity);
            if (result)
            {
                goodsSkuInfoRepository.UpdateCacheByParame("Goodsid", entity.Goodsid);
                foreach (var skuEntity in skuEntitys)
                {
                    GoodsSkuInout inoutEntity = GoodsSkuInout.New();
                    inoutEntity.Goodsid = entity.Goodsid;
                    inoutEntity.DateCreated = DateTime.Now;
                    GoodsSkuInfo goodsSkuInfo = goodsSkuInfoRepository.GetByCache(w => w.Skuid == skuEntity.Skuid, skuEntity.Skuid);
                    if (goodsSkuInfo != null && goodsSkuInfo.Skuid > 0)
                    {
                        if (goodsSkuInfo.Stock != skuEntity.Stock)
                        {
                            inoutEntity.Skuid = skuEntity.Skuid;
                            inoutEntity.IsOut = goodsSkuInfo.Stock - skuEntity.Stock > 0 ? true : false;
                            inoutEntity.InoutNumber = Math.Abs(goodsSkuInfo.Stock - skuEntity.Stock);
                            var formatOut = inoutEntity.IsOut ? "出库" + inoutEntity.InoutNumber : "入库" + inoutEntity.InoutNumber;
                            inoutEntity.Operation = string.Format("管理用户:{0}、编辑商品属性:“{1}”、规格:“{2}”、{3} 时间：{4}", userId, entity.GoodsName, skuEntity.SkuName, formatOut, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            inoutEntitys.Add(inoutEntity);

                            result = goodsSkuInfoRepository.UpdateByEntity(skuEntity);
                            if (result)
                                if (!goodsSkuInoutRepository.AddByCache(inoutEntitys))
                                    errorCount++;
                                else
                                {
                                    if (!inoutEntity.IsOut)
                                        entity.Stock += inoutEntity.InoutNumber;
                                    else
                                    {
                                        if (entity.Stock - inoutEntity.InoutNumber < 0)
                                            entity.Stock = 0;
                                        else
                                            entity.Stock -= inoutEntity.InoutNumber;
                                    }
                                    
                                    if (!goodsInfoRepository.UpdateCache(entity, new { stock = entity.Stock }, w => w.Goodsid == entity.Goodsid))
                                        errorCount++;
                                }

                        }
                        else
                        {
                            if (!goodsSkuInfoRepository.UpdateByEntity(skuEntity))
                                errorCount++;
                        }
                    }
                    else
                    {
                        object objId = goodsSkuInfoRepository.AddByCache(skuEntity, true);
                        var skuId = 0;
                        int.TryParse(objId.ToString(), out skuId);
                        inoutEntity.Skuid = skuId;
                        inoutEntity.IsOut = false;
                        inoutEntity.InoutNumber = skuEntity.Stock;
                        inoutEntity.Operation = string.Format("管理用户:{0}、新增商品属性:“{1}”、规格:“{2}”、入库{3}  时间：{4}", userId, entity.GoodsName, skuEntity.SkuName, inoutEntity.InoutNumber, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        inoutEntitys.Add(inoutEntity);
                        if (!goodsSkuInoutRepository.AddByCache(inoutEntitys))
                            errorCount++;
                        else
                        {
                            entity.Stock += skuEntity.Stock;
                            if (!goodsInfoRepository.UpdateCache(entity, new { stock = entity.Stock }, w => w.Goodsid == entity.Goodsid))
                                errorCount++;
                        }
                    }
                }
            }
            return errorCount == 0 ? true : false;
        }

        /// <summary>
        /// 获取top条数的热门商品
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IEnumerable<GoodsInfo> GetGoodsTopNumberByHot(int top)
        {
            if (top <= 0)
                return null;
            var isHot = true;
            return goodsInfoRepository.GetGoodsTopNumberByHot(top, isHot);
        }
    }
}
