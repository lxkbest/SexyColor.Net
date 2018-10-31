using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SexyColor.Api.Controllers.v1_0_0
{
    [Authorize]
    [Route("api/v1/[controller]/[action]")]
    public class CommonsController : Controller
    {
        [HttpGet]
        public JsonResult Version()
        {
            return Json(new { ios_version = "1.0.0", android_version = "1.0.0" });
        }
    }
}
