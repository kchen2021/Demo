using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheDemo.Controllers.P02_Distributed_SqlServer
{
    [ApiController]
    public class SqlServerDemoController : ControllerBase
    {
        private IDistributedCache cache;

        public SqlServerDemoController(IDistributedCache cache)
        {
            this.cache = cache;
        }

        [Route("SqlServerDemo/Set")]
        public ActionResult Set()
        {
            var currentTime = DateTime.Now.ToString();
            cache.SetString("CurrentTime", currentTime);

            return Ok();
        }

        [Route("SqlServerDemo/Get")]
        public ActionResult Get()
        {
            var currentTime =  cache.GetString("CurrentTime");

            return Ok(currentTime);
        }
    }
}