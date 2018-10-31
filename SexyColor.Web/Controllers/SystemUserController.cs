using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using SexyColor.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.Drawing.Imaging;

namespace SexyColor.Web.Controllers
{
    [Authorize(Policy = "EnterSystem")]
    [Permissions]
    public class SystemUserController : Controller
    {
        //角色服务
        public RolesService rolesService { get; set; }
        //用户服务
        public IUserService userService { get; set; }
        //会员服务
        public IMembershipService membershipService { get; set; }
        //权限服务
        public IPermissionItemsService permissionItemsService { get; set; }
        //用户地址
        public IUserAddressService userAddressService { get; set; }
        //用户资料服务
        public UserProfilesService userProfilesService { get; set; }
        //用户粉丝服务
        public IFollowsService followsService { get; set; }
        //授权助手
        public AuthorizerHelper authorizerHelper { get; set; }


        #region 用户管理

        /// <summary>
        /// 管理用户
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [OperationLogs(false, Description = "可以管理用户相关的操作")]
        public IActionResult ManageUser(int? pageIndex, int pageSize = 20)
        {
            var from = RequestHelper.GetQueryParams(Request);
            ViewData["currentUser"] = UserContext.CurrentUser;

            UserQuery userQuery = new UserQuery();
            userQuery.Keyword = from["keyWord"];
            userQuery.AccountEmailFilter = from["accountEmail"];
            userQuery.RoleName = from["rolename"];

            var isActivated = from["isActivated"];
            var isModerated = from["isModerated"];
            var isBanned = from["isBanned"];



            if (!string.IsNullOrWhiteSpace(isActivated))
                userQuery.IsActivated = isActivated.AsBool();
            if (!string.IsNullOrWhiteSpace(isModerated))
                userQuery.IsModerated = isModerated.AsBool();
            if (!string.IsNullOrWhiteSpace(isBanned))
                userQuery.IsBanned = isBanned.AsBool();

            int result = 0;
            if (int.TryParse(from["userRankLowerLimit"], out result))
            {
                userQuery.UserRankLowerLimit = from["userRankLowerLimit"].AsInt();
                ViewData["UserRankLowerLimit"] = userQuery.UserRankLowerLimit;
            }

            if (int.TryParse(from["userRankUpperLimit"], out result))
            {
                userQuery.UserRankUpperLimit = from["userRankUpperLimit"].AsInt();
                ViewData["UserRankUpperLimit"] = userQuery.UserRankUpperLimit;
            }

            DateTime dateResult = DateTime.MinValue;
            if (DateTime.TryParse(from["registerTimeLowerLimit"], out dateResult))
            {
                userQuery.RegisterTimeLowerLimit = from["registerTimeLowerLimit"].AsDateTime();
                ViewData["RegisterTimeLowerLimit"] = userQuery.RegisterTimeLowerLimit;
            }

            if (DateTime.TryParse(from["registerTimeUpperLimit"], out dateResult))
            {
                userQuery.RegisterTimeUpperLimit = from["registerTimeUpperLimit"].AsDateTime();
                ViewData["RegisterTimeUpperLimit"] = userQuery.RegisterTimeUpperLimit;
            }

            if (!string.IsNullOrWhiteSpace(userQuery.Keyword))
                ViewData["Keyword"] = userQuery.Keyword;
            if (!string.IsNullOrWhiteSpace(userQuery.AccountEmailFilter))
                ViewData["AccountEmailFilter"] = userQuery.AccountEmailFilter;


            IEnumerable<Roles> roles = rolesService.GetRoles();
            if (roles != null)
                ViewData["Rolename"] = new SelectList(roles, "Rolename", "FriendlyRolename", userQuery.RoleName);

            Dictionary<bool, string> activatedValues = new Dictionary<bool, string> { { true, "已激活" }, { false, "未激活" } };
            ViewData["IsActivated"] = new SelectList(activatedValues.Select(w => new { text = w.Value, value = w.Key.ToString().ToLower() }), "value", "text", userQuery.IsActivated);


            Dictionary<bool, string> moderatedValues = new Dictionary<bool, string> { { true, "已管制" }, { false, "未管制" } };
            ViewData["IsModerated"] = new SelectList(moderatedValues.Select(w => new { text = w.Value, value = w.Key.ToString().ToLower() }), "value", "text", userQuery.IsModerated);

            Dictionary<bool, string> bannedValues = new Dictionary<bool, string> { { true, "已封禁" }, { false, "未封禁" } };
            ViewData["IsBanned"] = new SelectList(bannedValues.Select(w => new { text = w.Value, value = w.Key.ToString().ToLower() }), "value", "text", userQuery.IsBanned);

            pageIndex = pageIndex ?? 1;
            PagingDataSet<User> userPaging = userService.GetUsers(userQuery, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (userPaging == null || userPaging.Count() == 0))
                userPaging = userService.GetUsers(userQuery, pageSize, pageIndex.Value - 1);
            return View(userPaging);
        }




