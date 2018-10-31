using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品类型
    /// </summary>
    [CacheSetting(true, ExpirationPolicy = CachingExpirationType.Stable)]
    [SugarMapping(TableName = "sc_goods_type")]
    [Serializable]
    public class GoodsType : IEntity
    {
        #region	构造
        public GoodsType()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsType New()
        {

            GoodsType goodsType = new GoodsType();
            goodsType.GoodsTypeId = 0;
            goodsType.GoodsTypeName = string.Empty;
            goodsType.DisplayOrder = 0;
            goodsType.Description = string.Empty;
            return goodsType;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 商品类型Id
        /// </summary>
        [SugarMapping(ColumnName = "goods_type_id")]
        public int GoodsTypeId { get; set; }

        /// <summary>
        /// 商品类型名称
        /// </summary>
        [SugarMapping(ColumnName = "goods_type_name")]
        public string GoodsTypeName { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        [SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarMapping(ColumnName = "description")]
        public string Description { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => GoodsTypeId; }
        #endregion
    }
}
