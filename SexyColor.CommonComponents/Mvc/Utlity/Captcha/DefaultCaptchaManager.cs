using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace SexyColor.CommonComponents
{
    public class DefaultCaptchaManager : ICaptchaManager
    {
        private string _captchaInputName = "CaptchaInputName";
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="showCaptchaImage">默认是否显示验证码图片</param>
        /// <returns></returns>
        public IHtmlContent GenerateCaptcha<T>(IHtmlHelper<T> htmlHelper, bool showCaptchaImage = false)
        {
            return htmlHelper.EditorForModel("DefaultCaptchaInput", new { InputName = _captchaInputName, ShowCaptchaImage = showCaptchaImage });
        }

        /// <summary>
        /// 验证码是否输入正确
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool IsCaptchaValid(ActionExecutingContext filterContext)
        {
            ControllerBase controllerBase = filterContext.Controller as ControllerBase;
            string captchaText = controllerBase.ControllerContext.HttpContext.Request.Form[_captchaInputName];
            if (string.IsNullOrEmpty(captchaText))
                return false;

            var capchaSettings = CaptchaSettings.Instance();
            string cookieName = capchaSettings.CaptchaCookieName;
            string publicKey = filterContext.HttpContext.Request.Cookies[cookieName];

            string cookieCaptcha = VerificationCodeManager.GetCachedTextAndForceExpire(filterContext.HttpContext, publicKey);
            //从cookie未获取验证码时，提供一个随机数
            if (cookieCaptcha == null)
                cookieCaptcha = DateTime.UtcNow.Ticks.ToString();

            if (!string.IsNullOrEmpty(captchaText) && !captchaText.Equals(cookieCaptcha, StringComparison.CurrentCultureIgnoreCase))
                return false;
            return true;
        }
    }
}
