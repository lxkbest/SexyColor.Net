using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SexyColor.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SexyColor.Api.Controllers.v1_0_0
{
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {

        //private readonly ICacheService cacheService = DIContainer.Resolve<ICacheService>();
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            ICacheService cacheService = DIContainer.Resolve<ICacheService>();
            cacheService.Add("name", "love", CachingExpirationType.SingleObject);

            var name = cacheService.Get("name");

            return new string[] { name.ToString(), "345" };
        }

 
    }
}
