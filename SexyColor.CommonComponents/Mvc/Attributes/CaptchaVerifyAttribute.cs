using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.CommonComponents
{
    public class CaptchaVerifyAttribute : ActionFilterAttribute
    {
        private const string CaptchaVerifyError = "验证码输入错误";
        private VerifyScenarios scenarios = VerifyScenarios.Post;

        /// <summary>
        /// 带参构造器
        /// </summary>
        /// <param name="scenarios">验证码使用场景</param>
        public CaptchaVerifyAttribute(VerifyScenarios scenarios = VerifyScenarios.Post)
        {
            this.scenarios = scenarios;
        }

        /// <summary>
        /// 执行Action时
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CaptchaUtility.UseCaptcha(scenarios, true))
            {
                try
                {
                    ICaptchaManager captchaManager = DIContainer.Resolve<ICaptchaManager>();
                    Controller controllerBase = filterContext.Controller as Controller;
                    if (!captchaManager.IsCaptchaValid(filterContext))
                    {
                        //controllerBase.ModelState.AddModelError("Captcha", CaptchaVerifyError);
                        controllerBase.ViewData["UserLoginStatus"] = UserLoginStatus.CapCode;
                    }
                    else if (controllerBase.ModelState.IsValid)
                    {
                        //表单通过验证时，重设累计次数
                        //CaptchaUtility.ResetLimitTryCount(scenarios);
                        //此处修改成登录成功，重设累计次数
                    }
                }
                catch
                {
                    throw new Exception("检查验证码时，出现异常");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
