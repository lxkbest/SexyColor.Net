using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SexyColor.Api.Business.v1_0_0;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SexyColor.Api.Controllers.v1_0_0
{
    [Route("api/v1/[controller]")]
    public class SitesController : Controller
    {

        private SitesMiddlewareBusiness sitesMiddleware;

        public SitesController(SitesMiddlewareBusiness sitesMiddleware)
        {
            this.sitesMiddleware = sitesMiddleware;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var siteObj = sitesMiddleware.GetSitesDto();
            return Json(siteObj);
        }
    }
}
