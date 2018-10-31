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
    public class SystemPointController : Controller
    {
        //用户积分服务
        public IPointLogsService pointLogsService { get; set; }
        //积分类型服务
        public IPointTypeService pointTypeService { get; set; }
        //积分项目服务
        public IPointItemsService pointItemsService { get; set; }

        /// <summary>
        /// 用户积分明细
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [OperationLogs(true, Name = "用户积分明细", Type = PermissionItemsType.LeftMenu)]
        public IActionResult PointLogs(int? pageIndex, int pageSize = 20)
        {
            NameValueCollection from = RequestHelper.GetQueryParams(Request);
            PointLogsQuery pointLogsQuery = new PointLogsQuery();
            string id = from["Id"],
             userId = from["UserId"],
             userName = from["UserName"],
             itemsName = from["ItemsName"],
             sexyPoints = from["SexyPoints"],
             experiencePoints = from["ExperiencePoints"],
             isIncome = from["IsIncome"],
             dateCreated = from["DateCreated"],
             dateOverdue = from["DateOverdue"];

            if (!string.IsNullOrWhiteSpace(userName))
            {
                ViewData["UserName"] = pointLogsQuery.UserName = userName;
            }
            if (!string.IsNullOrWhiteSpace(itemsName))
            {
                ViewData["ItemsName"] = pointLogsQuery.Itemsname = itemsName;
            }

            if (!string.IsNullOrWhiteSpace(sexyPoints))
            {
                ViewData["SexyPoints"] = pointLogsQuery.SexyPoints = sexyPoints.AsInt();
            }

            if (!string.IsNullOrWhiteSpace(experiencePoints))
            {
                ViewData["ExperiencePoints"] = pointLogsQuery.ExperiencePoints = experiencePoints.AsInt();
            }

            if (!string.IsNullOrWhiteSpace(isIncome))
            {
                pointLogsQuery.IsIncome = isIncome.AsBool();
            }

            if (!string.IsNullOrWhiteSpace(dateCreated))
            {
                pointLogsQuery.DateCreated = dateCreated.AsDateTime();
            }

            if (!string.IsNullOrWhiteSpace(dateOverdue))
            {
                pointLogsQuery.DateOverdue = dateOverdue.AsDateTime();
            }

            Dictionary<bool, string> builtinValues = new Dictionary<bool, string> { { true, "收入" }, { false, "支出" } };
            ViewData["IsIncome"] = new SelectList(builtinValues.Select(m => new { text = m.Value, value = m.Key.ToString().ToLower() }), "value", "text", pointLogsQuery.IsIncome);

            pageIndex = pageIndex ?? 1;
            pointLogsQuery.PointLogsSortBy = PointLogsSortBy.Id;

            PagingDataSet<PointLogs> pointLogsPaging = pointLogsService.GetPointLogs(pointLogsQuery, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (pointLogsPaging == null || pointLogsPaging.Count == 0))
                pointLogsPaging = pointLogsService.GetPointLogs(pointLogsQuery, pageSize, pageIndex.Value - 1);

            return View(pointLogsPaging);
        }
        /// <summary>
        /// 积分类型管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [OperationLogs(true, Name = "积分类型", Type = PermissionItemsType.LeftMenu)]
        public IActionResult ManagePointType(int? pageIndex, int pageSize = 20)
        {

            pageIndex = pageIndex ?? 1;

            PagingDataSet<PointType> pointTypePaging = pointTypeService.GetPointType(pageIndex.Value, pageSize);
            if (pageIndex > 1 && (pointTypePaging == null || pointTypePaging.Count == 0))
                pointTypePaging = pointTypeService.GetPointType(pageSize, pageIndex.Value - 1);

            return View(pointTypePaging);
        }



        /// <summary>
        /// 编辑积分类型展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _EditPointType(string Typekey)
        {
            if (string.IsNullOrWhiteSpace(Typekey)) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该积分类型！"));
            PointType pointType = pointTypeService.GetFullPointType(Typekey);
            EditPointTypeModel model = new EditPointTypeModel()
            {
                DisplayOrder = pointType.DisplayOrder,
                QuotaDay = pointType.QuotaDay,
                Typename = pointType.Typename,
                Description = pointType.Description
            };
            return View(model);
        }

        /// <summary>
        /// 编辑积分类型保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditPointType(EditPointTypeModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Typekey)) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该积分类型！"));
            PointType pointType = pointTypeService.GetFullPointType(model.Typekey);
            pointType.Typename = model.Typename;
            pointType.QuotaDay = model.QuotaDay;
            pointType.Description = model.Description;
            pointType.DisplayOrder = model.DisplayOrder;

            bool result = pointTypeService.EditPointType(pointType);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "编辑成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "编辑失败！"));
        }

        /// <summary>
        /// 添加积分类型展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _AddPointType()
        {
            return View();
        }

        /// <summary>
        /// 添加积分类型保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPointType(EditPointTypeModel model)
        {
            PointType pointType = PointType.New();
            pointType.Description = model.Description;
            pointType.DisplayOrder = model.DisplayOrder;
            pointType.QuotaDay = model.QuotaDay;
            pointType.Typename = model.Typename;
            pointType.Typekey = Guid.NewGuid().ToString();
            bool result = pointTypeService.AddPointType(pointType);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "添加成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "添加失败！"));
        }
        /// <summary>
        /// 积分项目管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OperationLogs(true, Name = "积分项目", Type = PermissionItemsType.LeftMenu)]
        [HttpGet]
        public IActionResult ManagePointItems(int? pageIndex, int pageSize = 20)
        {
            pageIndex = pageIndex ?? 1;

            PagingDataSet<PointItems> pointItemsPaging = pointItemsService.GetPointItems(pageIndex.Value, pageSize);
            if (pageIndex > 1 && (pointItemsPaging == null || pointItemsPaging.Count == 0))
                pointItemsPaging = pointItemsService.GetPointItems(pageSize, pageIndex.Value - 1);

            return View(pointItemsPaging);
        }

        /// <summary>
        /// 编辑积分项目展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _EditPointItems(int Itemskey)
        {
            if (Itemskey <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该积分项目！"));
            PointItems pointItem = pointItemsService.GetFullPointItems(Itemskey);
            EditPointItemsModel model = new EditPointItemsModel()
            {
                Applicationid = pointItem.Applicationid,
                Description = pointItem.Description,
                DisplayOrder = pointItem.DisplayOrder,
                ExperiencePoints = pointItem.ExperiencePoints,
                Itemstyle = pointItem.Itemstyle,
                Itemsname = pointItem.Itemsname,
                SexyPoints = pointItem.SexyPoints
            };
            IEnumerable<PointType> pointType = pointTypeService.GetPointTypeList();
            if (pointType != null)
                ViewData["PointType"] = new SelectList(pointType, "Typekey", "Typename", pointItem.Itemskey);
            return View(model);
        }

        /// <summary>
        /// 编辑积分项目展示保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditPointItems(EditPointItemsModel model)
        {
            if (model.Itemskey <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该积分项目！"));
            PointItems pointItems = pointItemsService.GetFullPointItems(model.Itemskey);

            pointItems.Itemsname = model.Itemsname;
            pointItems.Applicationid = model.Applicationid;
            pointItems.Description = model.Description;
            pointItems.DisplayOrder = model.DisplayOrder;
            pointItems.ExperiencePoints = model.ExperiencePoints;
            pointItems.Itemstyle = model.Itemstyle;
            pointItems.SexyPoints = model.SexyPoints;

            bool result = pointItemsService.EditPointItems(pointItems);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "编辑成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "编辑失败！"));
        }

        /// <summary>
        /// 添加积分项目展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _AddPointItems()
        {
            IEnumerable<PointType> pointType = pointTypeService.GetPointTypeList();
            if (pointType != null)
                ViewData["PointType"] = new SelectList(pointType, "Typekey", "Typename");
            return View();
        }

        /// <summary>
        /// 添加积分项目保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPointItems(EditPointItemsModel model)
        {
            PointItems pointItems = PointItems.New();
            pointItems.Description = model.Description;
            pointItems.ExperiencePoints = model.ExperiencePoints;
            pointItems.DisplayOrder = model.DisplayOrder;
            pointItems.Itemsname = model.Itemsname;
            pointItems.Itemstyle = model.Itemstyle;
            pointItems.SexyPoints = model.SexyPoints;
            pointItems.Applicationid = model.Applicationid;
            bool result = pointItemsService.AddPointItems(pointItems);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "添加成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "添加失败！"));
        }


    }
}
