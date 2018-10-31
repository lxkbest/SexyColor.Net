using Microsoft.AspNetCore.Mvc;


namespace SexyColor.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult GlobalError()
        {
            return View();
        }

        public IActionResult SystemError(string errorMsg)
        {
            ViewData["errorMsg"] = errorMsg;
            return View();
        }
    }


}
