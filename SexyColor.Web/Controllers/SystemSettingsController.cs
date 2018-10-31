using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using SexyColor.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Web.Controllers
{
    [Authorize(Policy = "EnterSystem")]
    [Permissions]
    public class SystemSettingsController : Controller
    {
        public IUserService userService { get; set; }
        public IHomeDynamicSettingsService homeDynamicSettingsService { get; set; }

        #region 首页动态设置
        /// <summary>
        /// 首页轮换管理
        /// </summary>
        /// <returns></returns>
        public IActionResult HomeLoopSetting(int? pageIndex, int pageSize = 20)
        {
            ViewBag.Module = "首页模块";
            ViewBag.Site = "首页动态设置";

            //ViewData["Model"] = new HomeDynamicModel().ToHomeDynamicModel(HomeDynamicSettings.New());

            pageIndex = pageIndex ?? 1;
            PagingDataSet<HomeDynamicSettings> paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Loop, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Loop, pageSize, pageIndex.Value - 1);
            return View(paging);
        }

        /// <summary>
        /// 首页分类管理
        /// </summary>
        /// <returns></returns>
        public IActionResult HomeClassifySetting(int? pageIndex, int pageSize = 20)
        {
            ViewBag.Module = "首页模块";
            ViewBag.Site = "首页分类管理";

            pageIndex = pageIndex ?? 1;
            PagingDataSet<HomeDynamicSettings> paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Classify, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Classify, pageSize, pageIndex.Value - 1);
            return View(paging);
        }

        /// <summary>
        /// 首页自定义管理
        /// </summary>
        /// <returns></returns>
        public IActionResult HomeCustomSetting(int? pageIndex, int pageSize = 20)
        {
            ViewBag.Module = "首页模块";
            ViewBag.Site = "首页自定义管理";

            pageIndex = pageIndex ?? 1;
            PagingDataSet<HomeDynamicSettings> paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Custom, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Custom, pageSize, pageIndex.Value - 1);
            return View(paging);
        }


        /// <summary>
        /// 首页秒杀管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult HomeSeckillSetting(int? pageIndex, int pageSize = 20)
        {
            ViewBag.Module = "首页模块";
            ViewBag.Site = "首页秒杀管理";

            pageIndex = pageIndex ?? 1;
            PagingDataSet<HomeDynamicSettings> paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Seckill, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = homeDynamicSettingsService.GetPageHomeDynamic((int)DynamicSettingsType.Seckill, pageSize, pageIndex.Value - 1);
            return View(paging);
        }

        /// <summary>
        /// 调整排序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult HomeDisplayOrder(long id, int type, bool isUp)
        {
            StatusMessageData msg = new StatusMessageData(StatusMessageType.Error, "未知错误！");
            var dynamicSource = homeDynamicSettingsService.GetHomeDynamicSingle(id);
            var dynamicTarget = homeDynamicSettingsService.GetHomeDynamicTop(id, type, isUp);

            var tempOrder = dynamicSource.DisplayOrder;
            dynamicSource.DisplayOrder = dynamicTarget.DisplayOrder;
            dynamicTarget.DisplayOrder = tempOrder;

            string message = string.Empty;
            homeDynamicSettingsService.UpdateHomeDynamicByDisplayOrder(dynamicSource, out message);
            homeDynamicSettingsService.UpdateHomeDynamicByDisplayOrder(dynamicTarget, out message);
            if (message.IsNotNullAndWhiteSpace())
            {
                msg = new StatusMessageData(StatusMessageType.Success, message);
            }
            return Json(msg);
        }

        /// <summary>
        /// 添加更新首页设置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> HomeDynamicAddUpdateAsync([FromServices]IHostingEnvironment env, IFormFile files, HomeDynamicModel model)
        {
            ValidateInput(model, ModelState);
            if (model.Id == 0)
                model.IsUpdate = false;
            else
                model.IsUpdate = true;

            string message = string.Empty;

            
            if (ModelState.IsValid)
            {
                if (model.ImageUrl.IsNullOrWhiteSpace())
                {
                    var file = Request.Form.Files[0];
                    long size = Request.Form.Files.Sum(f => f.Length);
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
                        var pathName = string.Empty;

                        string hostPath = WebSiteUtility.MapPhysicsToUrl($"{filePath}{saveName}");
                        string qiniuHostPath = WebSiteUtility.MapPhysicsToUrl($"{qiniuHost}{qiniuFilePath}{saveName}");

                        using (var stream = new FileStream($"{env.WebRootPath}{filePath}{saveName}{saveSuffix}", FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            stream.Flush();
                            pathName = stream.Name;
                            model.ImageUrl = $"{Request.Scheme}://{Request.Host}{hostPath}{saveSuffix}";
                        }
                        if (useQiNiu.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                        {
                            await Task.Run(() =>
                            {
                                QiniuManager qiniu = new QiniuManager();
                                string savekey = $"{qiniuFilePath}{saveName}";
                                byte[] data = System.IO.File.ReadAllBytes(pathName);
                                qiniu.ByteUpload(data, savekey);
                            });
                            model.ImageUrl = qiniuHostPath;
                        }
                    }
                }

                if (model.IsUpdate)
                {
                    HomeDynamicSettings dynamic = homeDynamicSettingsService.GetHomeDynamicSingle(model.Id);
                    dynamic.DateLastModified = DateTime.Now;
                    dynamic.ImageUrl = model.ImageUrl;
                    dynamic.LabelText = model.LabelText;
                    dynamic.RedirectUrl = model.RedirectUrl;
                    homeDynamicSettingsService.UpdateHomeDynamic(dynamic, out message);
                }
                else
                {
                    HomeDynamicSettings dynamic = model.AsHomeDynamic();
                    dynamic.Userid = UserContext.CurrentUser.UserId;
                    dynamic.DisplayOrder = homeDynamicSettingsService.GetMaxDisplayOrder(model.Type);
                    homeDynamicSettingsService.AddHomeDynamic(dynamic, out message);
                }
                return Json(new StatusMessageData(StatusMessageType.Success, "保存成功"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "保存失败"));
        }

        [NonAction]
        private void ValidateInput(HomeDynamicModel model, ModelStateDictionary modelState)
        {
            if (model.LabelText.IsNullOrWhiteSpace())
                modelState.AddModelError("LabelText", "文本文字为必填选项");
            else
                if (model.LabelText.Length > 64)
                    modelState.AddModelError("LabelText", "文本文字最大长度为64个字符");
                
            if (model.RedirectUrl.IsNullOrWhiteSpace())
                modelState.AddModelError("RedirectUrl", "重定向Url为必填选项");
            else
                if (model.RedirectUrl.Length > 255)
                    modelState.AddModelError("RedirectUrl", "重定向Url最大长度为255个字符");


        }

 
        #endregion
    }
}
