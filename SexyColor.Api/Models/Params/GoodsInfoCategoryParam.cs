using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Api.Models
{
    public class GoodsInfoCategoryParam
    {
        /// <summary>
        /// 分类项
        /// </summary>
        public int CategoryItemid { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public GoodsInfoCategoryAPISortBy Sort { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public int Brandsid { get; set; }
        /// <summary>
        /// 特征
        /// </summary>
        public int Charaid { get; set; }
        /// <summary>
        /// 价格上限
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 价格下限
        /// </summary>
        public int Min { get; set; }
    }


    public enum GoodsInfoCategoryAPISortBy
    {
        /// <summary>
        /// 综合
        /// </summary>
        Default,
        /// <summary>
        /// 销量
        /// </summary>
        Sale,
        /// <summary>
        /// 价格 降序
        /// </summary>
        Peice_Desc,
        /// <summary>
        /// 价格 升序
        /// </summary>
        Peice_Asc
    }
}
