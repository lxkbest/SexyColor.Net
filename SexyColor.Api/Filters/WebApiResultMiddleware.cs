using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace SexyColor.Api
{
    public class WebApiResultMiddleware : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult.Value == null)
                    context.Result = new JsonResult(new { code = 404, msg = "Not Found", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented });
                else
                    context.Result = new JsonResult(new { code = 200, msg = "Success", result = objectResult.Value }, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new { code = 404, msg = "Not Found", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new JsonResult(new { code = 200, msg = "Success", result = (context.Result as ContentResult).Content }, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new JsonResult(new { code = (context.Result as StatusCodeResult).StatusCode, msg = "", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
            else if (context.Result is JsonResult)
            {
                context.Result = new JsonResult(new { code = 200, msg = "Success", result = (context.Result as JsonResult).Value }, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }
        }
    }
}
