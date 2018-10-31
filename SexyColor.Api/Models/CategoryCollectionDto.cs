using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Api.Models
{

    public class CategoryDto
    {
        public string cat_name { get; set; } = string.Empty;
        public IEnumerable<CategoryItemDto> sub_category { get; set; } = new List<CategoryItemDto>();
    }

    public class CategoryItemDto
    {
        public string link_url { get; set; } = string.Empty;

        public string cat_name { get; set; } = string.Empty;

        public string cat_img { get; set; } = string.Empty;

        public int is_hot { get; set; } = 0;
    }

    public class GoodsInfoCategoryListDto
    {
        public ScreeningDto screening { get; set; }
 
        public int c_page_next { get; set; }
 
        public int c_total { get; set; }
 
        public int page_total { get; set; }

        public List<GoodsInfoCategoryDto> goods_list { get; set; }

    }

    public class ScreeningDto
    {
        public List<BrandDto> brands { get; set; }
        public List<GradeDto> price_grade { get; set; }
        public List<CharaDto> characteristic { get; set; }
    }

    public class BrandDto
    {
        public int brand_id { get; set; }
        public string brand_name { get; set; }
    }

    public class GradeDto
    {
        public int min { get; set; }
        public int max { get; set; }
        public string price_range { get; set; }
        public int selected { get; set; }
    }

    public class CharaDto
    {
        public int chara { get; set; }
        public string name { get; set; }
    }


    public class GoodsInfoCategoryDto
    {
        public int goods_id { get; set; }

        public string img_url { get; set; }

        public bool is_new { get; set; }

        public bool is_hot { get; set; }

        public bool is_promote { get; set; }

        public bool is_vedio { get; set; }

        public string goods_name { get; set; }

        public decimal app_price { get; set; }

        public int sold_num { get; set; }

        public int comment_num { get; set; }

        public bool is_free_shipping { get; set; } = true;
    }


}
