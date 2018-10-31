using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public static class HtmlHelperMessageExtensions
    {
        /// <summary>
        /// 操作结果提示信息
        /// </summary>
        /// <param name="statusMessageData">参数为null时会自动检查ViewData和TempData是否有Key=“StatusMessageData”对应的值</param>
        /// <param name="hintMillisecondForHide">是否自动隐藏的秒数默认为2秒,小于0时不自动隐藏</param>
        /// <returns></returns>
        public static IHtmlContent StatusMessage(this IHtmlHelper htmlHelper, StatusMessageData statusMessageData = null, int hintMillisecondForHide = 2)
        {
            if (statusMessageData == null)
            {
                if (htmlHelper.ViewData != null)
                {
                    statusMessageData = htmlHelper.ViewData.Get<StatusMessageData>("StatusMessageData", null);
                }

                if (statusMessageData == null && htmlHelper.ViewContext.TempData != null)
                {
                    statusMessageData = htmlHelper.ViewContext.TempData.Get<StatusMessageData>("StatusMessageData", null);
                    if (statusMessageData != null)
                        htmlHelper.ViewContext.TempData.Remove("StatusMessageData");
                }
            }
            if (statusMessageData == null)
                return HtmlString.Empty;

            htmlHelper.ViewData["StatusMessageData"] = statusMessageData;
            htmlHelper.ViewData["HintMillisecondForHide"] = hintMillisecondForHide;
            return htmlHelper.DisplayForModel("StatusMessage");
        }
    }
}
