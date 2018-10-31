using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SexyColor.Infrastructure;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    public static class HtmlHelperPagingExtensions
    {
        /// <summary>
        /// 呈现普通分页按钮
        /// </summary>
        /// <param name="paginationMode">分页按钮显示模式</param>
        /// <param name="html">被扩展的HtmlHelper</param>
        /// <param name="pagingDataSet">数据集</param>
        /// <param name="numericPagingButtonCount">数字分页按钮显示个数</param>
        /// <returns>分页按钮html代码</returns>
        public static IHtmlContent PagingButton(this IHtmlHelper html, IPagingDataSet pagingDataSet, PagingMode pagingMode = PagingMode.NumericNextPrevious, int numericPagingButtonCount = 7)
        {
            return PagingButton(html, pagingDataSet, false, null, pagingMode, numericPagingButtonCount);
        }

        /// <summary>
        /// 呈现分页按钮
        /// </summary>
        /// <param name="html">被扩展的HtmlHelper</param>
        /// <param name="pagingDataSet">数据集</param>
        /// <param name="updateTargetId">异步分页时，被更新的目标元素Id</param>
        /// <param name="paginationMode">分页按钮显示模式</param>
        /// <param name="numericPagingButtonCount">数字分页按钮显示个数</param>
        /// <param name="enableAjax">是否使用ajax分页</param>
        /// <returns>分页按钮html代码</returns>
        private static IHtmlContent PagingButton(this IHtmlHelper html, IPagingDataSet pagingDataSet, bool enableAjax, string updateTargetId, PagingMode pagingMode = PagingMode.NumericNextPrevious, int numericPagingButtonCount = 7, string ajaxUrl = null)
        {
            if (pagingDataSet.TotalRecords == 0 || pagingDataSet.PageSize == 0)
                return HtmlString.Empty;

            //计算总页数
            int totalPages = (int)(pagingDataSet.TotalRecords / (long)pagingDataSet.PageSize);
            if ((pagingDataSet.TotalRecords % pagingDataSet.PageSize) > 0)
                totalPages++;

            //未超过一页时不显示分页按钮
            if (totalPages <= 1)
                return HtmlString.Empty;

            bool showFirst = false;
            if (pagingMode == PagingMode.NextPreviousFirstLast)
                showFirst = true;

            bool showLast = false;
            if (pagingMode == PagingMode.NextPreviousFirstLast)
                showLast = true;

            bool showPrevious = true;
            bool showNext = true;


            bool showNumeric = false;
            if (pagingMode == PagingMode.NumericNextPrevious)
                showNumeric = true;

            //显示多少个数字分页按钮
            //int numericPageButtonCount = 10;

            //对pageIndex进行修正
            if ((pagingDataSet.PageIndex < 1) || (pagingDataSet.PageIndex > totalPages))
                pagingDataSet.PageIndex = 1;

            //string pagingContainer = "<div class=\"tn-pagination-btn\"";
            string pagingContainer = "<ul class=\"pagination\"";
            if (enableAjax)
                pagingContainer += " plugin=\"ajaxPagingButton\" data=\"" + WebUtility.HtmlEncode(JsonConvert.SerializeObject(new { updateTargetId = updateTargetId })) + "\"";
            pagingContainer += ">";

            StringBuilder pagingButtonsHtml = new StringBuilder(pagingContainer);

            //构建 "首页"
            if (showFirst)
            {
                if ((pagingDataSet.PageIndex > 1) && (totalPages > numericPagingButtonCount))
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat("<li>{0}</li>", BuildLink("&lt;&lt;", GetPagingNavigateUrl(html, 1, ajaxUrl), "Previous"));
                }
                else if (pagingMode == PagingMode.NextPreviousFirstLast)
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat("<li class=\"disabled\"><a href=\"###\" aria-label=\"Previous\"><span aria-hidden=\"true\">{0}</span></a></li>", "&lt;&lt;");
                }
            }


            //构建 "上一页"
            if (showPrevious)
            {
                pagingButtonsHtml.AppendLine();
                if (pagingDataSet.PageIndex == 1)
                    pagingButtonsHtml.AppendFormat("<li class=\"disabled\"><a href=\"###\" aria-label=\"Previous\"><span aria-hidden=\"true\">{0}</span></a></li>", "«");
                else
                    pagingButtonsHtml.AppendFormat("<li>{0}</li>", BuildLink("«", GetPagingNavigateUrl(html, pagingDataSet.PageIndex - 1, ajaxUrl), "Previous"));
            }

            //构建 数字分页部分
            if (showNumeric)
            {
                int startNumericPageIndex;
                if (numericPagingButtonCount > totalPages || pagingDataSet.PageIndex - (numericPagingButtonCount / 2) <= 0)
                    startNumericPageIndex = 1;
                else if (pagingDataSet.PageIndex + (numericPagingButtonCount / 2) > totalPages)
                    startNumericPageIndex = totalPages - numericPagingButtonCount + 1;
                else
                    startNumericPageIndex = pagingDataSet.PageIndex - (numericPagingButtonCount / 2);

                if (startNumericPageIndex < 1)
                    startNumericPageIndex = 1;

                int lastNumericPageIndex = startNumericPageIndex + numericPagingButtonCount - 1;
                if (lastNumericPageIndex > totalPages)
                    lastNumericPageIndex = totalPages;

                if (startNumericPageIndex > 1)
                {
                    for (int i = 1; i < startNumericPageIndex; i++)
                    {
                        pagingButtonsHtml.AppendLine();

                        if (i > 3)
                            break;
                        if (i == 3)
                            pagingButtonsHtml.Append("<li><span class=\"\">...</span></li>");
                        else
                        {
                            if (pagingDataSet.PageIndex == i)
                                pagingButtonsHtml.AppendFormat("<li class=\"active\"><a href=\"###\">{0}<span class=\"sr-only\">(current)</span></a></li>", i);
                            else
                            {
                                pagingButtonsHtml.AppendFormat("<li>{0}</li>", BuildLink(i.ToString(), GetPagingNavigateUrl(html, i, ajaxUrl), "Next"));
                            }
                                
                        }
                    }
                }

                for (int i = startNumericPageIndex; i <= lastNumericPageIndex; i++)
                {
                    pagingButtonsHtml.AppendLine();
                    if (pagingDataSet.PageIndex == i)
                        pagingButtonsHtml.AppendFormat("<li class=\"active\"><a href=\"###\">{0}<span class=\"sr-only\">(current)</span></a></li>", i);
                    else
                    {
                        string navigateUrl = GetPagingNavigateUrl(html, i, ajaxUrl);
                        string buildLink = BuildLink(i.ToString(), navigateUrl, "Next");
                        pagingButtonsHtml.AppendFormat("<li>{0}</li>", buildLink);
                    }
                        
                }

                if (lastNumericPageIndex < totalPages)
                {
                    int lastStart = lastNumericPageIndex + 1;
                    if (totalPages - lastStart > 2)
                        lastStart = totalPages - 2;

                    for (int i = lastStart; i <= totalPages; i++)
                    {
                        pagingButtonsHtml.AppendLine();
                        if ((i == lastStart) && (totalPages - lastNumericPageIndex > 3))
                        {
                            pagingButtonsHtml.AppendLine();
                            pagingButtonsHtml.Append("<li><span class=\"\">...</span></li>");
                            continue;
                        }

                        if (pagingDataSet.PageIndex == i)
                            pagingButtonsHtml.AppendFormat("<li class=\"active\"><a href=\"###\">{0}<span class=\"sr-only\">(current)</span></a></li>", i);
                        else
                            pagingButtonsHtml.AppendFormat("<li>{0}</li>", BuildLink(i.ToString(), GetPagingNavigateUrl(html, i, ajaxUrl), "Next"));
                    }
                }

            }

            if (showNext)
            {
                pagingButtonsHtml.AppendLine();
                if (pagingDataSet.PageIndex == totalPages)
                    pagingButtonsHtml.AppendFormat("<li class=\"disabled\"><a href=\"###\" aria-label=\"Previous\"><span aria-hidden=\"true\">{0}</span></a></li>", "»");
                else
                    pagingButtonsHtml.AppendFormat("<li>{0}</li>", BuildLink("»", GetPagingNavigateUrl(html, pagingDataSet.PageIndex + 1, ajaxUrl), "Next"));
            }

            if (showLast)
            {
                if ((pagingDataSet.PageIndex < totalPages) && (totalPages > numericPagingButtonCount))
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat("<li>{0}</li>", BuildLink("&gt;&gt;", GetPagingNavigateUrl(html, totalPages, ajaxUrl), "Next"));
                }
                else if (pagingMode == PagingMode.NextPreviousFirstLast)
                {
                    pagingButtonsHtml.AppendLine();
                    pagingButtonsHtml.AppendFormat("<li class=\"disabled\"><a href=\"###\" aria-label=\"Previous\"><span aria-hidden=\"true\">{0}</span></a></li>", "&gt;&gt;");
                }
            }
            pagingButtonsHtml.Append("</ul>");
            return new HtmlString(pagingButtonsHtml.ToString());
        }

        /// <summary>
        /// 生成带Href的链接
        /// </summary>
        private static string BuildLink(string linkText, string url, string cssClassName = "Previous")
        {
            return string.Format("<a href=\"{0}\" aria-label=\"{1}\"><span aria-hidden=\"true\">{2}</span></a>", url, string.IsNullOrEmpty(cssClassName) ? string.Empty : cssClassName, linkText);
        }

        /// <summary>
        /// 构建分页按钮的链接
        /// </summary>
        /// <param name="htmlHelper">被扩展的HtmlHelper</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>分页按钮的url字符串</returns>
        public static string GetPagingNavigateUrl(this IHtmlHelper htmlHelper, int pageIndex, string currentUrl = null)
        {
            object pageIndexObj = null;
            if (htmlHelper.ViewContext.RouteData.Values.TryGetValue("pageIndex", out pageIndexObj))
            {
                htmlHelper.ViewContext.RouteData.Values["pageIndex"] = pageIndex;
                var urlHelper =  DIContainer.Resolve<IUrlHelper>();
                return urlHelper.RouteUrl(htmlHelper.ViewContext.RouteData.Values);
            }

            if (string.IsNullOrEmpty(currentUrl))
                currentUrl = WebUtility.HtmlEncode($"{htmlHelper.ViewContext.HttpContext.Request.Scheme}://{htmlHelper.ViewContext.HttpContext.Request.Host}{htmlHelper.ViewContext.HttpContext.Request.Path}{htmlHelper.ViewContext.HttpContext.Request.QueryString}");

            if (currentUrl.IndexOf("?") == -1)
            {
                return currentUrl + string.Format("?pageIndex={0}", pageIndex);
            }
            else
            {
                if (currentUrl.IndexOf("pageIndex=", StringComparison.CurrentCultureIgnoreCase) == -1)
                    return currentUrl + string.Format("&pageIndex={0}", pageIndex);
                else
                    return Regex.Replace(currentUrl, @"pageIndex=(\d+\.?\d*|\.\d+)", "pageIndex=" + pageIndex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }
    }

    /// <summary>
    /// 分页按钮显示模式
    /// </summary>
    public enum PagingMode
    {
        /// <summary>
        /// 上一页/下一页 模式
        /// </summary>
        NextPrevious,

        /// <summary>
        /// 首页/末页/上一页/下一页 模式
        /// </summary>
        NextPreviousFirstLast,

        /// <summary>
        /// 上一页/下一页 + 数字 模式，例如： 上一页 1 2 3 4 5 下一页
        /// </summary>
        NumericNextPrevious,
    }
}
