using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SexyColor.Api.Business.v1_0_0;
using SexyColor.CommonComponents;
using SexyColor.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SexyColor.Api.Controllers.v1_0_0
{
    [Route("api/v1/[controller]")]
    public class CategoryController : Controller
    {

        private CategoryMiddlewareBusiness categoryMiddleware;

        public CategoryController(CategoryMiddlewareBusiness categoryMiddleware)
        {
            this.categoryMiddleware = categoryMiddleware;
        }


        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var categpry = await categoryMiddleware.GetCategory();
            return Json(categpry);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id, int? page, int pagesize = 10, string sort = "default")
        {
            var from = RequestHelper.GetQueryParams(Request);
            GoodsInfoCategoryParam param = new GoodsInfoCategoryParam();
            param.CategoryItemid = id;

            param.PageIndex = page ?? 1;
            param.PageSize = pagesize;
            param.Sort = SexyColor.CommonComponents.Utility.StringConvertToEnum<GoodsInfoCategoryAPISortBy>(sort);

            var brandResult = 0;
            if (int.TryParse(from["brand"], out brandResult))
                param.Brandsid = brandResult;

            var charaResult = 0;
            if (int.TryParse(from["chara"], out charaResult))
                param.Charaid = charaResult;

            var maxResult = 0;
            if (int.TryParse(from["max"], out maxResult))
                param.Max = maxResult;

            var minResult = 0;
            if (int.TryParse(from["min"], out minResult))
                param.Min = minResult;


            var goods = await categoryMiddleware.GetCategoryByParams(param);
            return Json(goods);
        }

        

    }
}
