using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SwaggerWebapi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SwaggerWebapi.Controllers.Temps.V2
{
    /// <summary>
    /// ValuesController
    /// </summary>
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/ValuesV2")]
    public class ValuesV2Controller : ControllerBase
    {
        ///  <summary>
        ///  GET api/values
        ///  </summary>
        /// <remarks>
        /// eg:
        ///      Get:api/value/1
        ///  </remarks>
        ///  <param name="sign">sign</param>
        ///  <param name="timestamp">timestamp</param>
        ///  <returns></returns>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("V2Get/{sign}/{timestamp}")]
        [SwaggerResponse(200, "this is useinfo!", typeof(UseInfo))]
        public ActionResult<IEnumerable<UseInfo>> V2Get(int sign, int timestamp)
        {
            return new[]
            {
                new UseInfo(){Id = Guid.NewGuid(), Name = "张三", Age = 11, Sign = sign},
                new UseInfo(){Id = Guid.NewGuid(), Name = "李四", Age = 22, Sign = sign}
            };
        }

        ///  <summary>
        ///  postPost
        ///  </summary>
        /// <remarks>
        /// eg:
        ///      Get:api/value/1
        ///  </remarks>
        ///  <param name="sign">sign</param>
        ///  <param name="timestamp">timestamp</param>
        ///  <returns></returns>
        [HttpPost]
        [MapToApiVersion("2.0")]
        [Route("V2Post/{sign}")]
        [SwaggerResponse(200, "this is useinfo!", typeof(UseInfo))]
        public ActionResult<IEnumerable<UseInfo>> V2Post([FromBody]int sign)
        {
            return new[]
            {
                new UseInfo(){Id = Guid.NewGuid(), Name = "张三", Age = 11, Sign = sign},
                new UseInfo(){Id = Guid.NewGuid(), Name = "李四", Age = 22, Sign = sign}
            };
        }
    }
}
