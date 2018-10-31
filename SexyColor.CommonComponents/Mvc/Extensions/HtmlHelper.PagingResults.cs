using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SexyColor.Infrastructure;
using System.IO;
using System.Text.Encodings.Web;

namespace SexyColor.CommonComponents
{
    public static class HtmlHelperPagingResultsExtesions
    {
        /// <summary>
        /// 呈现分页统计
        /// </summary>
        /// <param name="html">被扩展的HtmlHelper</param>
        /// <param name="pagingDataSet">分页组件</param>
        /// <returns></returns>
        public static IHtmlContent PagingResults(this IHtmlHelper html, IPagingDataSet pagingDataSet)
        {
            TagBuilder builder = new TagBuilder("div");
            builder.AddCssClass("info_box");
            
            long startCount = (pagingDataSet.PageIndex - 1) * pagingDataSet.PageSize + 1;
            builder.InnerHtml.AppendFormat("<span>{0}</span>-", startCount);
            long endCount = pagingDataSet.PageIndex * pagingDataSet.PageSize;
            if (endCount > pagingDataSet.TotalRecords)
                endCount = pagingDataSet.TotalRecords;
            builder.InnerHtml.AppendFormat("<span>{0}</span>&nbsp;共&nbsp;", endCount);
            builder.InnerHtml.AppendFormat("<span>{0}</span>&nbsp;条", pagingDataSet.TotalRecords);
            var writer = new StringWriter();
            builder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