        /// <summary>
        /// 批量激活操作
        /// </summary>
        /// <param name="isActivated">激活或取消激活</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ActivatedUsers(bool isActivated)
        {
            //判断权限是否有权操作 

            string returnUrl = Request.GetString("returnUrl", string.Empty);
            IEnumerable<long> uids = Request.Gets<long>("customCheck");
            if (uids != null && uids.Count() > 0)
            {
                membershipService.ActivatedUsers(uids, isActivated);
            }
            return Redirect(returnUrl);
        }

        /// <summary>
        /// 批量取消封禁
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult UnBannedUsers(string returnUrl)
        {
            //判断权限是否有权操作 

            IEnumerable<long> uids = Request.Gets<long>("customCheck");
            var isPower = false;

            if (uids != null && uids.Count() > 0)
            {
                foreach (long uid in uids)
                {
                    if (!authorizerHelper.IsUserPower(uid))
                        return Json(new StatusMessageData(StatusMessageType.Error, "选中的用户中有人的权限比您高！"));
                    else
                        isPower = true;
                }
                if (isPower)
                    membershipService.UnBannedUsers(uids);

            }

            return Redirect(returnUrl);
        }

        /// <summary>
        /// 封禁用户控件
        /// </summary>
        [HttpGet]
        public IActionResult _BannedUsers()
        {
            BannedUserModel model = new BannedUserModel();
            return View(model);
        }

