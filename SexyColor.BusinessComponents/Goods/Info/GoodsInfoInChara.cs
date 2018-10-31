using MySqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{

    [SugarMapping(TableName = "sc_goods_in_chara")]
    [Serializable]
    public class GoodsInfoInChara
    {
        #region	构造
        public GoodsInfoInChara()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsInfoInChara New()
        {
            GoodsInfoInChara goodsInfoInChara = new GoodsInfoInChara();
            goodsInfoInChara.Id = 0;
            goodsInfoInChara.Goodsid = 0;
            goodsInfoInChara.Charaid = 0;
            return goodsInfoInChara;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Goodsid { get; set; }
        /// <summary>
        /// 特点id
        /// </summary>
        public int Charaid { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
