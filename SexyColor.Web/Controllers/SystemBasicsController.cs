using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace SexyColor.Web.Controllers
{
    [Authorize(Policy = "EnterSystem")]
    [Permissions]
    public class SystemBasicsController : Controller
    {
        //用户服务
        public IUserService userService { get; set; }
        //操作日志服务
        public OperationLogsService operationLogsService { get; set; }
        //站点公告服务
        public IAnnouncementsService announcementsService { get; set; }
        //全局设置服务
        public IGlobalSettingsService globalSettingsService { get; set; }
        //用户等级服务
        public IUserRanksService userRanksService { get; set; }

        #region 日志相关

        /// <summary>
        /// 管理操作日志
        /// </summary>
        /// <returns></returns>
        public IActionResult ManageOperation(int? pageIndex, int pageSize = 20)
        {
            var from = RequestHelper.GetQueryParams(Request);
            ViewData["currentUser"] = UserContext.CurrentUser;

            OperationLogsQuery query = new OperationLogsQuery();
            query.OperationName = from["name"];
            query.OperationType = from["type"];
            query.Username = from["username"];

            if (!string.IsNullOrWhiteSpace(query.OperationName))
                ViewData["OperationName"] = query.OperationName;
            if (!string.IsNullOrWhiteSpace(query.Username))
                ViewData["Username"] = query.Username;

            DateTime result = new DateTime();
            if (DateTime.TryParse(from["timeLowerLimit"], out result))
            {
                query.TimeLowerLimit = from["timeLowerLimit"].AsDateTime();
                ViewData["TimeLowerLimit"] = query.TimeLowerLimit;
            }
            if (DateTime.TryParse(from["timeUpperLimit"], out result))
            {
                query.TimeUpperLimit = from["timeUpperLimit"].AsDateTime();
                ViewData["TimeUpperLimit"] = query.TimeUpperLimit;
            }

            Dictionary<string, string> values = new Dictionary<string, string> { { "LeftMenu", "左侧菜单" }, { "OrdinaryButton", "普通按钮" } };
            ViewData["OperationType"] = new SelectList(values.Select(w => new { text = w.Value, value = w.Key.ToString() }), "value", "text", query.OperationType);

            pageIndex = pageIndex ?? 1;
            PagingDataSet<OperationLogs> paging = operationLogsService.GetPageOperationLogs(query, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = operationLogsService.GetPageOperationLogs(query, pageSize, pageIndex.Value - 1);
            return View(paging);
        }

        /// <summary>
        /// 查看详细日志
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public IActionResult _DetailsOperation(int operationId)
        {
            OperationLogs logs = operationLogsService.GetOperationLogs(operationId);
            if (logs != null && logs.Id > 0)
                return View(logs);
            return View("/Views/Error/GlobalError.cshtml");
        }


        #endregion

        #region 站点公告
        /// <summary>
        /// 站点公告
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ManageAnnouncements()
        {
            int id = announcementsService.GetGetAnnouncementsId();

            Announcements announcements = announcementsService.GetAnnouncements(id);

            EditAnnouncementsModel editAnnouncementsModel = new EditAnnouncementsModel()
            {
                Id = announcements.Id,
                Body = announcements.Body,
                DateCreate = announcements.DateCreate,
                DateExpired = announcements.DateExpired,
                DateLastupdate = announcements.DateLastupdate,
                DateRelease = announcements.DateRelease,
                DisplayOrder = announcements.DisplayOrder,
                IsEnabled = announcements.IsEnabled,
                Subject = announcements.Subject,
                Userid = announcements.Userid
            };

            return View(editAnnouncementsModel);

        }

        /// <summary>
        /// 编辑站点公告
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditAnnouncements(EditAnnouncementsModel model)
        {
            Announcements announcements = announcementsService.GetAnnouncements(model.Id);
            if (announcements == null) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该公告！"));

            if (announcements.DateLastupdate == null)
            {
                announcements.DateCreate = DateTime.Now;
            }
            announcements.Subject = model.Subject;
            announcements.Body = model.Body;
            announcements.DateRelease = model.DateRelease;
            announcements.DateLastupdate = DateTime.Now;
            announcements.DateExpired = model.DateExpired;
            announcements.IsEnabled = model.IsEnabled;
            announcements.DisplayOrder = model.DisplayOrder;
            announcements.Userid = UserContext.CurrentUser.UserId;

            bool result = announcementsService.EditAnnouncements(announcements);

            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "保存成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "保存失败！"));

        }
        #endregion

        #region 全局设置
        /// <summary>
        /// 全局设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ManageGlobalSettings()
        {
            GlobalSettings singleSettings = GlobalSettingsSingle.Instance().GetGlobalSettings();
            EditGlobalSettingsModel editGlobalSettingsModel = new EditGlobalSettingsModel()
            {
                GlobalFreeMoneySetting = singleSettings.GlobalFreeMoneySetting,
                GlobalFreightSetting = singleSettings.GlobalFreightSetting,
                GlobalOrderSetting = singleSettings.GlobalOrderSetting,
                GlobalReturnGoodsSetting = singleSettings.GlobalReturnGoodsSetting
            };
            return View(editGlobalSettingsModel);
        }

        /// <summary>
        /// 全局设置编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditGlobalSettings(EditGlobalSettingsModel model)
        {
            GlobalSettings settings = model.AsGlobalSettings();
            GlobalSettingsSingle.Instance().SetGlobalSettings(settings);
            GlobalSettings resultEntity = globalSettingsService.EditGlobalSettings(settings);
            if (resultEntity != null)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "保存成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "保存失败！"));
        }
        #endregion

        #region 用户等级
        /// <summary>
        /// 用户等级管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [OperationLogs(true, Name = "用户等级", Type = PermissionItemsType.LeftMenu)]
        public IActionResult ManageUserRanks(int? pageIndex, int pageSize = 20)
        {
            NameValueCollection from = RequestHelper.GetQueryParams(Request);
            UserRanksQuery userRanksQuery = new UserRanksQuery();
            userRanksQuery.Rank = from["Rank"];
            userRanksQuery.RankName = from["RankName"];
            userRanksQuery.PointLower = from["PointLower"];

            if (!string.IsNullOrWhiteSpace(userRanksQuery.Rank))
                ViewData["Rank"] = userRanksQuery.Rank;
            if (!string.IsNullOrWhiteSpace(userRanksQuery.RankName))
                ViewData["RankName"] = userRanksQuery.RankName;
            if (!string.IsNullOrWhiteSpace(userRanksQuery.PointLower))
                ViewData["PointLower"] = userRanksQuery.PointLower;


            pageIndex = pageIndex ?? 1;
            userRanksQuery.UserRanksSortBy = UserRanksSortBy.Rank;

            PagingDataSet<UserRanks> userRanksPaging = userRanksService.GetUserRanks(userRanksQuery, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (userRanksPaging == null || userRanksPaging.Count() == 0))
                userRanksPaging = userRanksService.GetUserRanks(userRanksQuery, pageSize, pageIndex.Value - 1);

            return View(userRanksPaging);
        }
        /// <summary>
        /// 用户等级编辑展示数据
        /// </summary>
        /// <param name="Rank"></param>
        /// <returns></returns>
        public IActionResult _EditUserRanks(int Rank)
        {
            if (Rank <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该等级！"));
            UserRanks userRanks = userRanksService.GetFullUserRanks(Rank);
            EditUserRanksModel model = new EditUserRanksModel()
            {
                PointLower = userRanks.PointLower,
                RankName = userRanks.RankName
            };
            return View(model);
        }

        /// <summary>
        /// 用户等级编辑保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditUserRanks(EditUserRanksModel model)
        {
            if (model.Rank <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该等级！"));
            UserRanks userRanks = userRanksService.GetFullUserRanks(model.Rank);
            userRanks.RankName = model.RankName;
            userRanks.PointLower = model.PointLower;

            bool result = userRanksService.EditUserRanks(userRanks);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "编辑成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "编辑失败！"));
        }

        #endregion

    }

}
