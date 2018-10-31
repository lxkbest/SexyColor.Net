using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Api.Models
{
 
    public class PageingDto
    {
        /// <summary>
        /// 下一页链接
        /// </summary>
        public string next_url { get; set; }

        /// <summary>
        /// 下一页
        /// </summary>
        public string c_page_next { get; set; }

        /// <summary>
        /// 总记录
        /// </summary>
        public int c_total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int page_total { get; set; }
    }
 
}