        /// <summary>
        /// 批量封禁操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult BannedUsers(BannedUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "数据未通过验证！"));
            }
            var isPower = false;
            IEnumerable<long> uids = Request.Gets<long>("UserIds");
            if (uids != null && uids.Count() > 0)
            {
                foreach (long uid in uids)
                {
                    if (!authorizerHelper.IsUserPower(uid))
                        return Json(new StatusMessageData(StatusMessageType.Error, "选中的用户中有人的权限比您高！"));
                    else
                        isPower = true;
                }
                if (isPower)
                    membershipService.BannedUsers(uids, model.BanDeadline, model.BanReason);
                return Json(new StatusMessageData(StatusMessageType.Success, "操作成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "未知错误！"));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult DeleteUser(long userId, string returnUrl)
        {
            if (authorizerHelper.IsUserPower(userId))
                userService.DeleteUser(userId);
            return Redirect(returnUrl);
        }

        /// <summary>
        /// 添加用户控件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _CreateUser()
        {
            CreateUserModel model = new CreateUserModel();
            return View(model);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateUser(CreateUserModel model)
        {
            User user = model.AsUser();
            user.IsActivated = true;
            user.IpCreated = Utility.GetIP();

            UserCreateStatus status = UserCreateStatus.UnknownFailure;
            User createdUser = membershipService.CreateUser(user, model.Password, out status);
            var userProfiles = UserProfiles.New();
            userProfiles.Userid = createdUser.UserId;
            userProfilesService.Create(userProfiles);

            StatusMessageData statusMessageData = null;
            switch (status)
            {
                case UserCreateStatus.DisallowedUsername:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，您输入的帐号禁止使用，请输入其他名称");
                    break;
                case UserCreateStatus.DuplicateEmailAddress:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，您输入的电子邮箱地址已经被使用，请输入其他邮箱");
                    break;
                case UserCreateStatus.DuplicateUsername:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，您输入的帐号已经被使用，请输入其他名称");
                    break;
                case UserCreateStatus.InvalidPassword:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "您的密码长度少于本站要求的最小密码长度，请重新输入");
                    break;
                case UserCreateStatus.UnknownFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，注册新用户的时候产生了一个未知错误");
                    break;
                default:
                    break;
            }
            ViewData["statusMessageData"] = statusMessageData;
            if (statusMessageData == null)
                return Json(new StatusMessageData(StatusMessageType.Success, "创建成功！"));
            else
                return View(model);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ResetPassword(long userId)
        {
            //判断权限是否有权操作 
            if (!authorizerHelper.IsUserPower(userId))
                return Json(new StatusMessageData(StatusMessageType.Error, "您目前没有权限对此用户操作！"));

            //从用户配置获取重置密码默认值
            UserSettings settings = new UserSettings();
            User user = userService.GetUser(userId);
            if (membershipService.ResetPassword(user, settings.ResetPassword))
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "重设密码成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "重设密码失败！"));
        }

        /// <summary>
        /// 设置角色控件
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="returnUrl">回跳Url</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _SetRoles(long userId, string returnUrl)
        {
            IEnumerable<Roles> allRoles = rolesService.GetRoles();
            IEnumerable<Roles> models = null;

            if (allRoles != null)
            {
                if (!authorizerHelper.IsSuperAdministrator(UserContext.CurrentUser))
                    models = allRoles.Where(w => w.Rolename != "SuperAdministrator");
                else
                    models = allRoles.Where(w => w.Rolename != "SuperAdministrator" && w.Rolename != "ContentAdministrator");
            }

            ViewData["user"] = userService.GetFullUser(userId);
            ViewData["returnUrl"] = returnUrl;
            return View(models);
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetRoles()
        {

            IEnumerable<string> strRoles = Request.Gets<string>("strRoles");
            long userId = Request.Get<long>("userId");
            string returnUrl = Request.Get<string>("returnUrl");

            if (!authorizerHelper.IsUserPower(userId))
                return Json(new StatusMessageData(StatusMessageType.Error, "您目前没有权限对此用户操作"));

            if (strRoles != null)
            {
                if (strRoles.Where(w => w.ToString() == "SuperAdministrator").Count() > 0 ? false : true)
                {
                    rolesService.RemoveRolesInUser(userId);
                    rolesService.AddUserToRoles(userId, strRoles.ToList());
                    return Json(new StatusMessageData(StatusMessageType.Success, "设置角色成功"));
                }
                else
                {
                    return Json(new StatusMessageData(StatusMessageType.Error, "角色中包含了超管"));
                }
            }
            else
            {
                rolesService.RemoveRolesInUser(userId);
                return Json(new StatusMessageData(StatusMessageType.Success, "设置角色成功"));
            }
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _EditUser(long userId)
        {
            User user = userService.GetFullUser(userId);
            EditUserModel model = new EditUserModel() { UserName = user.UserName, AccountEmail = user.AccountEmail, AccountMobile = user.AccountMobile, DateCreated = user.DateCreated, NickName = user.NickName, IsEmailVerified = user.IsEmailVerified, IsMobileVerified = user.IsMobileVerified, PasswordAnswer = user.PasswordAnswer, PasswordQuestion = user.PasswordQuestion, BanDeadLine = user.BanDeadLine, UserId = user.UserId, FollowedCount = user.FollowedCount, FollowerCount = user.FollowerCount, ForceLogin = user.ForceLogin, FrozenSexyPoints = user.FrozenSexyPoints, IpCreated = user.IpCreated, IpLastActivity = user.IpLastActivity, IsAuthentication = user.IsAuthentication, IsForceModerated = user.IsForceModerated, LastAction = user.LastAction, LastActivityTime = user.LastActivityTime, Rank = user.Rank, SexyPoints = user.SexyPoints, UserType = user.UserType };
            return View(model);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditUser(EditUserModel model)
        {
            long uid = model.UserId;

            //验证权限
            if (!authorizerHelper.IsUserPower(uid))
                return Json(new StatusMessageData(StatusMessageType.Error, "选中的用户中有人的权限比您高！"));


            User user = userService.GetUser(model.UserId);
            user.PasswordQuestion = model.PasswordQuestion;
            user.PasswordAnswer = model.PasswordAnswer;
            user.IsEmailVerified = model.IsEmailVerified;
            user.IsMobileVerified = model.IsMobileVerified;
            user.ForceLogin = model.ForceLogin;
            user.IsForceModerated = model.IsForceModerated;
            user.IsAuthentication = model.IsAuthentication;
            user.UserType = model.UserType;
            user.FollowerCount = model.FollowerCount;//粉丝数
            user.FollowedCount = model.FollowedCount;//关注用户数
            user.Rank = model.Rank;
            user.SexyPoints = model.SexyPoints;
            user.FrozenSexyPoints = model.FrozenSexyPoints;

            bool result = userService.EditUser(user);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "操作成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "操作失败！"));
        }

        /// <summary>
        /// 用户资料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _DetailsUserProfiles(long userId)
        {
            UserProfiles userProfiles = userProfilesService.GetUserProfiles(userId);
            if (userProfiles == null)
            {
                //返回一个ajax用的错误页  
                return View("/Views/Error/SystemErrorAjax.cshtml");
            }

            //性别
            Dictionary<int, string> sexValues = new Dictionary<int, string> { { -1, "未填" }, { 0, "男" }, { 1, "女" } };
            ViewData["Sex"] = new SelectList(sexValues.Select(m => new { text = m.Value, value = m.Key.ToString() }), "value", "text", userProfiles.Sex);
            //性取向
            Dictionary<int, string> sexualOrientationValues = new Dictionary<int, string> { { -1, "未填" }, { 1, "男" }, { 2, "女" }, { 3, "双性" } };
            ViewData["SexualOrientation"] = new SelectList(sexualOrientationValues.Select(m => new { text = m.Value, value = m.Key.ToString() }), "value", "text", userProfiles.SexualOrientation);
            //婚姻状况
            Dictionary<int, string> marriageValues = new Dictionary<int, string> { { -1, "未填" }, { 0, "未婚" }, { 1, "已婚" } };
            ViewData["Marriage"] = new SelectList(marriageValues.Select(m => new { text = m.Value, value = m.Key.ToString() }), "value", "text", userProfiles.SexualOrientation);
            //婚姻状况是否保密 所在地是否保密 性取向是否保密
            Dictionary<int, string> isMarriageSecrecyValues = new Dictionary<int, string> { { -1, "未填" }, { 0, "否" }, { 1, "是" } };
            ViewData["IsOK"] = new SelectList(isMarriageSecrecyValues.Select(m => new { text = m.Value, value = m.Key.ToString() }), "value", "text", userProfiles.SexualOrientation);

            DetailsUserProfilesModel detailsUserProfilesModel = new DetailsUserProfilesModel()
            {
                Age = userProfiles.Age,
                Birthday = userProfiles.Birthday,
                City = userProfiles.City,
                Integrity = userProfiles.Integrity,
                IsMarriageSecrecy = userProfiles.IsMarriageSecrecy,
                IsNowareaSecrecy = userProfiles.IsNowareaSecrecy,
                IsSexualorientationSecrecy = userProfiles.IsSexualorientationSecrecy,
                Marriage = userProfiles.Marriage,
                Provinces = userProfiles.Provinces,
                Sex = userProfiles.Sex,
                SexualOrientation = userProfiles.SexualOrientation,
                Userid = userProfiles.Userid
            };
            return View(detailsUserProfilesModel);
        }

        /// <summary>
        /// 编辑用户资料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditUserProfiles(DetailsUserProfilesModel model)
        {
            long uid = model.Userid;

            //验证权限
            if (!authorizerHelper.IsUserPower(uid))
                return Json(new StatusMessageData(StatusMessageType.Error, "选中的用户中有人的权限比您高！"));

            UserProfiles userProfiles = userProfilesService.GetUserProfiles(uid);
            userProfiles.Sex = model.Sex;
            userProfiles.Age = model.Age;
            userProfiles.Birthday = model.Birthday;
            userProfiles.SexualOrientation = model.SexualOrientation;
            userProfiles.IsSexualorientationSecrecy = model.IsSexualorientationSecrecy;
            userProfiles.Marriage = model.Marriage;
            userProfiles.IsMarriageSecrecy = model.IsMarriageSecrecy;
            userProfiles.Provinces = model.Provinces;
            userProfiles.City = model.City;
            userProfiles.IsNowareaSecrecy = model.IsNowareaSecrecy;
            bool result = userProfilesService.EditUserProfiles(userProfiles);

            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "操作成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "操作失败！"));

        }

        /// <summary>
        /// 设置用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _SetHeadImage(long userId)
        {
            User user = userService.GetFullUser(userId);
            return View(user);
        }

        /// <summary>
        /// 保存裁剪头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveHeadImage([FromServices]IHostingEnvironment env, IFormFile files, long userId)
        {

            var file = Request.Form.Files[0];
            long size = Request.Form.Files.Sum(f => f.Length);


            var x1 = Request.GetInt("x1", 0);
            var y1 = Request.GetInt("y1", 0);
            var w = Request.GetInt("w", 0);
            var h = Request.GetInt("h", 0);

            var filePath = string.Format("/Uploads/Images/{0}/{1}/{2}/", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));
            var qiniuFilePath = string.Format("{0}/{1}/{2}/", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));
            if (!Directory.Exists(env.WebRootPath + filePath))
                Directory.CreateDirectory(env.WebRootPath + filePath);

            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
                const string saveSuffix = ".jpeg";
                if (fileExtension == null)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件没有后缀"));
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不是图片"));
                if (size > 1024 * 1024 * 2)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不能大于2M"));

                var strDateTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                var strRan = Convert.ToString(new Random().Next(100, 999));
                var saveName = strDateTime + strRan;
                var useQiNiu = BuilderContext.Configuration["UseQiNiu"];
                var qiniuHost = BuilderContext.Configuration["QiNiuHost"];

                string hostPath = WebSiteUtility.MapPhysicsToUrl($"{filePath}{saveName}");
                string qiniuHostPath = WebSiteUtility.MapPhysicsToUrl($"{qiniuHost}{qiniuFilePath}{saveName}");

                await Task.Factory.StartNew(() =>
                {
                    byte[] cutBytes = ConvertUtility.StreamToBytes(file.OpenReadStream());
                    Bitmap bitmap = ConvertUtility.BytesToBitmap(cutBytes);
                    var scale = bitmap.Height / 360.00;
                    Bitmap newBitMap = Utility.CutImage(bitmap, Convert.ToInt32(x1 * scale), Convert.ToInt32(y1 * scale), Convert.ToInt32(w * scale), Convert.ToInt32(h * scale));
                    Bitmap newBitMap100 = Utility.ResizeImage(newBitMap, 100, 100);
                    Bitmap newBitMap50 = Utility.ResizeImage(newBitMap, 50, 50);
                    Bitmap newBitMap30 = Utility.ResizeImage(newBitMap, 30, 30);

                    newBitMap.Save($"{env.WebRootPath}{filePath}{saveName}.jpeg", ImageFormat.Jpeg);
                    newBitMap100.Save($"{env.WebRootPath}{filePath}{saveName}_100x100{saveSuffix}", ImageFormat.Jpeg);
                    newBitMap50.Save($"{env.WebRootPath}{filePath}{saveName}_50x50{saveSuffix}", ImageFormat.Jpeg);
                    newBitMap30.Save($"{env.WebRootPath}{filePath}{saveName}_30x30{saveSuffix}", ImageFormat.Jpeg);
                });
                if (useQiNiu.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                {
                    await Task.Run(() =>
                    {
                        QiniuManager qiniu = new QiniuManager();
                        string savekey = $"{qiniuFilePath}{saveName}";
                        byte[] data = System.IO.File.ReadAllBytes($"{env.WebRootPath}{filePath}{saveName}{saveSuffix}");
                        qiniu.ByteUpload(data, savekey);
                    });
                    await Task.Run(() =>
                    {
                        QiniuManager qiniu = new QiniuManager();
                        string savekey = $"{qiniuFilePath}{saveName}_100x100";
                        byte[] data = System.IO.File.ReadAllBytes($"{env.WebRootPath}{filePath}{saveName}_100x100{saveSuffix}");
                        qiniu.ByteUpload(data, savekey);
                    });
                    await Task.Run(() =>
                    {
                        QiniuManager qiniu = new QiniuManager();
                        string savekey = $"{qiniuFilePath}{saveName}_50x50";
                        byte[] data = System.IO.File.ReadAllBytes($"{env.WebRootPath}{filePath}{saveName}_50x50{saveSuffix}");
                        qiniu.ByteUpload(data, savekey);
                    });
                    await Task.Run(() =>
                    {
                        QiniuManager qiniu = new QiniuManager();
                        string savekey = $"{qiniuFilePath}{saveName}_30x30";
                        byte[] data = System.IO.File.ReadAllBytes($"{env.WebRootPath}{filePath}{saveName}_30x30{saveSuffix}");
                        qiniu.ByteUpload(data, savekey);
                    });
                }


                User user = userService.GetFullUser(userId);
                if (useQiNiu.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                    user = membershipService.SetHeadImage(user, $"{qiniuHost}{qiniuFilePath}{saveName}");
                else
                    user = membershipService.SetHeadImage(user, $"{Request.Scheme}://{Request.Host}{filePath}{saveName}{saveSuffix}");

                if (user != null)
                {
                    HttpContext.Session.Set<User>(user.UserId.ToString(), user);
                }

                return Json(new StatusMessageData(StatusMessageType.Success, "上传成功"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "上传失败"));
        }

        /// <summary>
        /// 查看用户粉丝
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _UserFollowed(long userId, int? pageIndex, int pageSize = 5)
        {
            if (userId == 0) return Json(new StatusMessageData(StatusMessageType.Error, "没有此用户！"));

            pageIndex = pageIndex ?? 1;

            IEnumerable<long> followedIds = followsService.GetFollowedIds(userId);

            if (followedIds.Count() <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "此用户没有粉丝！"));

            PagingDataSet<User> userPaging = userService.GetUsers(followedIds, pageIndex.Value, pageSize);

            if (pageIndex > 1 && (userPaging == null || userPaging.Count() == 0))
                userPaging = userService.GetUsers(followedIds, pageSize, pageIndex.Value - 1);
            return View(userPaging);
        }

        /// <summary>
        /// 查看关注用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _UserFocus(long userId, int? pageIndex, int pageSize = 5)
        {
            if (userId == 0) return Json(new StatusMessageData(StatusMessageType.Error, "没有此用户！"));

            pageIndex = pageIndex ?? 1;

            IEnumerable<long> focusIds = followsService.GetFocusIds(userId);

            if (focusIds.Count() <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "此用户没有关注用户！"));

            PagingDataSet<User> userPaging = userService.GetUsers(focusIds, pageIndex.Value, pageSize);

            if (pageIndex > 1 && (userPaging == null || userPaging.Count() == 0))
                userPaging = userService.GetUsers(focusIds, pageSize, pageIndex.Value - 1);
            return View(userPaging);
        }


        #endregion

        #region 角色管理
        /// <summary>
        /// 管理角色
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [OperationLogs(true, Name = "角色管理", Type = PermissionItemsType.LeftMenu)]
        public IActionResult ManageRoles(int? pageIndex, int pageSize = 20)
        {
            NameValueCollection from = RequestHelper.GetQueryParams(Request);
            RolesQuery rolesQuery = new RolesQuery();
            rolesQuery.Keyword = from["keyWord"];

            string isEnabled = from["isEnabled"],
                isPublic = from["isPublic"],
                isBuiltin = from["isBuiltin"];

            if (!string.IsNullOrWhiteSpace(rolesQuery.Keyword))
                ViewData["Keyword"] = rolesQuery.Keyword;
            if (!string.IsNullOrWhiteSpace(isEnabled))
                rolesQuery.IsEnabled = isEnabled.AsBool();
            if (!string.IsNullOrWhiteSpace(isBuiltin))
                rolesQuery.IsBuiltin = isBuiltin.AsBool();
            if (!string.IsNullOrWhiteSpace(isPublic))
                rolesQuery.IsPublic = isPublic.AsBool();

            Dictionary<bool, string> enabledValues = new Dictionary<bool, string> { { true, "已启用" }, { false, "未启用" } };
            ViewData["IsEnabled"] = new SelectList(enabledValues.Select(m => new { text = m.Value, value = m.Key.ToString().ToLower() }), "value", "text", rolesQuery.IsEnabled);

            Dictionary<bool, string> publicValues = new Dictionary<bool, string> { { true, "已公开" }, { false, "未公开" } };
            ViewData["IsPublic"] = new SelectList(publicValues.Select(m => new { text = m.Value, value = m.Key.ToString().ToLower() }), "value", "text", rolesQuery.IsPublic);

            Dictionary<bool, string> builtinValues = new Dictionary<bool, string> { { true, "是" }, { false, "否" } };
            ViewData["IsBuiltin"] = new SelectList(builtinValues.Select(m => new { text = m.Value, value = m.Key.ToString().ToLower() }), "value", "text", rolesQuery.IsBuiltin);

            pageIndex = pageIndex ?? 1;
            rolesQuery.RoleSortBy = RoleSortBy.Rolename;

            PagingDataSet<Roles> rolesPaging = rolesService.GetRoles(rolesQuery, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (rolesPaging == null || rolesPaging.Count() == 0))
                rolesPaging = rolesService.GetRoles(rolesQuery, pageSize, pageIndex.Value - 1);

            return View(rolesPaging);
        }


        /// <summary>
        /// 批量启用操作
        /// </summary>
        /// <param name="isEnabled">是否启用</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EnabledRole(bool isEnabled)
        {
            string returnUrl = Request.GetString("returnUrl", string.Empty);
            IEnumerable<string> uids = Request.Gets<string>("customCheck");
            if (uids != null && uids.Count() > 0)
            {
                rolesService.EnabledRole(uids, isEnabled);
            }
            return Redirect(returnUrl);
        }
        /// <summary>
        /// 批量公开用户
        /// </summary>
        /// <param name="isPublic">是否公开</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PublicRole(bool isPublic)
        {
            //判断权限是否有权操作 
            string returnUrl = Request.GetString("returnUrl", string.Empty);
            IEnumerable<string> uids = Request.Gets<string>("customCheck");
            if (uids != null && uids.Count() > 0)
            {
                rolesService.IsPublic(uids, isPublic);
            }
            return Redirect(returnUrl);
        }
        /// <summary>
        /// 编辑角色控件
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _EditRoles(string roleName)
        {
            Roles role = rolesService.GetFullRoles(roleName);
            EditRolesModel model = new EditRolesModel()
            {
                ConnectToUser = role.ConnectToUser,
                Description = role.Description,
                FriendlyRolename = role.FriendlyRolename,
                IsBuiltin = role.IsBuiltin,
                IsEnabled = role.IsEnabled,
                IsPublic = role.IsPublic,
                Rolename = role.Rolename,
                Applicationid = role.Applicationid,
                RoleImage = role.RoleImage
            };
            return View(model);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditRoles(EditRolesModel model)
        {
            string roleNmae = model.Rolename;

            //验证权限
            if (!authorizerHelper.IsUserPower(UserContext.CurrentUser.UserId))
                return Json(new StatusMessageData(StatusMessageType.Error, "选中的用户中有人的权限比您高！"));


            Roles role = rolesService.GetFullRoles(model.Rolename);

            role.ConnectToUser = model.ConnectToUser;
            role.Description = model.Description;
            role.FriendlyRolename = model.FriendlyRolename;
            role.IsBuiltin = model.IsBuiltin;
            role.IsEnabled = model.IsEnabled;
            role.IsPublic = model.IsPublic;
            role.Applicationid = model.Applicationid;
            role.RoleImage = model.RoleImage;

            bool result = rolesService.EditRoles(role);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "操作成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "操作失败！"));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult DeleteRoles(string roleName)
        {
            Roles role = rolesService.GetFullRoles(roleName);
            if (!authorizerHelper.IsUserPower(UserContext.CurrentUser.UserId))
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "选中的用户中有人的权限比您高！"));
            }
            else if (role.IsBuiltin)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "系统内置用户不可删除！"));
            }
            else
            {
                IEnumerable<string> rolesNames = rolesService.GetRolesNamesInUser(UserContext.CurrentUser.UserId);
                foreach (String item in rolesNames)
                {
                    if (item.Equals("SuperAdministrator") || item.Equals("ContentAdministrator"))
                    {

                    }
                    else
                    {
                        return Json(new StatusMessageData(StatusMessageType.Error, "您没有足够的权限！"));
                    }
                }
            }

            bool result = rolesService.DeleteRoles(roleName);
            bool result2 = permissionItemsService.DeleteRolesPermissionItems(roleName);
            if (result && result2)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "操作成功！"));
            }

            return Json(new StatusMessageData(StatusMessageType.Error, "操作失败！"));
        }

        /// <summary>
        /// 新增角色控件
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _AddRoles()
        {
            AddRolesModel model = new AddRolesModel();
            return View(model);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRoles(AddRolesModel model)
        {
            string roleNmae = model.Rolename;

            //验证权限
            if (!authorizerHelper.IsUserPower(UserContext.CurrentUser.UserId))
                return Json(new StatusMessageData(StatusMessageType.Error, "选中的用户中有人的权限比您高！"));

            Roles role = Roles.New();
            role.ConnectToUser = model.ConnectToUser;
            role.RoleImage = model.RoleImage;
            role.Description = model.Description;
            role.Rolename = model.Rolename;
            role.FriendlyRolename = model.FriendlyRolename;
            role.IsBuiltin = model.IsBuiltin;
            role.IsEnabled = model.IsEnabled;
            role.IsPublic = model.IsPublic;
            role.Applicationid = model.Applicationid;

            bool result = rolesService.AddRoles(role);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "操作成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "操作失败！"));
        }

        /// <summary>
        /// 设置角色权限页面
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetRolesPermission(string roleName)
        {
            List<SetRolesPermissionModel> models = new List<SetRolesPermissionModel>();
            IEnumerable<PermissionItems> permissionItemsList = permissionItemsService.GetPermissionItemsAll();
            IEnumerable<PermissionItemsInRoles> permissionItemsInRolesList = permissionItemsService.GetPermissionItemsInRolesByRolesname(roleName);
            permissionItemsList.Each(w => AddHaveItem(w, permissionItemsInRolesList, models));
            IEnumerable<SetRolesPermissionModel> list = models.Where(w => w.IsHave);
            ViewData["Rolename"] = roleName;
            return View(models);
        }

        /// <summary>
        /// 添加拥有项
        /// </summary>
        /// <param name="permissionItems"></param>
        /// <param name="permissionItemsList"></param>
        /// <param name="model"></param>
        [NonAction]
        private void AddHaveItem(PermissionItems permissionItems, IEnumerable<PermissionItemsInRoles> permissionItemsList, List<SetRolesPermissionModel> list)
        {
            SetRolesPermissionModel model = new SetRolesPermissionModel();
            var item = permissionItemsList.FirstOrDefault(w => w.Itemkey == permissionItems.Itemkey);
            model.ToSetRolesPermissionModel(permissionItems);
            if (item != null)
                model.IsHave = true;
            else
                model.IsHave = false;
            list.Add(model);
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetRolesPermission(string roleName, string[] permission)
        {
            permissionItemsService.DeleteRolesPermissionItems(roleName);
            var result = permissionItemsService.AddRolesToPermissionItems(roleName, permission);
            StatusMessageData statusMessageData = new StatusMessageData(StatusMessageType.Error, "设置时，产生了一个未知错误");
            if (result)
                return Json(new StatusMessageData(StatusMessageType.Success, "设置成功！"));
            else
                return Json(statusMessageData);
        }
        #endregion

        #region 地址管理
        /// <summary>
        /// 用户地址管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ManageAddress(int? pageIndex, int pageSize = 20)
        {
            NameValueCollection from = RequestHelper.GetQueryParams(Request);
            UserAddressQuery userAddressQuery = new UserAddressQuery();
            userAddressQuery.Name = from["name"];
            userAddressQuery.Phone = from["phone"];
            userAddressQuery.Address = from["address"];
            userAddressQuery.UserName = from["userName"];

            string isDefault = from["isDefault"],
                   isDeleted = from["isDeleted"],
                   provinceid = from["provinceid"],
                   cityid = from["cityid"],
                   areaid = from["areaid"],
                   userId = from["userId"];

            if (!string.IsNullOrWhiteSpace(isDefault))
                userAddressQuery.IsDefault = isDefault.AsBool();
            if (!string.IsNullOrWhiteSpace(isDeleted))
                userAddressQuery.IsDeleted = isDeleted.AsBool();

            if (!string.IsNullOrWhiteSpace(userId))
                userAddressQuery.UserId = long.Parse(userId);
            if (!string.IsNullOrWhiteSpace(provinceid))
                ViewData["provinceid"] = userAddressQuery.Provinceid = provinceid.AsInt();
            if (!string.IsNullOrWhiteSpace(cityid))
                ViewData["cityid"] = userAddressQuery.Cityid = cityid.AsInt();
            if (!string.IsNullOrWhiteSpace(areaid))
                ViewData["areaid"] = userAddressQuery.Areaid = areaid.AsInt();

            if (!string.IsNullOrWhiteSpace(userAddressQuery.UserName))
                ViewData["userName"] = userAddressQuery.UserName;
            if (!string.IsNullOrWhiteSpace(userAddressQuery.Name))
                ViewData["name"] = userAddressQuery.Name;
            if (!string.IsNullOrWhiteSpace(userAddressQuery.Phone))
                ViewData["phone"] = userAddressQuery.Phone;
            if (!string.IsNullOrWhiteSpace(userAddressQuery.Address))
                ViewData["address"] = userAddressQuery.Address;

            Dictionary<bool, string> builtinValues = new Dictionary<bool, string> { { true, "是" }, { false, "否" } };
            ViewData["IsDeleted"] = ViewData["IsDefault"] = new SelectList(builtinValues.Select(m => new { text = m.Value, value = m.Key.ToString().ToLower() }), "value", "text", userAddressQuery.IsDefault);

            pageIndex = pageIndex ?? 1;
            userAddressQuery.UserAddressSortBy = UserAddressSortBy.Name;

            PagingDataSet<UserAddress> userAddressPaging = userAddressService.GetUserAddress(userAddressQuery, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (userAddressPaging == null || userAddressPaging.Count() == 0))
                userAddressPaging = userAddressService.GetUserAddress(userAddressQuery, pageSize, pageIndex.Value - 1);

            return View(userAddressPaging);
        }

        /// <summary>
        /// 批量启用用户地址
        /// </summary>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EnableUserAddress(bool isEnabled)
        {
            string returnUrl = Request.GetString("returnUrl", string.Empty);
            IEnumerable<string> aids = Request.Gets<string>("customCheck");
            if (aids != null && aids.Count() > 0)
            {
                userAddressService.EnableUserAddress(aids, isEnabled);
            }
            return Redirect(returnUrl);
        }


        /// <summary>
        /// 用户地址详情
        /// </summary>
        /// <param name="userAddressId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _UserAddressDetails(int userAddressId)
        {
            UserAddress userAddress = userAddressService.GetFullUserAddress(userAddressId);
            DetailsUserAddressModel model = new DetailsUserAddressModel()
            {
                Address = userAddress.Address,
                IsDefault = userAddress.IsDefault,
                IsDeleted = userAddress.IsDeleted,
                Name = userAddress.Name,
                Phone = userAddress.Phone,
                ProvinceCityArea = userAddress.ProvinceCityArea

            };
            return View(model);
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Getprovinces()
        {
            IEnumerable<BasicsProvinces> list = userAddressService.GetProvinces();
            return Json(list);
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCitys(string provincesId)
        {
            int pid = provincesId.AsInt();
            IEnumerable<BasicsCitys> list = userAddressService.GetCitys(pid);
            return Json(list);
        }

        /// <summary>
        /// 获取区域
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAreas(string cityId)
        {
            int pid = cityId.AsInt();
            IEnumerable<BasicsAreas> list = userAddressService.GetAreas(pid);
            return Json(list);
        }
        #endregion
    }
}
