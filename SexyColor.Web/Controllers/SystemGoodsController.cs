using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using SexyColor.BusinessComponents;
using SexyColor.CommonComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Web.Controllers
{
    [Authorize(Policy = "EnterSystem")]
    [Permissions]
    public class SystemGoodsController : Controller
    {
        //商品信息服务
        public IGoodsInfoService goodsInfoService { get; set; }
        //商品属性服务
        public IGoodsSkuInfoService goodsSkuInfoService { get; set; }
        //商品分类服务
        public IGoodsCategoryLevelService goodsCategoryLevelService { get; set; }
        //用户服务
        public IUserService userService { get; set; }
        //会员服务
        public IMembershipService membershipService { get; set; }
        //商品出入库服务
        public IGoodsSkuInoutService goodsSkuInoutService { get; set; }
        //商品轮换图片服务
        public IGoodsInRotationImageService goodsInRotationImageService { get; set; }
        //商品详情图片服务
        public IGoodsInDetailImageService goodsInDetailImageService { get; set; }
        //商品浏览记录服务
        public IGoodsBrowseLogsService goodsBrowseLogsService { get; set; }
        //商品收藏记录服务
        public IGoodsCollectionLogsService goodsCollectionLogsService { get; set; }

        #region 管理商品

        /// <summary>
        /// 管理商品
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult ManageGoods(GoodsInfoSortBy? sort, int? pageIndex, int pageSize = 20, GoodsInfoStatus status = GoodsInfoStatus.Sale)
        {
            var from = RequestHelper.GetQueryParams(Request);
            ViewData["currentUser"] = UserContext.CurrentUser;

            GoodsInfoQuery query = new GoodsInfoQuery();
            query.Keyword = from["keyWord"];
            query.Keynum = from["keynum"];
            query.CategoryLavel = from["categoryLavel"];
            query.GoodsInfoStatus = status;
            ViewData["Status"] = status;

            if (sort.HasValue)
            {
                query.GoodsInfoSortBy = sort.Value;
                ViewData["Sort"] = query.GoodsInfoSortBy;
            }
            decimal result = decimal.Zero;
            if (decimal.TryParse(from["goodsPriceLowerLimit"], out result))
            {
                query.GoodsPriceLowerLimit = from["goodsPriceLowerLimit"].AsDecimal();
                ViewData["GoodsPriceLowerLimit"] = query.GoodsPriceLowerLimit;
            }

            if (decimal.TryParse(from["goodsPriceUpperLimit"], out result))
            {
                query.GoodsPriceUpperLimit = from["goodsPriceUpperLimit"].AsDecimal();
                ViewData["GoodsPriceUpperLimit"] = query.GoodsPriceUpperLimit;
            }

            int buyResult = 0;
            if (int.TryParse(from["buyCountLowerLimit"], out buyResult))
            {
                query.BuyCountLowerLimit = from["buyCountLowerLimit"].AsInt();
                ViewData["BuyCountLowerLimit"] = query.BuyCountLowerLimit;
            }

            if (int.TryParse(from["buyCountUpperLimit"], out buyResult))
            {
                query.BuyCountUpperLimit = from["buyCountTimeUpperLimit"].AsInt();
                ViewData["BuyCountUpperLimit"] = query.BuyCountUpperLimit;
            }

            if (!string.IsNullOrWhiteSpace(query.Keyword))
                ViewData["Keyword"] = query.Keyword;
            if (!string.IsNullOrWhiteSpace(query.Keynum))
                ViewData["Keynum"] = query.Keynum;

            pageIndex = pageIndex ?? 1;
            PagingDataSet<GoodsInfo> paging = goodsInfoService.GetPageGoodsInfo(query, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (paging == null || paging.Count() == 0))
                paging = goodsInfoService.GetPageGoodsInfo(query, pageSize, pageIndex.Value - 1);
            return View(paging);
        }

        /// <summary>
        /// 批量回收商品
        /// </summary>
        /// <returns></returns>
        public IActionResult RecycleGoods()
        {
            IEnumerable<long> goodsIds = Request.Gets<long>("customCheck");
            HttpContext.Session.Set<StatusMessageData>("StatusMessageData", new StatusMessageData(StatusMessageType.Error, "回收错误"));
            if (goodsIds != null && goodsIds.Count() > 0)
            {
                goodsInfoService.RecycleGoods(goodsIds);
                HttpContext.Session.Set<StatusMessageData>("StatusMessageData", new StatusMessageData(StatusMessageType.Success, "回收成功"));
            }
            return RedirectToAction("ManageGoods", "SystemGoods");
        }

        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <returns></returns>
        public IActionResult RecoveryGoods(long goodsId)
        {
            if (goodsId > 0)
            {
                goodsInfoService.RecoveryGoods(goodsId);
                return Json(new StatusMessageData(StatusMessageType.Success, "恢复成功"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "恢复失败"));
        }

        /// <summary>
        /// 下架商品
        /// </summary>
        /// <returns></returns>
        public IActionResult DownShelvesGoods(long goodsId)
        {
            GoodsInfo goodsInfo = goodsInfoService.GetGoodsInfo(goodsId);
            var result = goodsInfoService.UpDownShelvesGoods(goodsInfo, false);
            if (result != null)
                return Json(new StatusMessageData(StatusMessageType.Success, "下架成功"));
            return Json(new StatusMessageData(StatusMessageType.Error, "下架失败"));
        }

        /// <summary>
        /// 上架商品
        /// </summary>
        /// <returns></returns>
        public IActionResult UpShelvesGoods(long goodsId)
        {
            GoodsInfo goodsInfo = goodsInfoService.GetGoodsInfo(goodsId);
            if (goodsInfo.Stock > 0)
            {
                var result = goodsInfoService.UpDownShelvesGoods(goodsInfo, true);
                if (result != null)
                    return Json(new StatusMessageData(StatusMessageType.Success, "上架成功"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "上架失败"));
        }

        /// <summary>
        /// 新增商品页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddGoods()
        {
            return View();
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGoods([FromServices]IHostingEnvironment env, GoodsInfoModel model, IList<IFormFile> imgfiles, IList<IFormFile> skufiles)
        {

            const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
            const string saveSuffix = ".jpeg";
            foreach (var file in Request.Form.Files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == null)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件没有后缀"));
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不是图片"));
                if (file.Length > 1024 * 1024 * 2)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不能大于2M"));
            }

            var skuArr = Request.Get<string>("SkuArr");
            string[] skuNumArr = skuArr.Split(',');

            GoodsInfo infoModel = model.AsGoodsInfo();
            infoModel.Status = (int)GoodsInfoStatus.Shelves;
            infoModel.IsEnable = false;
            infoModel.DateCreated = DateTime.Now;
            infoModel.Userid = UserContext.CurrentUser.UserId;

            List<GoodsSkuInfo> skuModels = new List<GoodsSkuInfo>();
            List<GoodsInRotationImage> imgModels = new List<GoodsInRotationImage>();

            var index = 0;
            foreach (var skuNum in skuNumArr)
            {
                GoodsSkuInfo goodsSkuInfo = new GoodsSkuInfo();
                goodsSkuInfo.SkuName = Request.Get<string>($"SkuName{skuNum}");
                goodsSkuInfo.SkuOriginalPrice = Request.Get<string>($"SkuOriginalPrice{skuNum}").AsDecimal();
                goodsSkuInfo.SkuMaketPrice = Request.Get<decimal>($"SkuMaketPrice{skuNum}");
                goodsSkuInfo.SkuFactoryPrice = Request.Get<decimal>($"SkuFactoryPrice{skuNum}");
                goodsSkuInfo.SkuVipPrice = Request.Get<string>($"SkuVipPrice{skuNum}").AsDecimal();
                goodsSkuInfo.Stock = Request.Get<string>($"Stock{skuNum}").AsInt();
                goodsSkuInfo.Number = Request.Get<int>($"Number{skuNum}");
                goodsSkuInfo.IsDefault = Request.Get<bool>($"IsDefault{skuNum}");
                goodsSkuInfo.Status = (int)GoodsSkuInfoStatus.Show;
                IFormFile file = skufiles[index];
                goodsSkuInfo.SkuImage = await Utility.SaveImage(env, file, Request, saveSuffix);

                if (index == 0)
                {
                    infoModel.GoodsPrice = goodsSkuInfo.SkuMaketPrice;
                    infoModel.GoodsRealPrice = goodsSkuInfo.SkuFactoryPrice;
                }

                skuModels.Add(goodsSkuInfo);
                index++;
            }


            foreach (var img in imgfiles)
            {
                GoodsInRotationImage imgModel = new GoodsInRotationImage();
                var imgIndex = imgfiles.IndexOf(img);
                IFormFile file = imgfiles[imgIndex];
                imgModel.Number = imgIndex + 1;
                imgModel.GoodsRotationImage = await Utility.SaveImage(env, file, Request, saveSuffix);
                if (imgIndex == 0)
                    infoModel.ImageUrl = imgModel.GoodsRotationImage;
                imgModels.Add(imgModel);
            }
            var detailsImg = Request.Get<string>("goodsDetailsImg");
            string[] detailsImgArr = detailsImg.Split(',');

            if (goodsInfoService.ReleaseGoods(infoModel, skuModels, imgModels, detailsImgArr, UserContext.CurrentUser.UserId))
                return Json(new StatusMessageData(StatusMessageType.Success, "发布成功"));
            return Json(new StatusMessageData(StatusMessageType.Error, "发布失败"));
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _DetailsGoodsInfo(long goodsId)
        {
            GoodsInfo goodsInfo = goodsInfoService.GetGoodsInfo(goodsId);
            return View(goodsInfo);
        }

        /// <summary>
        /// 富文本编辑上传详情图片
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditorUpload([FromServices]IHostingEnvironment env)
        {
            var files = Request.Form.Files;
            UploadFileModel model = new UploadFileModel();
            model.data = new List<string>();
            model.errno = 0;

            const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
            const string saveSuffix = ".jpeg";
            foreach (var file in Request.Form.Files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == null)
                {
                    model.errno = 1;
                    return Json(model);
                }
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                {
                    model.errno = 2;
                    return Json(model);
                }
                if (file.Length > 1024 * 1024 * 2)
                {
                    model.errno = 3;
                    return Json(model);
                }
            }

            var index = 0;
            foreach (var def in files)
            {
                IFormFile file = files[index];
                var resutl = await Utility.SaveImage(env, file, Request, saveSuffix);
                model.data.Add(resutl);
                index++;
            }
            return Json(model);
        }

        /// <summary>
        /// 查看买家评论信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IActionResult _CommentsGoodsInfo(long goodsId)
        {
            return View();
        }

        /// <summary>
        /// 设置商品基本信息控件
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IActionResult _SetGoodsInfo(long goodsId)
        {
            GoodsInfo info = goodsInfoService.GetGoodsInfo(goodsId);
            SetGoodsInfoModel model = new SetGoodsInfoModel();
            model = model.ToSetGoodsInfoModel(info);
            GoodsCategoryLevel2 level2 = goodsCategoryLevelService.GetCategoryLevel2(model.CategoryLevel2Id);
            if (level2 != null)
                model.CategoryLevel2Name = level2.CategoryName;
            else
                model.CategoryLevel2Name = string.Empty;
            return View(model);
        }

        /// <summary>
        /// 设置商品基本信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IActionResult SetGoodsInfo(SetGoodsInfoModel model)
        {
            if (model == null || model.Goodsid <= 0)
                return Json(new StatusMessageData(StatusMessageType.Error, "数据不正确！"));
            StatusMessageData statusMessageData = null;
            GoodsInfo info = goodsInfoService.GetGoodsInfo(model.Goodsid);
            info = model.AsGoodsInfo(info);

            SetGoodsInfoStatus status = SetGoodsInfoStatus.UnknownFailure;
            goodsInfoService.SetGoodsInfo(info, out status);
            switch (status)
            {
                case SetGoodsInfoStatus.UnknownFailure:
                    statusMessageData = new StatusMessageData(StatusMessageType.Success, "发生未知错误！");
                    break;
                case SetGoodsInfoStatus.DataCatch:
                    statusMessageData = new StatusMessageData(StatusMessageType.Success, "数据发生异常！");
                    break;
                default:
                    break;
            }

            if (statusMessageData == null)
                return Json(new StatusMessageData(StatusMessageType.Success, "设置成功！"));
            else
                return Json(statusMessageData);
        }

        /// <summary>
        /// 设置商品属性控件
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IActionResult _SetGoodsSkuInfo(long goodsId)
        {
            List<SetGoodsSkuInfoModel> models = new List<SetGoodsSkuInfoModel>();
            var skuInfoEntitys = goodsSkuInfoService.GetGoodsSkuInfoByGoodsId(goodsId);
            if (skuInfoEntitys != null && skuInfoEntitys.Count() > 0)
            {
                foreach (var entity in skuInfoEntitys)
                {
                    SetGoodsSkuInfoModel model = new SetGoodsSkuInfoModel();
                    models.Add(model.ToSetGoodsInfoModel(entity));
                }
            }
            ViewData["Goodsid"] = goodsId;
            return View(models.Where(w => w.Status == (int)GoodsSkuInfoStatus.Show));
        }


        /// <summary>
        /// 设置商品属性
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetGoodsSkuInfo([FromServices]IHostingEnvironment env, IList<IFormFile> skufiles, long goodsId)
        {
            const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
            const string saveSuffix = ".jpeg";
            foreach (var file in Request.Form.Files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == null)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件没有后缀"));
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不是图片"));
                if (file.Length > 1024 * 1024 * 2)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不能大于2M"));
            }

            var skuArr = Request.Get<string>("SkuArr");
            string[] skuNumArr = skuArr.Split(',');

            GoodsInfo info = goodsInfoService.GetGoodsInfo(goodsId);
            List<GoodsSkuInfo> skuModels = new List<GoodsSkuInfo>();
            List<GoodsSkuInout> skuinoutModels = new List<GoodsSkuInout>();
            var index = 0;
            var imgIndex = 0;
            foreach (var skuNum in skuNumArr)
            {
                GoodsSkuInfo goodsSkuInfo = new GoodsSkuInfo();
                goodsSkuInfo.Skuid = Request.Get<int>($"Skuid{skuNum}");
                goodsSkuInfo.SkuName = Request.Get<string>($"SkuName{skuNum}");
                goodsSkuInfo.SkuOriginalPrice = Request.Get<decimal>($"SkuOriginalPrice{skuNum}");
                goodsSkuInfo.SkuMaketPrice = Request.Get<decimal>($"SkuMaketPrice{skuNum}");
                goodsSkuInfo.SkuFactoryPrice = Request.Get<decimal>($"SkuFactoryPrice{skuNum}");
                goodsSkuInfo.SkuVipPrice = Request.Get<decimal>($"SkuVipPrice{skuNum}");
                goodsSkuInfo.Stock = Request.Get<int>($"Stock{skuNum}");
                goodsSkuInfo.IsDefault = Request.Get<bool>($"IsDefault{skuNum}");
                goodsSkuInfo.Number = Request.Get<int>($"Number{skuNum}");
                goodsSkuInfo.Status = goodsSkuInfo.Status = (int)GoodsSkuInfoStatus.Show;
                goodsSkuInfo.SkuImage = Request.Get<string>($"SkuImage{skuNum}");
                goodsSkuInfo.Goodsid = goodsId;

                if (string.IsNullOrWhiteSpace(goodsSkuInfo.SkuImage))
                {
                    if (skufiles.Count > 0)
                    {
                        IFormFile file = skufiles[imgIndex - index];
                        goodsSkuInfo.SkuImage = await Utility.SaveImage(env, file, Request, saveSuffix);
                    }
                }
                else
                {
                    imgIndex++;
                }

                if (goodsSkuInfo.IsDefault)
                {
                    info.GoodsPrice = goodsSkuInfo.SkuMaketPrice;
                    info.GoodsRealPrice = goodsSkuInfo.SkuFactoryPrice;
                    info.ImageUrl = goodsSkuInfo.SkuImage;
                }
                skuModels.Add(goodsSkuInfo);
                index++;
            }
            if (goodsInfoService.SetGoodsSkuInfo(info, skuModels, UserContext.CurrentUser.UserId))
                return Json(new StatusMessageData(StatusMessageType.Success, "发布成功"));
            return Json(new StatusMessageData(StatusMessageType.Error, "设置失败"));
        }

        /// <summary>
        /// 删除商品属性（修改状态）
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ModifySkuStatus(int skuId, int checkSkuId)
        {
            StatusMessageData statusMessageData = null;
            goodsSkuInfoService.ModifySkuStatus(skuId, checkSkuId, UserContext.CurrentUser.UserId, GoodsSkuInfoStatus.Hide);
            if (statusMessageData == null)
                return Json(new StatusMessageData(StatusMessageType.Success, "设置成功！"));
            else
                return Json(statusMessageData);
        }

        /// <summary>
        /// 商品轮换图
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _SetGoodsRotationImage(long goodsId)
        {
            IEnumerable<GoodsInRotationImage> rotationLists = goodsInRotationImageService.GetAllGoodsRotationImage(goodsId);
            ViewData["goodsId"] = goodsId;
            return View(rotationLists);
        }

        /// <summary>
        /// 编辑商品轮换图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditGoodsRotationImage([FromServices]IHostingEnvironment env, IList<IFormFile> imgfile, long id, int number)
        {
            if (id <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该商品图片！"));
            const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
            const string saveSuffix = ".jpeg";
            foreach (var file in Request.Form.Files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == null)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件没有后缀"));
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不是图片"));
                if (file.Length > 1024 * 1024 * 2)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不能大于2M"));
            }

            GoodsInRotationImage rotationImg = goodsInRotationImageService.GetGoodsInRotationImage(id);

            if (number != rotationImg.Number)
            {
                rotationImg.Number = number;
            }

            if (imgfile.Count > 0)
            {
                IFormFile imgFile = imgfile[0];
                rotationImg.GoodsRotationImage = await Utility.SaveImage(env, imgFile, Request, saveSuffix);
            }

            bool result = goodsInRotationImageService.EditGoodsRotationImage(rotationImg);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "编辑成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "编辑失败！"));
        }

        /// <summary>
        /// 新增商品轮换图
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGoodsRotationImage([FromServices]IHostingEnvironment env, IList<IFormFile> addImgFiles, long goodsId, int number)
        {
            if (goodsId <= 0)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "找不到该商品！"));
            }
            else if (addImgFiles.Count <= 0)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "添加失败！"));
            }
            const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
            const string saveSuffix = ".jpeg";
            foreach (var file in Request.Form.Files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == null)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件没有后缀"));
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不是图片"));
                if (file.Length > 1024 * 1024 * 2)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不能大于2M"));
            }

            IEnumerable<GoodsInRotationImage> rotationLists = goodsInRotationImageService.GetAllGoodsRotationImage(goodsId);
            int imgMaxIndex = 0;
            if (rotationLists.Count() <= 0)
            {
                imgMaxIndex = 0;
            }
            else
            {
                imgMaxIndex = rotationLists.Max(m => m.Number);
            }

            List<bool> resultList = new List<bool>();

            foreach (var img in addImgFiles)
            {
                GoodsInRotationImage imgModel = new GoodsInRotationImage();
                var imgIndex = addImgFiles.IndexOf(img);
                IFormFile file = addImgFiles[imgIndex];
                imgModel.Number = ++imgMaxIndex;
                imgModel.Goodsid = goodsId;
                imgModel.GoodsRotationImage = await Utility.SaveImage(env, file, Request, saveSuffix);
                bool result = goodsInRotationImageService.AddGoodsInRotationImage(imgModel);
                resultList.Add(result);
            }
            if (resultList.All(m => m == true))
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "添加成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "添加失败！"));
        }

        /// <summary>
        /// 删除商品轮换图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteGoodsRotationImage(long id)
        {
            if (id <= 0)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "找不到该商品图片！"));
            }
            GoodsInRotationImage rotationImg = goodsInRotationImageService.GetGoodsInRotationImage(id);
            bool result = goodsInRotationImageService.DeleteGoodsRotationImage(rotationImg);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "删除成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "删除失败！"));
        }

        /// <summary>
        /// 商品详情图
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _SetGoodsDetailImage(long goodsId)
        {
            IEnumerable<GoodsInDetailImage> goodsInDetailImages = goodsInDetailImageService.GetAllGoodsInDetailImage(goodsId);
            ViewData["goodsId"] = goodsId;
            return View(goodsInDetailImages);
        }

        /// <summary>
        /// 编辑商品详情图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditGoodsInDetailImage([FromServices]IHostingEnvironment env, IList<IFormFile> imgfile, long id, int number)
        {
            if (id <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "找不到该商品详情图片！"));
            const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
            const string saveSuffix = ".jpeg";
            foreach (var file in Request.Form.Files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == null)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件没有后缀"));
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不是图片"));
                if (file.Length > 1024 * 1024 * 2)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不能大于2M"));
            }

            GoodsInDetailImage goodsInDetailImage = goodsInDetailImageService.GetSingleGoodsInDetailImage(id);

            if (number != goodsInDetailImage.Number)
            {
                goodsInDetailImage.Number = number;
            }

            if (imgfile.Count > 0)
            {
                IFormFile imgFile = imgfile[0];
                goodsInDetailImage.GoodsDetaiImage = await Utility.SaveImage(env, imgFile, Request, saveSuffix);
            }

            bool result = goodsInDetailImageService.EditGoodsInDetailImage(goodsInDetailImage);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "编辑成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "编辑失败！"));
        }

        /// <summary>
        /// 新增商品详情图
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGoodsInDetailImage([FromServices]IHostingEnvironment env, IList<IFormFile> addImgFiles, long goodsId, int number)
        {
            if (goodsId <= 0)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "找不到该商品！"));
            }
            else if (addImgFiles.Count <= 0)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "添加失败！"));
            }
            const string fileFilt = ".gif|.jpg|.jpeg|.png|.webp|";
            const string saveSuffix = ".jpeg";
            foreach (var file in Request.Form.Files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension == null)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件没有后缀"));
                if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不是图片"));
                if (file.Length > 1024 * 1024 * 2)
                    return Json(new StatusMessageData(StatusMessageType.Error, "上传的文件不能大于2M"));
            }

            IEnumerable<GoodsInDetailImage> detailImageLists = goodsInDetailImageService.GetAllGoodsInDetailImage(goodsId);

            int imgMaxIndex = 0;
            if (detailImageLists.Count() <= 0)
            {
                imgMaxIndex = 0;
            }
            else
            {
                imgMaxIndex = detailImageLists.Max(m => m.Number);
            }

            List<bool> resultList = new List<bool>();

            foreach (var img in addImgFiles)
            {
                GoodsInDetailImage imgModel = new GoodsInDetailImage();
                var imgIndex = addImgFiles.IndexOf(img);
                IFormFile file = addImgFiles[imgIndex];
                imgModel.Number = ++imgMaxIndex;
                imgModel.Goodsid = goodsId;
                imgModel.GoodsDetaiImage = await Utility.SaveImage(env, file, Request, saveSuffix);
                bool result = goodsInDetailImageService.AddGoodsInDetailImage(imgModel);
                resultList.Add(result);
            }
            if (resultList.All(m => m == true))
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "添加成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "添加失败！"));
        }

        /// <summary>
        /// 删除商品详情图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteGoodsInDetailImage(long id)
        {
            if (id <= 0)
            {
                return Json(new StatusMessageData(StatusMessageType.Error, "找不到该商品详情图片！"));
            }
            GoodsInDetailImage detailImage = goodsInDetailImageService.GetSingleGoodsInDetailImage(id);
            bool result = goodsInDetailImageService.DeleteGoodsInDetailImage(detailImage);
            if (result)
            {
                return Json(new StatusMessageData(StatusMessageType.Success, "删除成功！"));
            }
            return Json(new StatusMessageData(StatusMessageType.Error, "删除失败！"));
        }


        #endregion

        #region 管理分类

        /// <summary>
        /// 管理分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ManageCategoryLevel()
        {
            var model = goodsCategoryLevelService.GetCategoryLevel1All();
            return View(model);
        }

        /// <summary>
        /// 获取分类JSON
        /// </summary>
        /// <returns></returns>
        public JsonResult JsonCategory()
        {
            List<JsonPermissionModel> jsonCollection = new List<JsonPermissionModel>();
            string json = goodsCategoryLevelService.GetCategoryLevel1AllJson();
            JArray jsonObj = JArray.Parse(json);
            foreach (JObject jObject in jsonObj)
            {
                JsonPermissionModel model = new JsonPermissionModel();
                model.name = jObject["category_name"].ToString();
                model.id = jObject["category_level1_id"].ToString();
                model.isParent = true;
                jsonCollection.Add(model);
            }

            return Json(jsonCollection);
        }

        /// <summary>
        /// 根据一级分类获取二级分类
        /// </summary>
        /// <param name="categoryLevelId"></param>
        /// <returns></returns>
        public JsonResult JsonCategory2(int categoryLevelId)
        {
            List<JsonPermissionModel> jsonCollection = new List<JsonPermissionModel>();
            string json = goodsCategoryLevelService.GetCategoryLevel2Json(categoryLevelId);
            JArray jsonObj = JArray.Parse(json);
            foreach (JObject jObject in jsonObj)
            {
                JsonPermissionModel model = new JsonPermissionModel();
                model.name = jObject["category_name"].ToString();
                model.id = jObject["category_level2_id"].ToString();
                model.isParent = false;
                jsonCollection.Add(model);
            }

            return Json(jsonCollection);
        }

        /// <summary>
        /// 查看分类详情控件
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public IActionResult _DetailsCategoryLevel(int categoryId, CategoryLevel level)
        {
            CategoryLevelModel model = new CategoryLevelModel();
            if (level == CategoryLevel.Level1)
                model.ToCategoryLevel1Model(goodsCategoryLevelService.GetCategoryLevel1(categoryId));
            else
            {
                model.ToCategoryLevel2Model(goodsCategoryLevelService.GetCategoryLevel2(categoryId));
                model.ParentCategoryName = goodsCategoryLevelService.GetCategoryLevel1(model.ParentCategoryId.Value).CategoryName;
            }

            return View(model);
        }

        /// <summary>
        /// 创建分类控件
        /// </summary>
        /// <returns></returns>
        public IActionResult _CreateCategoryLevel()
        {
            ViewBag.SubjectTitle = "商品分类创建";
            CategoryLevelModel model = new CategoryLevelModel();
            return View("/Views/SystemGoods/_EditCategoryLevel.cshtml", model);
        }

        /// <summary>
        /// 编辑分类控件
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public IActionResult _EditCategoryLevel(int categoryId, CategoryLevel level)
        {
            ViewBag.SubjectTitle = "商品分类编辑";
            CategoryLevelModel model = new CategoryLevelModel();
            if (level == CategoryLevel.Level1)
                model.ToCategoryLevel1Model(goodsCategoryLevelService.GetCategoryLevel1(categoryId));
            else
            {
                model.ToCategoryLevel2Model(goodsCategoryLevelService.GetCategoryLevel2(categoryId));
                model.ParentCategoryName = goodsCategoryLevelService.GetCategoryLevel1(model.ParentCategoryId.Value).CategoryName;
            }
            return View(model);
        }

        /// <summary>
        /// 保存分类（新增编辑操作）
        /// </summary>
        /// <param name="env"></param>
        /// <param name="files"></param>
        /// <param name="model"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditCategoryLevelAsync([FromServices]IHostingEnvironment env, IFormFile files, CategoryLevelModel model, bool isUpdate)
        {
            var file = files;
            if (file != null)
            {
                var filePath = string.Format("/Uploads/Images/{0}/{1}/{2}/", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));
                var qiniuFilePath = string.Format("{0}/{1}/{2}/", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));
                if (!Directory.Exists(env.WebRootPath + filePath))
                    Directory.CreateDirectory(env.WebRootPath + filePath);
                long size = file.Length;
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
            if (isUpdate)
            {
                if (model.LevelEnum == CategoryLevel.Level1)
                {
                    GoodsCategoryLevel1 category = goodsCategoryLevelService.GetCategoryLevel1(model.CategoryId);
                    category.CategoryName = model.CategoryName;
                    category.Description = model.Description ?? string.Empty;
                    category.CategoryLevel1ImageUrl = model.ImageUrl ?? string.Empty;
                    goodsCategoryLevelService.UpdataCategoryLevel<GoodsCategoryLevel1>(category);
                    return Json(new StatusMessageData(StatusMessageType.Success, "保存成功"));
                }
                else if (model.LevelEnum == CategoryLevel.Level2)
                {
                    GoodsCategoryLevel2 category = goodsCategoryLevelService.GetCategoryLevel2(model.CategoryId);
                    var oldCategoryLevel2Id = category.CategoryLevel1Id;
                    category.CategoryName = model.CategoryName;
                    category.CategoryLevel1Id = model.ParentCategoryId.Value;
                    category.Description = model.Description ?? string.Empty;
                    category.CategoryLevel2ImageUrl = model.ImageUrl ?? string.Empty;
                    goodsCategoryLevelService.UpdataCategoryLevel<GoodsCategoryLevel2>(category, oldCategoryLevel2Id);
                    return Json(new StatusMessageData(StatusMessageType.Success, "保存成功"));
                }
            }
            else
            {
                if (model.LevelEnum == CategoryLevel.Level1)
                {
                    GoodsCategoryLevel1 category = GoodsCategoryLevel1.New();
                    category.CategoryName = model.CategoryName;
                    category.Description = model.Description ?? string.Empty;
                    category.CategoryLevel1ImageUrl = model.ImageUrl ?? string.Empty;
                    goodsCategoryLevelService.CreateCategoryLevel<GoodsCategoryLevel1>(category);
                    return Json(new StatusMessageData(StatusMessageType.Success, "保存成功"));
                }
                else if (model.LevelEnum == CategoryLevel.Level2)
                {
                    GoodsCategoryLevel2 category = GoodsCategoryLevel2.New();
                    category.CategoryName = model.CategoryName;
                    category.CategoryLevel1Id = model.ParentCategoryId.Value;
                    category.Description = model.Description ?? string.Empty;
                    category.CategoryLevel2ImageUrl = model.ImageUrl ?? string.Empty;
                    goodsCategoryLevelService.CreateCategoryLevel<GoodsCategoryLevel2>(category);
                    return Json(new StatusMessageData(StatusMessageType.Success, "保存成功"));
                }
            }

            return Json(new StatusMessageData(StatusMessageType.Error, "保存失败"));
        }
        #endregion

        #region 商品出入库
        /// <summary>
        /// 商品出入库管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [OperationLogs(true, Name = "商品出入库", Type = PermissionItemsType.LeftMenu)]
        public IActionResult ManageGoodsSkuInout(int? pageIndex, int pageSize = 20)
        {
            NameValueCollection from = RequestHelper.GetQueryParams(Request);
            GoodsSkuInoutQuery goodsSkuInoutQuery = new GoodsSkuInoutQuery();
            string goodsName = from["GoodsName"],
             skuName = from["SkuName"],
             inoutNumberMin = from["InoutNumberMin"],
              inoutNumberMax = from["InoutNumberMax"],
             isOut = from["IsOut"];

            if (!string.IsNullOrWhiteSpace(goodsName))
            {
                ViewData["GoodsName"] = goodsSkuInoutQuery.GoodsName = goodsName;
            }
            if (!string.IsNullOrWhiteSpace(skuName))
            {
                ViewData["SkuName"] = goodsSkuInoutQuery.SkuName = skuName;
            }

            if (!string.IsNullOrWhiteSpace(inoutNumberMin))
            {
                ViewData["InoutNumberMin"] = goodsSkuInoutQuery.InoutNumberMin = inoutNumberMin.AsInt();
            }

            if (!string.IsNullOrWhiteSpace(inoutNumberMax))
            {
                ViewData["InoutNumberMax"] = goodsSkuInoutQuery.InoutNumberMax = inoutNumberMax.AsInt();
            }

            if (!string.IsNullOrWhiteSpace(isOut))
            {
                goodsSkuInoutQuery.IsOut = isOut.AsBool();
            }


            Dictionary<bool, string> isOUtnValues = new Dictionary<bool, string> { { true, "是" }, { false, "否" } };
            ViewData["IsOut"] = new SelectList(isOUtnValues.Select(m => new { text = m.Value, value = m.Key.ToString() }), "value", "text", goodsSkuInoutQuery.IsOut);

            pageIndex = pageIndex ?? 1;
            goodsSkuInoutQuery.GoodsSkuInoutSortBy = GoodsSkuInoutSortBy.DateCreated;

            PagingDataSet<GoodsSkuInout> goodsSkuInoutPaging = goodsSkuInoutService.GetGoodsSkuInout(goodsSkuInoutQuery, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (goodsSkuInoutPaging == null || goodsSkuInoutPaging.Count == 0))
                goodsSkuInoutPaging = goodsSkuInoutService.GetGoodsSkuInout(goodsSkuInoutQuery, pageSize, pageIndex.Value - 1);

            return View(goodsSkuInoutPaging);
        }

        #endregion

        #region 商品浏览记录
        /// <summary>
        /// 获取商品浏览记录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _GetGoodsBrowseLogs(long userId, int? pageIndex, int pageSize = 5)
        {
            if (userId == 0) return Json(new StatusMessageData(StatusMessageType.Error, "没有此用户！"));
            pageIndex = pageIndex ?? 1;
            PagingDataSet<GoodsBrowseLogs> goodsBrowseLogsPading = goodsBrowseLogsService.GetGoodsBrowseLogs(userId, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (goodsBrowseLogsPading == null || goodsBrowseLogsPading.Count() == 0))
                goodsBrowseLogsPading = goodsBrowseLogsService.GetGoodsBrowseLogs(userId, pageSize, pageIndex.Value - 1);

            if (goodsBrowseLogsPading.Count() <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "此用户没有商品浏览记录！"));

            return View(goodsBrowseLogsPading);
        }
        #endregion

        #region 商品收藏记录
        /// <summary>
        /// 获取商品收藏记录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _GetGoodsCollectionLogs(long userId, int? pageIndex, int pageSize = 5)
        {
            if (userId == 0) return Json(new StatusMessageData(StatusMessageType.Error, "没有此用户！"));
            pageIndex = pageIndex ?? 1;
            PagingDataSet<GoodsCollectionLogs> padingSet = goodsCollectionLogsService.GetGoodsCollectionLogs(userId, pageIndex.Value, pageSize);
            if (pageIndex > 1 && (padingSet == null || padingSet.Count() == 0))
                padingSet = goodsCollectionLogsService.GetGoodsCollectionLogs(userId, pageSize, pageIndex.Value - 1);

            if (padingSet.Count() <= 0) return Json(new StatusMessageData(StatusMessageType.Error, "此用户没有商品收藏记录！"));


            return View(padingSet);
        }
        #endregion


    }
}
