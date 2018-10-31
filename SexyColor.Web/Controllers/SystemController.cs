using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using Microsoft.AspNetCore.Authorization;
using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.Web.Controllers
{
    [Authorize(Policy = "EnterSystem")]
    public class SystemController : Controller
    {
        public IMembershipService membershipService { get; set; }
        public IUserService userService { get; set; }
        public UserProfilesService userProfilesService { get; set; }
        

        /// <summary>
        /// 后台主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Home()
        {
            var userName = User.Identity.Name;
            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            return View();
        }

        /// <summary>
        /// 后台登录页面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ManageLogin(string returnUrl)
        {
            ViewData["title"] = "登录";
            return View(new LoginModel { loginInModal = false, ReturnUrl = returnUrl, UserName = "admin", Password = "123456" });
        }

        /// <summary>
        /// 后台登录验证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CaptchaVerify(VerifyScenarios.Login)]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageLogin(LoginModel model)
        {
            var resutl = TryValidateModel(model);

            if (!ModelState.IsValid)
            {
                model.Password = string.Empty;
                return View(model);
            }
            var loginStatus = ViewData.Get<UserLoginStatus>("UserLoginStatus", UserLoginStatus.Success);
            if (loginStatus == UserLoginStatus.CapCode)
            {
                ViewData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Error, "验证码错误，请重新输入！");
                return View(model);
            }

            User user = model.AsUser();
            //首先验证用户名匹配密码
            loginStatus = membershipService.ValidateUser(user.UserName, user.Password);
            if (loginStatus == UserLoginStatus.InvalidCredentials)
            {
                //不匹配，验证邮箱匹配密码
                User userEmail = userService.FindUserByEmail(user.UserName);
                if (userEmail != null)
                {
                    user = userEmail as User;
                    loginStatus = membershipService.ValidateUser(userEmail.UserName, model.Password);
                }
                if (loginStatus == UserLoginStatus.InvalidCredentials)
                {
                    //不匹配，验证手机号码匹配密码
                    User userMobile = userService.FindUserByMobile(user.UserName);
                    if (userMobile != null)
                    {
                        user = userMobile as User;
                        loginStatus = membershipService.ValidateUser(userMobile.UserName, model.Password);
                    }
                }
            }
            else
            {
                //匹配成功获取完整用户
                user = userService.GetFullUser(user.UserName);
            }
            


            // 不匹配就提示账户或密码错误
            if (loginStatus == UserLoginStatus.InvalidCredentials)
                ViewData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Error, "帐号或密码错误，请重新输入！");
            else if (loginStatus == UserLoginStatus.NotActivated)
                ViewData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Error, "账号未激活，请等待激活！");
            else if (loginStatus == UserLoginStatus.Banned)
                ViewData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Error, "账号被封禁，请联系管理员！");
            // 匹配成功
            else if (loginStatus == UserLoginStatus.Success)
            {
                CaptchaUtility.ResetLimitTryCount(VerifyScenarios.Login);
                //验证此账号是否有访问后台权限
                if (user.IsAllowEntryManager())
                {
                    var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName)
                        }, "Forms");
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.Authentication.SignInAsync("Sexy.Cookie", principal, new AuthenticationProperties
                    { IsPersistent = true, AllowRefresh = false, ExpiresUtc = DateTime.UtcNow.AddMinutes(30) });
                    HttpContext.Session.Set<User>(user.UserId.ToString(), user);


                }
                else
                {
                    ViewData["StatusMessageData"] = new StatusMessageData(StatusMessageType.Error, "此账号无权限访问后台！");
                    return View(model);
                }

                string redirectUrl = null;
                if (!string.IsNullOrEmpty(model.ReturnUrl))
                    redirectUrl = model.ReturnUrl;
                else
                    redirectUrl = NavigationUrls.Instance().ManageHome();
                return Redirect(redirectUrl);
            }
            return View(model);

        }
        /// <summary>
        /// 后台登出
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> ManageLogout(string returnUrl)
        {
            await HttpContext.Authentication.SignOutAsync("Sexy.Cookie");
            PermissionsCollction.Clear();
            return Redirect(returnUrl ?? "~/");
        }

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ValidateUserName(string userName)
        {
            string errorMessage;
            bool valid = Utility.ValidateUserName(userName, out errorMessage);
            if (valid)
                return Json(true);
            return Json(errorMessage);
        }


        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ValidatePassword(string password)
        {
            string errorMessage;
            bool valid = Utility.ValidatePassword(password, out errorMessage);
            if (valid)
                return Json(true);
            return Json(errorMessage);
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="accountEmail">邮箱</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ValidateEmail(string accountEmail)
        {
            string errorMessage;
            bool valid = Utility.ValidateEmail(accountEmail, out errorMessage);
            if (valid)
                return Json(true);
            return Json(errorMessage);

        }

        /// <summary>
        /// 验证用户昵称
        /// </summary>
        /// <param name="nickName">用户昵称</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ValidateNickName(string nickName)
        {
            string errorMessage;
            bool valid = Utility.ValidateNickName(nickName, out errorMessage);
            if (valid)
                return Json(true);
            return Json(errorMessage);
        }

        /// <summary>
        /// 用户角色-验证角色名称
        /// </summary>
        /// <param name="userName">角色名称</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ValidateRolesName(string roleName)
        {
            string errorMessage;
            bool valid = Utility.ValidateRolesName(roleName, out errorMessage);
            if (valid)return Json(true);
            return Json(errorMessage);
        }
    }
}
