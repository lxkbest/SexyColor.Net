using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Web
{
    public class PageingModel<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
    }
}
