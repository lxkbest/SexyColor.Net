using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Api.Models
{
    /// <summary>
    /// APIDto
    /// </summary>
    public class SitesDto
    {
        public IEnumerable<HomeDynamicSettingsDto> top_ads = new List<HomeDynamicSettingsDto>();
        public IEnumerable<HomeDynamicSettingsDto> hot_list = new List<HomeDynamicSettingsDto>();
        public IEnumerable<FloorAreaDto> other_recommend_list = new List<FloorAreaDto>();
        public IEnumerable<GoodsInfoDto> product_recommendation = new List<GoodsInfoDto>();

    }

    /// <summary>
    /// 首页动态Dto
    /// </summary>
    public class HomeDynamicSettingsDto
    {
        /// <summary>
        /// 链接url
        /// </summary>
        public string link_url { get; set; } = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public string main_title { get; set; } = string.Empty;
        /// <summary>
        /// 副标题
        /// </summary>
        public string subtitle { get; set; } = string.Empty;
        /// <summary>
        /// 图片url
        /// </summary>
        public string img_url { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public int type { get; set; } = 0;
    }

    /// <summary>
    /// 楼层专区Dto
    /// </summary>
    public class FloorAreaDto
    {
        public string main_title { get; set; }

        public string subtitle { get; set; }

        public string link_url { get; set; }
        
        public int type { get; set; }
        [JsonIgnore]
        public int id { get; set; }

        public IEnumerable<HomeDynamicSettingsDto> ad_list { get; set; }
    }

}
