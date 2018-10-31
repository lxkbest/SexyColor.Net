using SexyColor.BusinessComponents;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class HomeDynamicModel
    {

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 文本文字
        /// </summary>
        public string LabelText { get; set; }

        /// <summary>
        /// 重定向Url
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 将HomeDynamicModel转换成HomeDynamicSettings
        /// </summary>
        /// <returns></returns>
        public HomeDynamicSettings AsHomeDynamic()
        {
            HomeDynamicSettings dynamic = HomeDynamicSettings.New();
            dynamic.ImageUrl = ImageUrl;
            dynamic.Type = Type;
            dynamic.RedirectUrl = RedirectUrl;
            dynamic.Id = Id;
            dynamic.LabelText = LabelText;
            return dynamic;
        }

        /// <summary>
        /// 将HomeDynamicSettings转换成HomeDynamicModel
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HomeDynamicModel ToHomeDynamicModel(HomeDynamicSettings item)
        {
            ImageUrl = item.ImageUrl;
            Type = item.Type;
            RedirectUrl = item.RedirectUrl;
            Id = item.Id;
            LabelText = item.LabelText;
            return this;
        }


    }
}
