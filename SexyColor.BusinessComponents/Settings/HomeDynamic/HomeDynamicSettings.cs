using System;
using SexyColor.Infrastructure;
using MySqlSugar;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:操作记录
    /// </summary>
    [SugarMapping(TableName = "sc_home_dynamic_settings")]
    public class HomeDynamicSettings : IEntity
    {
        #region	构造
        public HomeDynamicSettings()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static HomeDynamicSettings New()
        {
            HomeDynamicSettings homeDynamicSettings = new HomeDynamicSettings();
            homeDynamicSettings.Id = 0;
            homeDynamicSettings.Type = 0;
            homeDynamicSettings.ImageUrl = string.Empty;
            homeDynamicSettings.LabelText = string.Empty;
            homeDynamicSettings.Userid = 0;
            homeDynamicSettings.DateCreated = DateTime.Now;
            homeDynamicSettings.DateLastModified = DateTime.Now;
            homeDynamicSettings.RedirectUrl = string.Empty;
            homeDynamicSettings.JsonObj = string.Empty;
            homeDynamicSettings.DisplayOrder = 0;

            homeDynamicSettings.GoodsPrice = string.Empty;
            homeDynamicSettings.ParentsId = string.Empty;
            return homeDynamicSettings;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 首页动态设置标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 展位类型  1.轮换，2分类，3其他
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 图片url
        /// </summary>
        [SugarMapping(ColumnName = "image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 文本文字
        /// </summary>
        [SugarMapping(ColumnName = "label_text")]
        public string LabelText { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [SugarMapping(ColumnName = "date_last_modified")]
        public DateTime DateLastModified { get; set; }

        /// <summary>
        /// 重定向Url
        /// </summary>
        [SugarMapping(ColumnName = "redirect_url")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Json对象字符串
        /// </summary>
        [SugarMapping(ColumnName = "json_obj")]
        public string JsonObj { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarMapping(ColumnName = "display_order")]
        public int DisplayOrder { get; set; }

        [SugarMapping(ColumnName = "goods_price")]
        public string GoodsPrice { get; set; }

        [SugarMapping(ColumnName = "parents_id")]
        public string ParentsId { get; set; }
        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion

    }
}
