using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Api.Models
{
    public class GoodsInfoDto
    {
        public string goods_id { get; set; }
        public string goods_attr { get; set; }
        public string goods_sn { get; set; }
        public string goods_name { get; set; }
        public decimal app_price { get; set; }
        public decimal special_price { get; set; }
        public int special_type { get; set; }
        public int special_id { get; set; }
        public string sort_order { get; set; }
        public string start { get; set; }
        public string recommend { get; set; }
        public string total_comment { get; set; }
        public decimal market_price { get; set; }
        public float discount { get; set; }
        public string img_url { get; set; }
        public string seller_note { get; set; }
        public int difference { get; set; }
        public int goods_number { get; set; }
        public int user_number { get; set; }
        public string sold_number { get; set; }
    }
}
