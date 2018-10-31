
using System.ComponentModel.DataAnnotations;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// 商品状态
    /// </summary>
    public enum GoodsInfoStatus
    {
        /// <summary>
        /// 待上架
        /// </summary>
        [Display(Name = "待上架")]
        Shelves = 1,

        /// <summary>
        /// 售卖中
        /// </summary>
        [Display(Name = "售卖中")]
        Sale = 2,

        /// <summary>
        /// 缺少库存被下架
        /// </summary>
        [Display(Name = "缺少库存被下架")]
        MissStock = 3,

        /// <summary>
        /// 手动下架
        /// </summary>
        [Display(Name = "手动下架")]
        ManualShelves = 4,

        /// <summary>
        /// 回收站（删除后状态）
        /// </summary>
        [Display(Name = "回收站（删除后状态）")]
        RecycleBin = 5,
    }

    /// <summary>
    /// 商品上下架
    /// </summary>
    public enum GoodsInfoEnable
    {
        /// <summary>
        /// 下架
        /// </summary>
        [Display(Name = "下架")]
        Down = 0,


        /// <summary>
        /// 上架
        /// </summary>
        [Display(Name = "上架")]
        Up = 1,
    }

    /// <summary>
    /// 商品是否热门
    /// </summary>
    public enum GoodsInfoHot
    {
        /// <summary>
        /// 否
        /// </summary>
        [Display(Name = "否")]
        NO = 0,


        /// <summary>
        /// 是
        /// </summary>
        [Display(Name = "是")]
        YES = 1,
    }

    /// <summary>
    /// 商品是否前台可见
    /// </summary>
    public enum GoodsInfoVisible
    {
        /// <summary>
        /// 否
        /// </summary>
        [Display(Name = "否")]
        NO = 0,


        /// <summary>
        /// 是
        /// </summary>
        [Display(Name = "是")]
        YES = 1,
    }

    public enum SetGoodsInfoStatus
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownFailure = 0,

        /// <summary>
        /// 数据异常
        /// </summary>
        DataCatch = 1,

        /// <summary>
        /// 处理成功
        /// </summary>
        Success = 2
    }
}
