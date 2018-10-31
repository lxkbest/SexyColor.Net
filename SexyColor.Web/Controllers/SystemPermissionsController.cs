using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SexyColor.Web.Controllers
{
    [Authorize(Policy = "EnterSystem")]
    [Permissions]
    public class SystemPermissionsController : Controller
    {
        //权限服务
        public IPermissionItemsService permissionItemsService { get; set; }
        //角色服务
        public RolesService rolesService { get; set; }

        #region 权限管理
        /// <summary>
        /// 权限管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ManagePermission()
        {
            IEnumerable<PermissionItems> models = permissionItemsService.GetPermissionItemsAll();
            return View(models);
        }

        /// <summary>
        /// 获取权限节点
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JsonPermission(string itemKey)
        {
            List<JsonPermissionModel> jsonCollection = new List<JsonPermissionModel>();
            string json = permissionItemsService.GetPermissionItemsByParentsid(itemKey);
            JArray jsonObj = JArray.Parse(json);
            foreach (JObject jObject in jsonObj)
            {
                JsonPermissionModel model = new JsonPermissionModel();
                model.name = jObject["itemname"].ToString();
                model.id = jObject["itemkey"].ToString();
                model.isParent = true;
                jsonCollection.Add(model);
            }

            return Json(jsonCollection);
        }

        /// <summary>
        /// 查看权限项详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _DetailsPermission(string itemKey)
        {
            PermissionItems item = permissionItemsService.GetPermissionItemsByItemkey(itemKey);
            PermissionItems parentsItem = permissionItemsService.GetPermissionItemsByItemkey(item.Parentsid);
            
            DetailsPermissionModel model = new DetailsPermissionModel();
            model.ToDetailsPermissionModel(item);
            model.ParentsName =  parentsItem is null ?  string.Empty : parentsItem.Itemname;
            return View(model);
        }

        /// <summary>
        /// 创建权限项控件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _CreatePermission()
        {
            CreatePermissionModel model = new CreatePermissionModel();
            return View(model);
        }

        /// <summary>
        /// 创建权限项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePermission(CreatePermissionModel model)
        {
            PermissionItems permission =  model.AsPermissionItems();
            permission.Itemkey = Guid.NewGuid().ToString();
            permission.DateCreated = DateTime.Now;
            permission.DateLastModified = DateTime.Now;

            int newActionValue =  Request.GetInt("IsNewAction", -1);
            if (newActionValue == 0)
                permission.IsNewAction = false;
            else if (newActionValue == 1)
                permission.IsNewAction = true;

            PermissionItemsCreateStatus status = PermissionItemsCreateStatus.UnknownFailure;
            permissionItemsService.CreatePermissionItems(permission, out status);
            StatusMessageData statusMessageData = null;
            switch (status)
            {
                case PermissionItemsCreateStatus.GuidFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，您输入的父级项不正确");
                    break;
                case PermissionItemsCreateStatus.UrlFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，您输入的权限Url不正确");
                    break;
                case PermissionItemsCreateStatus.UnknownFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "创建时，产生了一个未知错误");
                    break;
                default:
                    break;
            }
            if (statusMessageData == null)
                return Json(new StatusMessageData(StatusMessageType.Success, "创建成功！"));
            else
                return Json(statusMessageData);
        }

        /// <summary>
        /// 编辑权限项控件
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public IActionResult _EditPermission(string itemKey)
        {
            PermissionItems permission = permissionItemsService.GetPermissionItemsByItemkey(itemKey);
            PermissionItems parentsItem = permissionItemsService.GetPermissionItemsByItemkey(permission.Parentsid);

            EditPermissionModel model = new EditPermissionModel().ToDetailsPermissionModel(permission);
            model.ParentsName = parentsItem is null ? string.Empty : parentsItem.Itemname;


            #region 无法配合模型验证移除
            //Dictionary<int, string> newAtionValues = Utility.GetDictionaryByEnumMemberInfo(typeof(IsNewAtionEnum));
            //ViewData["IsNewAction"] = new SelectList(newAtionValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", model.IsNewAction);

            //Dictionary<int, string> enableValues = Utility.GetDictionaryByEnumMemberInfo(typeof(IsEnableEnum));
            //ViewData["IsEnable"] = new SelectList(enableValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", model.IsEnable);

            //Dictionary<int, string> permissionItemsValues = Utility.GetDictionaryByEnumMemberInfo(typeof(PermissionItemsType));
            //ViewData["ItemType"] = new SelectList(permissionItemsValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", model.ItemType);

            //Dictionary<int, string> applicationidValues = Utility.GetDictionaryByEnumMemberInfo(typeof(ApplicationidEnum));
            //ViewData["Applicationid"] = new SelectList(applicationidValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", model.Applicationid);
            #endregion

            return View(model);
        }

        /// <summary>
        /// 编辑权限项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditPermission(EditPermissionModel model)
        {
            PermissionItems permission = model.AsPermissionItems();
            permission.DateLastModified = DateTime.Now;
            
            PermissionItemsCreateStatus status = PermissionItemsCreateStatus.UnknownFailure;
            permissionItemsService.EditPermissionItems(permission, out status);

            StatusMessageData statusMessageData = null;
            switch (status)
            {
                case PermissionItemsCreateStatus.GuidFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，您输入的父级项不正确");
                    break;
                case PermissionItemsCreateStatus.UrlFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，您输入的权限Url不正确");
                    break;
                case PermissionItemsCreateStatus.UnknownFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "更新时，产生了一个未知错误");
                    break;
                default:
                    break;
            }
            if (statusMessageData == null)
                return Json(new StatusMessageData(StatusMessageType.Success, "编辑成功！"));
            else
                return Json(statusMessageData);
        }

        /// <summary>
        /// 删除权限项
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeletePermission(string itemKey)
        {
            PermissionItems permission = permissionItemsService.GetPermissionItemsByItemkey(itemKey);
            IEnumerable<string> strRoles = rolesService.GetRoles().Select(w => w.Rolename);
            bool result = permissionItemsService.DeletePermissionItems(permission, strRoles);

            StatusMessageData statusMessageData = new StatusMessageData(StatusMessageType.Error, "删除时，产生了一个未知错误");
            if (result)
                return Json(new StatusMessageData(StatusMessageType.Success, "删除成功！"));
            else
                return Json(statusMessageData);
        }
        #endregion
    }
}
