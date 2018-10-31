using System.IO;
using Microsoft.AspNetCore.Mvc;
using SexyColor.CommonComponents;

namespace SexyColor.Web.Controllers
{
    public class HandlerController : Controller
    {
        public IActionResult CaptchaHttp()
        {
            string cookieName = CaptchaSettings.Instance().CaptchaCookieName;
            int minCharacterCount = CaptchaSettings.Instance().MinCharacterCount;
            int maxCharacterCount = CaptchaSettings.Instance().MaxCharacterCount;
            bool enableLineNoise = CaptchaSettings.Instance().EnableLineNoise;
            CaptchaCharacterSet characterSet = CaptchaSettings.Instance().CharacterSet;
            string generatedKey = string.Empty;
            bool isAddCooikes = false;
            //������ӻ���ȡ��֤��
            string publicKey = null;
            if (HttpContext.Request.Cookies[cookieName] != null)
                publicKey = HttpContext.Request.Cookies[cookieName];

            MemoryStream ms = null;
            if (!string.IsNullOrEmpty(publicKey))
                ms = VerificationCodeManager.GetCachedImageStream(publicKey);

            //��������е������Ѿ������Ƴ�����������ͼƬ����
            if (ms == null)
            {
                //������֤��ͼƬ��������֤�벢�һ�������
                string code =  VerificationCodeManager.GenerateAndCacheImage(out generatedKey);

                ms = VerificationCodeManager.GetCachedImageStream(generatedKey);
                VerificationCodeManager.CacheCode(HttpContext, code, generatedKey);
                isAddCooikes = true;
            }
            if (isAddCooikes)
                HttpContext.Response.Cookies.Append(cookieName, generatedKey);
            byte[] bytes = ms.ToArray();
            Response.Body.Dispose();
            return File(bytes, @"image/jpeg");
        }
    }
}
