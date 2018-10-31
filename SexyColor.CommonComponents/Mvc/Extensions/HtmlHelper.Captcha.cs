using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public static class HtmlHelperCaptchaExtensions
    {
        public static IHtmlContent Captcha<T>(this IHtmlHelper<T> htmlHelper, VerifyScenarios scenarios = VerifyScenarios.Post,
            bool showCaptchaImage = false, string templateName = "Captcha")
        {
            if (!CaptchaUtility.UseCaptcha(scenarios))
                return HtmlString.Empty;
            ICaptchaManager captchaManager = DIContainer.Resolve<ICaptchaManager>();
            IHtmlContent captchaText = captchaManager.GenerateCaptcha(htmlHelper, showCaptchaImage);

            return htmlHelper.EditorForModel(templateName, new { CaptchaText = captchaText });
        }
    }
}
