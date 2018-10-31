using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using SexyColor.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Threading.Tasks;

namespace SexyColor.Web.Controllers
{
    [Authorize(Policy = "EnterSystem")]
    [Permissions]
    public class SystemOrderController : Controller
    {
        //用户服务
        public IUserService userService { get; set; }
        //用户地址服务
        public IUserAddressService userAddressService { get;set;}
        //会员服务
        public IMembershipService membershipService { get; set; }
        //订单服务
        public IOrderInfoService orderInfoService { get; set; }
        //订单行服务
        public IOrderLineService orderLineService { get; set; }
        //订单支付记录服务
        public IOrderPayLogsService orderPayLogsService { get; set; }
        //订单变更状态记录服务
        public IOrderStatusLogsService orderStatusLogsService { get; set; }


        #region 管理订单
        /// <summary>
        /// 管理订单
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="type"></param>
        /// <param name="mode"></param>
        /// <param name="rights"></param>
        /// <param name="sort"></param>
        /// <param name="status"></param>
        /// <param name="pay"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult ManageOrders(KeyWordEnum keyword, OrderType? type, OrderLogisticsMode? mode, OrderRightsStatus? rights, OrderInfoSortBy? sort, OrderModifyStatus? status, OrderPayType? pay, int? pageIndex, int pageSize = 20)
        {
            var from = RequestHelper.GetQueryParams(Request);
            ViewData["currentUser"] = UserContext.CurrentUser;
            string keyvalue = from["keyvalue"];
            OrderInfoQuery query = new OrderInfoQuery();
            var isUse = from["isUse"];
            var isCompleteComment = from["isCompleteComment"];
            if (!string.IsNullOrWhiteSpace(isUse))
                query.IsUse = isUse.AsBool();
            if (!string.IsNullOrWhiteSpace(isCompleteComment))
                query.IsCompleteComment = isCompleteComment.AsBool();

            if (!string.IsNullOrWhiteSpace(keyvalue))
            {
                ViewData["Keyvalue"] = keyvalue;
                switch (keyword)
                {
                    case KeyWordEnum.Buyers:
                        query.Buyers = from["keyvalue"];
                        break;
                    case KeyWordEnum.BuyersPhone:
                        query.BuyersPhone = from["keyvalue"];
                        break;
                    case KeyWordEnum.OrderNumber:
                        query.OrderNumber = from["keyvalue"];
                        break;
                    case KeyWordEnum.WaybillNumber:
                        query.WaybillNumber = from["keyvalue"];
                        break;
                    case KeyWordEnum.OtherOrderNumber:
                        query.OtherOrderNumber = from["keyvalue"];
                        break;
                }
            }
            

            if (sort.HasValue)
            {
                query.OrderInfoSortBy = sort.Value;
                ViewData["Sort"] = query.OrderInfoSortBy;
            }
            decimal result = decimal.Zero;
            if (decimal.TryParse(from["realPriceLowerLimit"], out result))
            {
                query.RealPriceLowerLimit = from["realPriceLowerLimit"].AsDecimal();
                ViewData["RealPriceLowerLimit"] = query.RealPriceLowerLimit;
            }

            if (decimal.TryParse(from["realPriceUpperLimit"], out result))
            {
                query.RealPriceUpperLimit = from["realPriceUpperLimit"].AsDecimal();
                ViewData["RealPriceUpperLimit"] = query.RealPriceUpperLimit;
            }

            DateTime dateResult = DateTime.MinValue;
            if (DateTime.TryParse(from["orderTimeLowerLimit"], out dateResult))
            {
                query.OrderTimeLowerLimit = dateResult;
                ViewData["OrderTimeLowerLimit"] = query.OrderTimeLowerLimit;
            }

            if (DateTime.TryParse(from["orderTimeUpperLimit"], out dateResult))
            {
                query.OrderTimeUpperLimit = dateResult;
                ViewData["OrderTimeUpperLimit"] = query.OrderTimeUpperLimit;
            }

            if (!string.IsNullOrWhiteSpace(query.OrderNumber))
                ViewData["OrderNumber"] = query.OrderNumber;
            if (!string.IsNullOrWhiteSpace(query.WaybillNumber))
                ViewData["WaybillNumber"] = query.WaybillNumber;


            Dictionary<int, string> keywordValues = Utility.GetDictionaryByEnumMemberInfo(typeof(KeyWordEnum));
            ViewData["Keyword"] = new SelectList(keywordValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", query.KeyWordEnum);

            //订单类型
            if (type.HasValue)
                query.OrderType = type;
            Dictionary<int, string> typeValues = Utility.GetDictionaryByEnumMemberInfo(typeof(OrderType));
            ViewData["Type"] = new SelectList(typeValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", query.OrderType);

            //维权方式
            if (rights.HasValue)
                query.OrderRightsStatus = rights;
            Dictionary<int, string> rightsValues = Utility.GetDictionaryByEnumMemberInfo(typeof(OrderRightsStatus));
            ViewData["Rights"] = new SelectList(rightsValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", query.OrderRightsStatus);

            //物流方式
            if (mode.HasValue)
                query.OrderType = type;
            Dictionary<int, string> logisticsValues = Utility.GetDictionaryByEnumMemberInfo(typeof(OrderLogisticsMode));
            ViewData["Logistics"] = new SelectList(logisticsValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", query.OrderLogisticsMode);

            if (status.HasValue)
            {
                query.OrderInfoStatus = status;
                ViewData["StatusValue"] = (int)status.Value;
            }
            else
            {
                ViewData["StatusValue"] = 0;
            }
            Dictionary<int, string> statusValues = Utility.GetDictionaryByEnumMemberInfo(typeof(OrderModifyStatus));
            ViewData["Status"] = new SelectList(statusValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", query.OrderInfoStatus);

            if (pay.HasValue)
                query.OrderPayType = pay;
            Dictionary<int, string> payValues = Utility.GetDictionaryByEnumMemberInfo(typeof(OrderPayType));
            ViewData["Pay"] = new SelectList(payValues.Select(w => new { text = w.Value, value = w.Key }), "value", "text", query.OrderPayType);
 

            Dictionary<bool, string> commentValues = new Dictionary<bool, string> { { true, "已完成评价" }, { false, "未完成评价" } };
            ViewData["IsCompleteComment"] = new SelectList(commentValues.Select(w => new { text = w.Value, value = w.Key.ToString().ToLower() }), "value", "text", query.IsCompleteComment);


            Dictionary<bool, string> useValues = new Dictionary<bool, string> { { true, "已使用优惠券" }, { false, "未使用优惠券" } };
            ViewData["IsUse"] = new SelectList(useValues.Select(w => new { text = w.Value, value = w.Key.ToString().ToLower() }), "value", "text", query.IsUse);


            pageIndex = pageIndex ?? 1;
            PagingDataSet<OrderInfo> paging = orderInfoService.GetPageOrderInfo(query, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = orderInfoService.GetPageOrderInfo(query, pageSize, pageIndex.Value - 1);
            return View(paging);
        }

        /// <summary>
        /// 查看支付记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult SeePayLogs(long orderId, int? pageIndex, int pageSize = 5)
        {
            if (orderId == 0)
                return Json(new StatusMessageData(StatusMessageType.Error, "没有此订单！"));

            pageIndex = pageIndex ?? 1;

            var result = orderPayLogsService.IsExistsPayLogs(orderId);
            if (!result)
                return Json(new StatusMessageData(StatusMessageType.Error, "该订单还未有支付记录！"));

            PagingDataSet<OrderPayLogs> paging = orderPayLogsService.GetPageOrderPayLogs(orderId, pageIndex.Value, pageSize);

            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = orderPayLogsService.GetPageOrderPayLogs(orderId, pageSize, pageIndex.Value - 1);
            return View("_PayLogs", paging);
        }

        /// <summary>
        /// 查看状态记录
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult SeeStatusLogs(long orderId, int? pageIndex, int pageSize = 5)
        {
            if (orderId == 0)
                return Json(new StatusMessageData(StatusMessageType.Error, "没有此订单！"));

            pageIndex = pageIndex ?? 1;

            var result = orderStatusLogsService.IsExistsStatusLogs(orderId);
            if (!result)
                return Json(new StatusMessageData(StatusMessageType.Error, "该订单还未有状态变更记录！"));

            PagingDataSet<OrderStatusLogs> paging = orderStatusLogsService.GetPageOrderStatusLogs(orderId, pageIndex.Value, pageSize);

            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = orderStatusLogsService.GetPageOrderStatusLogs(orderId, pageSize, pageIndex.Value - 1);
            return View("_StatusLogs", paging);
        }

        /// <summary>
        /// 查看物流跟踪记录
        /// </summary>
        /// <returns></returns>
        public IActionResult SeeLogistics(string logisticsCode, string waybillNumber)
        {
            CaiNiaoManager cainiao = new CaiNiaoManager();
            Task<string> task = cainiao.ExecuteInstantQueryAPI(logisticsCode, waybillNumber);
            return View(task.Result);
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OrdersDetails(long orderId, long userId)
        {
            OrderInfo orderInfoEntity = orderInfoService.GetOrderInfoByOrderid(orderId);
            IEnumerable<OrderLine> orderLineEntitys = orderLineService.GetOrderLineEntitysByOrderid(orderId);
            UserAddress userAddressEntity = userAddressService.GetFullUserAddress(orderInfoEntity.Addressid);
            OrderDetailsModel model = new OrderDetailsModel(orderInfoEntity, orderLineEntitys, userAddressEntity);
            return View(model);
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SandChangeOrder(long orderId, string logisticsCode, string waybillNumber)
        {
            OrderChangeStatus changeStatus = OrderChangeStatus.UnknownFailure;
            orderInfoService.SandOrder(orderId, UserContext.CurrentUser.UserId, out changeStatus, logisticsCode, waybillNumber);

            StatusMessageData statusMessageData = null;
            switch (changeStatus)
            {
                case OrderChangeStatus.UnknownFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，发货时产生了一个未知错误");
                    break;
                case OrderChangeStatus.StatusException:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，当前订单状态与该业务不符");
                    break;
                case OrderChangeStatus.OperationNULL:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，未能获取发货操作员信息，请重新登录再尝试");
                    break;
                case OrderChangeStatus.LogisticsCodeOrWaybillNumberNULL:
                    statusMessageData = new StatusMessageData(StatusMessageType.Error, "对不起，请确保运单号和快递公司都填写正确");
                    break;
                default:
                    break;
            }

            return View();
        }




        #endregion

    }
}
