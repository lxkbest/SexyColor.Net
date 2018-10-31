using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SexyColor.CommonComponents
{
    public interface ICaptchaManager
    {
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="showCaptchaImage"></param>
        /// <returns></returns>
        IHtmlContent GenerateCaptcha<T>(IHtmlHelper<T> htmlHelper, bool showCaptchaImage = false);

        /// <summary>
        /// 验证码是否输入正确
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="captchaInputName">输入框名称</param>
        /// <returns></returns>
        bool IsCaptchaValid(ActionExecutingContext filterContext);
    }
}
