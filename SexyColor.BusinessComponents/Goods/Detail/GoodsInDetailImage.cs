﻿using MySqlSugar;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品关联详情图
    /// </summary>
    [SugarMapping(TableName = "sc_goods_in_detail_image")]
    [Serializable]
    public class GoodsInDetailImage : IEntity
    {
        #region	构造
        public GoodsInDetailImage()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsInDetailImage New()
        {

            GoodsInDetailImage goodsInDetailImage = new GoodsInDetailImage();
            goodsInDetailImage.Id = 0;
            goodsInDetailImage.Goodsid = 0;
            goodsInDetailImage.GoodsDetaiImage = string.Empty;
            goodsInDetailImage.Number = 0;
            return goodsInDetailImage;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品关联详情标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public long Goodsid { get; set; }

        /// <summary>
        /// 商品轮换图
        /// </summary>
        [SugarMapping(ColumnName = "goods_detai_image")]
        public string GoodsDetaiImage { get; set; }

        /// <summary>
        /// 序号  默认0表示 没有自己插入序号
        /// </summary>
        public int Number { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
