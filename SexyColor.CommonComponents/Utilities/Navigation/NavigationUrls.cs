using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Specialized;
using System.Net;

namespace SexyColor.CommonComponents
{
    public class NavigationUrls
    {
        IStoreProvider storeProvider = DIContainer.Resolve<IStoreProvider>();
        private static volatile NavigationUrls _instance = null;
        private static readonly object lockObject = new object();

        public static NavigationUrls Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new NavigationUrls();
                    }
                }
            }
            return _instance;
        }

        private NavigationUrls()
        { }

        #region 系统后台
        /// <summary>
        /// 后台登录页
        /// </summary>
        /// <returns></returns>
        public string ManageLogin()
        {
            HttpContext httpContext = HttpContextCore.Current;
            string rawUrl = $"{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}";
            string returnUrl = NavigationUrls.ExtractQueryParams(rawUrl)["ReturnUrl"];
            if (string.IsNullOrWhiteSpace(returnUrl))
                returnUrl = WebUtility.UrlEncode(rawUrl);
            return CachedUrlHelper.Action("ManageLogin", "System", "System", new RouteValueDictionary() { { "ReturnUrl", returnUrl } });
        }

        /// <summary>
        /// 后台首页
        /// </summary>
        /// <returns></returns>
        public string ManageHome()
        {
            return CachedUrlHelper.Action("Home", "System", "System");
        }
        #endregion

        #region 用户相关


        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public string ManageUser()
        {
            return CachedUrlHelper.Action("ManageUser", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 激活操作
        /// </summary>
        /// <param name="isActivated"></param>
        /// <returns></returns>
        public string ActivatedUser(bool isActivated)
        {
            return CachedUrlHelper.Action("ActivatedUsers", "SystemUser", "SystemUser", new RouteValueDictionary() { { "isActivated", isActivated } });
        }

        /// <summary>
        /// 封禁操作
        /// </summary>
        /// <returns></returns>
        public string BannedUser()
        {
            return CachedUrlHelper.Action("_BannedUsers", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 取消封禁操作
        /// </summary>
        /// <returns></returns>
        public string UnBannedUser(string returnUrl)
        {
            return CachedUrlHelper.Action("UnBannedUsers", "SystemUser", "SystemUser", new RouteValueDictionary() { { "returnUrl", returnUrl } });
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string DeleteUser(long userId, string returnUrl)
        {
            return CachedUrlHelper.Action("DeleteUser", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId }, { "returnUrl", returnUrl } });
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public string AddUser()
        {
            return CachedUrlHelper.Action("_CreateUser", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <returns></returns>
        public string EditUser(long userId)
        {
            return CachedUrlHelper.Action("_EditUser", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId } });
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string SetRoles(long userId, string returnUrl)
        {
            return CachedUrlHelper.Action("_SetRoles", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId }, { "returnUrl", returnUrl } });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string ResetPassword(long userId)
        {
            return CachedUrlHelper.Action("ResetPassword", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId } });
        }

        /// <summary>
        /// 查询地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string QueryUserAddress(long userId)
        {
            return CachedUrlHelper.Action("ManageAddress", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId } });
        }

        /// <summary>
        /// 用户详细资料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string DetailsUserProfiles(long userId)
        {
            return CachedUrlHelper.Action("_DetailsUserProfiles", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId } });
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string SetHeadImage(long userId)
        {
            return CachedUrlHelper.Action("_SetHeadImage", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId } });
        }

        /// <summary>
        /// 保存头像
        /// </summary>
        /// <returns></returns>
        public string SaveHeadImage()
        {
            return CachedUrlHelper.Action("SaveHeadImage", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 查看粉丝
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string UserFollowed(long userId)
        {
            return CachedUrlHelper.Action("_UserFollowed", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId } });
        }

        /// <summary>
        /// 查看关注用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string UserFocus(long userId)
        {
            return CachedUrlHelper.Action("_UserFocus", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userId", userId } });
        }
        #endregion

        #region 角色相关
        /// <summary>
        /// 批量启用操作
        /// </summary>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public string EnabledRole(bool isEnabled)
        {
            return CachedUrlHelper.Action("EnabledRole", "SystemUser", "SystemUser", new RouteValueDictionary() { { "isEnabled", isEnabled } });
        }
        /// <summary>
        /// 批量公开操作
        /// </summary>
        /// <param name="isPublic"></param>
        /// <returns></returns>
        public string PublicRole(bool isPublic)
        {
            return CachedUrlHelper.Action("PublicRole", "SystemUser", "SystemUser", new RouteValueDictionary() { { "isPublic", isPublic } });
        }

        /// <summary>
        /// 用户角色-编辑用户
        /// </summary>
        /// <returns></returns>
        public string EditRoles(string roleName)
        {
            return CachedUrlHelper.Action("_EditRoles", "SystemUser", "SystemUser", new RouteValueDictionary() { { "roleName", roleName } });
        }

        /// <summary>
        /// 用户角色-删除角色
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string DeleteRoles(string roleName)
        {
            return CachedUrlHelper.Action("DeleteRoles", "SystemUser", "SystemUser", new RouteValueDictionary() { { "roleName", roleName } });
        }

        /// <summary>
        /// 用户角色-新增用户
        /// </summary>
        /// <returns></returns>
        public string AddRoles()
        {
            return CachedUrlHelper.Action("_AddRoles", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 用户角色-设置角色权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public string SetRolesPermission(string roleName, string sidebarUrl)
        {
            return CachedUrlHelper.Action("SetRolesPermission", "SystemUser", "SystemUser", new RouteValueDictionary() { { "roleName", roleName }, { "sidebarUrl", sidebarUrl } });
        }
        #endregion

        #region 权限相关
        /// <summary>
        /// 查看权限项详情
        /// </summary>
        /// <returns></returns>
        public string DetailsPermission(string itemKey)
        {
            return CachedUrlHelper.Action("_DetailsPermission", "SystemPermissions", "SystemPermissions", new RouteValueDictionary() { { "itemKey", itemKey } });
        }

        /// <summary>
        /// 创建权限项
        /// </summary>
        /// <returns></returns>
        public string CreatePermission()
        {
            return CachedUrlHelper.Action("_CreatePermission", "SystemPermissions", "SystemPermissions");
        }

        /// <summary>
        /// 编辑权限项
        /// </summary>
        /// <returns></returns>
        public string EditPermission(string itemKey)
        {
            return CachedUrlHelper.Action("_EditPermission", "SystemPermissions", "SystemPermissions", new RouteValueDictionary() { { "itemKey", itemKey } });
        }

        /// <summary>
        /// 删除权限项
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public string DeletePermission(string itemKey)
        {
            return CachedUrlHelper.Action("DeletePermission", "SystemPermissions", "SystemPermissions", new RouteValueDictionary() { { "itemKey", itemKey } });
        }

        /// <summary>
        /// 获取权限父级项下属集合Json
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public string JsonPermission(string itemKey)
        {
            return CachedUrlHelper.Action("JsonPermission", "SystemPermissions", "SystemPermissions", new RouteValueDictionary() { { "itemKey", itemKey } });
        }
        #endregion

        #region 操作日志
        /// <summary>
        /// 管理操作日志
        /// </summary>
        /// <returns></returns>
        public string ManagOperation()
        {
            return CachedUrlHelper.Action("ManagOperation", "SystemBasics", "SystemBasics");
        }

        /// <summary>
        /// 查看操作日志详情
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public string DetailsOperation(int operationId)
        {
            return CachedUrlHelper.Action("_DetailsOperation", "SystemBasics", "SystemBasics", new RouteValueDictionary() { { "operationId", operationId } });
        }
        #endregion

        #region 系统错误提示
        /// <summary>
        /// 全站错误提示
        /// </summary>
        /// <returns></returns>
        public string GlobalError()
        {
            return CachedUrlHelper.Action("GlobalError", "Error", "Error");
        }

        /// <summary>
        /// 后台系统错误提示
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public string SystemError(string errorMsg)
        {
            return CachedUrlHelper.Action("SystemError", "Error", "Error", new RouteValueDictionary() { { "errorMsg", errorMsg } });
        }
        #endregion

        #region 首页相关
        public string HomeLoopSetting()
        {
            return CachedUrlHelper.Action("HomeLoopSetting", "SystemSettings", "SystemSettings");
        }


        /// <summary>
        /// 调整排序
        /// </summary>
        /// <param name="homeId"></param>
        /// <param name="typeEnum"></param>
        /// <param name="isUp"></param>
        /// <returns></returns>
        public string HomeDisplayOrder()
        {
            return CachedUrlHelper.Action("HomeDisplayOrder", "SystemSettings", "SystemSettings");
        }

        /// <summary>
        /// 调整排序
        /// </summary>
        /// <param name="homeId"></param>
        /// <param name="typeEnum"></param>
        /// <param name="isUp"></param>
        /// <returns></returns>
        public string HomeDisplayOrder(int homeId, DynamicSettingsType typeEnum, bool isUp)
        {
            return CachedUrlHelper.Action("HomeDisplayOrder", "SystemSettings", "SystemSettings", new RouteValueDictionary() { { "homeId", homeId }, { "type", (int)typeEnum }, { "isUp", isUp } });
        }
        #endregion

        #region 商品相关
        /// <summary>
        /// 商品管理
        /// </summary>
        /// <returns></returns>
        public string ManageGoods()
        {
            return CachedUrlHelper.Action("ManageGoods", "SystemGoods", "SystemGoods");
        }
        /// <summary>
        /// 商品管理（按状态）
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string ManageGoods(GoodsInfoStatus status)
        {
            return CachedUrlHelper.Action("ManageGoods", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "status", status } });
        }

        /// <summary>
        /// 回收商品
        /// </summary>
        /// <returns></returns>
        public string RecycleGoods()
        {
            return CachedUrlHelper.Action("RecycleGoods", "SystemGoods", "SystemGoods");
        }

        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <returns></returns>
        public string RecoveryGoods(long goodsId)
        {
            return CachedUrlHelper.Action("RecoveryGoods", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }

        /// <summary>
        /// 下架商品
        /// </summary>
        /// <returns></returns>
        public string DownShelvesGoods(long goodsId)
        {
            return CachedUrlHelper.Action("DownShelvesGoods", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }

        /// <summary>
        /// 上架商品
        /// </summary>
        /// <returns></returns>
        public string UpShelvesGoods(long goodsId)
        {
            return CachedUrlHelper.Action("UpShelvesGoods", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <returns></returns>
        public string AddGoods()
        {
            return CachedUrlHelper.Action("AddGoods", "SystemGoods", "SystemGoods");
        }
        /// <summary>
        /// 详情商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public string DetailsGoodsInfo(long goodsId)
        {
            return CachedUrlHelper.Action("_DetailsGoodsInfo", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <returns></returns>
        public string CreateGoodsInfo()
        {
            return CachedUrlHelper.Action("CreateGoodsInfo", "SystemGoods", "SystemGoods");
        }



        /// <summary>
        /// 查看评论
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public string CommentsGoodsInfo(long goodsId)
        {
            return CachedUrlHelper.Action("_CommentsGoodsInfo", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }

 
        /// <summary>
        /// 设置商品基本信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public string SetGoodsInfo(long goodsId)
        {
            return CachedUrlHelper.Action("_SetGoodsInfo", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }

        /// <summary>
        /// 设置商品属性
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public string SetGoodsSkuInfo(long goodsId)
        {
            return CachedUrlHelper.Action("_SetGoodsSkuInfo", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <returns></returns>
        public string ModifySkuStatus()
        {
            return CachedUrlHelper.Action("ModifySkuStatus", "SystemGoods", "SystemGoods");
        }

        /// <summary>
        /// 设置商品轮换图
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public string SetGoodsRotationImage(long goodsId)
        {
            return CachedUrlHelper.Action("_SetGoodsRotationImage", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "Goodsid", goodsId } });
        }

        /// <summary>
        /// 删除商品轮换图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteGoodsRotationImage(long id)
        {
            return CachedUrlHelper.Action("DeleteGoodsRotationImage", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "id", id } });
        }

        /// <summary>
        /// 商品详情图片上传
        /// </summary>
        /// <returns></returns>
        public string EditorUpload()
        {
            return CachedUrlHelper.Action("EditorUpload", "SystemGoods", "SystemGoods");
        }

        /// <summary>
        /// 删除商品详情图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteGoodsInDetailImage(long id)
        {
            return CachedUrlHelper.Action("DeleteGoodsInDetailImage", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "id", id } });
        }


        /// <summary>
        /// 设置商品详情图
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public string SetGoodsDetailImage(long goodsId)
        {
            return CachedUrlHelper.Action("_SetGoodsDetailImage", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "goodsId", goodsId } });
        }
        /// <summary>
        /// 查看商品浏览记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GoodsBrowseLogs(long userId)
        {
            return CachedUrlHelper.Action("_GetGoodsBrowseLogs", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "userId", userId } });
        }

        /// <summary>
        /// 查看商品收藏记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetGoodsCollectionLogs(long userId)
        {
            return CachedUrlHelper.Action("_GetGoodsCollectionLogs", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "userId", userId } });
        }

        #endregion

        #region 商品分类相关
        /// <summary>
        /// 创建分类
        /// </summary>
        /// <returns></returns>
        public string CreateCategoryLevel()
        {
            return CachedUrlHelper.Action("_CreateCategoryLevel", "SystemGoods", "SystemGoods");
        }

        /// <summary>
        /// 详情分类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public string DetailsCategoryLevel(int categoryId, CategoryLevel level)
        {
            return CachedUrlHelper.Action("_DetailsCategoryLevel", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "categoryId", categoryId }, { "level", level } });
        }
        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="LevelId"></param>
        /// <param name=""></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public string EditCategoryLevel(int categoryId, CategoryLevel level)
        {
            return CachedUrlHelper.Action("_EditCategoryLevel", "SystemGoods", "SystemGoods", new RouteValueDictionary() { { "categoryId", categoryId }, { "level", level } });
        }
        /// <summary>
        /// 获取分类JSON
        /// </summary>
        /// <returns></returns>
        public string JsonCategory()
        {
            return CachedUrlHelper.Action("JsonCategory", "SystemGoods", "SystemGoods");
        }

        /// <summary>
        /// 获取e二级分类JSON
        /// </summary>
        /// <returns></returns>
        public string JsonCategory2()
        {
            return CachedUrlHelper.Action("JsonCategory2", "SystemGoods", "SystemGoods");
        }


        #endregion

        #region 订单相关
        /// <summary>
        /// 订单管理
        /// </summary>
        /// <returns></returns>
        public string ManageOrders()
        {
            return CachedUrlHelper.Action("ManageOrders", "SystemOrder", "SystemOrder");
        }

        /// <summary>
        /// 订单管理
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string ManageOrders(int status)
        {
            return CachedUrlHelper.Action("ManageOrders", "SystemOrder", "SystemOrder", new RouteValueDictionary() { { "status", status } });
        }

        /// <summary>
        /// 查看支付记录
        /// </summary>
        /// <returns></returns>
        public string SeeOrderPayLogs(long orderId)
        {
            return CachedUrlHelper.Action("SeePayLogs", "SystemOrder", "SystemOrder", new RouteValueDictionary() { { "orderId", orderId } });
        }

        /// <summary>
        /// 查看状态变更记录
        /// </summary>
        /// <returns></returns>
        public string SeeOrderStatusLogs(long orderId)
        {
            return CachedUrlHelper.Action("SeeStatusLogs", "SystemOrder", "SystemOrder", new RouteValueDictionary() { { "orderId", orderId } });
        }

        /// <summary>
        /// 查看订单详情
        /// </summary>
        /// <returns></returns>
        public string OrdersDetails(long orderId, long userId)
        {
            return CachedUrlHelper.Action("OrdersDetails", "SystemOrder", "SystemOrder", new RouteValueDictionary() { { "orderId", orderId }, { "userId", userId } });
        }
        #endregion

        /// <summary>
        /// 获取Logo的URL
        /// </summary>
        /// <returns></returns>
        public string LogoUrl(string logoUrl, string commonTypeKey, string imageSizeTypeKey, bool enableClientCaching = true)
        {
            if (string.IsNullOrEmpty(logoUrl))
            {
                return WebSiteUtility.ResolveUrl("~/wwwroot/images/potu.jpg");
            }

            if (!logoUrl.ToLower().EndsWith(".gif") && !string.IsNullOrEmpty(imageSizeTypeKey) && imageSizeTypeKey != ImageSizeTypeKey.Instance().Original())
            {
                LogoSettings logoSettings = LogoSettings.GetRegisteredSettings(commonTypeKey);
                if (logoSettings != null && logoSettings.ImageSizeTypeKey != null && logoSettings.ImageSizeTypeKey.ContainsKey(imageSizeTypeKey))
                {
                    var imageSizeType = logoSettings.ImageSizeTypeKey[imageSizeTypeKey];
                    string sizeImageName = storeProvider.GetSizeImageName(logoUrl, imageSizeType.Key, imageSizeType.Value);

                    if (enableClientCaching)
                    {
                        var filePath = storeProvider.GetDirectlyUrl(sizeImageName);
                        return filePath;
                    }
                    else
                    {
                        return storeProvider.GetDirectlyUrl(sizeImageName, DateTime.Now);
                    }
                }
            }

            if (enableClientCaching)
            {
                return storeProvider.GetDirectlyUrl(logoUrl);
            }
            else
            {
                return storeProvider.GetDirectlyUrl(logoUrl, DateTime.Now);
            }
        }

        public static NameValueCollection ExtractQueryParams(string url)
        {
            int startIndex = url.IndexOf("?");
            NameValueCollection values = new NameValueCollection();
            if (startIndex <= 0)
                return values;

            string[] nameValues = url.Substring(startIndex + 1).Split('&');

            foreach (string s in nameValues)
            {
                string[] pair = s.Split('=');

                string name = pair[0];
                string value = string.Empty;

                if (pair.Length > 1)
                    value = pair[1];

                values.Add(name, value);
            }

            return values;
        }

        #region 用户地址

        /// <summary>
        /// 用户地址-获取省份
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string Getprovinces()
        {
            return CachedUrlHelper.Action("Getprovinces", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 用户地址-获取城市
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string GetCitys()
        {
            return CachedUrlHelper.Action("GetCitys", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 用户地址-获取区域
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string GetAreas()
        {
            return CachedUrlHelper.Action("GetAreas", "SystemUser", "SystemUser");
        }

        /// <summary>
        /// 用户地址-批量启用用户地址
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string EnableUserAddress(bool isEnabled)
        {
            return CachedUrlHelper.Action("EnableUserAddress", "SystemUser", "SystemUser", new RouteValueDictionary() { { "isEnabled", isEnabled } });
        }

        /// <summary>
        /// 用户地址-详情
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public string UserAddressDetails(int userAddressId)
        {
            return CachedUrlHelper.Action("_UserAddressDetails", "SystemUser", "SystemUser", new RouteValueDictionary() { { "userAddressId", userAddressId } });
        }

        #endregion

        #region 用户等级
        public string EditUserRanks(int rankId)
        {
            return CachedUrlHelper.Action("_EditUserRanks", "SystemBasics", "SystemBasics", new RouteValueDictionary() { { "Rank", rankId } });
        }
        #endregion

        #region 积分

        public string EditPointType(string typekey)
        {
            return CachedUrlHelper.Action("_EditPointType", "SystemPoint", "SystemPoint", new RouteValueDictionary() { { "Typekey", typekey } });
        }

        public string AddPointType()
        {
            return CachedUrlHelper.Action("_AddPointType", "SystemPoint", "SystemPoint", null);
        }

        public string EditPointItems(int itemKey)
        {
            return CachedUrlHelper.Action("_EditPointItems", "SystemPoint", "SystemPoint", new RouteValueDictionary() { { "Itemskey", itemKey } });
        }

        public string AddPointItems()
        {
            return CachedUrlHelper.Action("_AddPointItems", "SystemPoint", "SystemPoint", null);
        }


        #endregion

    }
}
