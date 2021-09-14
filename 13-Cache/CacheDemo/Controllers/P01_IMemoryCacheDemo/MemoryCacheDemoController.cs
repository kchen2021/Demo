using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Controllers.P01_IMemoryCacheDemo
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryCacheDemoController : ControllerBase
    {
        private IMemoryCache cache;

        public MemoryCacheDemoController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpGet("{id}")]
        public ActionResult<string> SetId(string id)
        {
            var ret = cache.Set("userId", id);
            
            //MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
            //{
            //    Priority = CacheItemPriority.NeverRemove
            //};

            //var ret = cache.Set("userId", id, options);

            return Ok("");
        }

        [HttpGet()]
        public ActionResult<string> GetId()
        {
            return cache.Get<string>("userId");
        }


    }
}