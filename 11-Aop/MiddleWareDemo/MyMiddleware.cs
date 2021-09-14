using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddleWareDemo
{
    #region 22222

    //public class MyMiddleware : IMiddleware
    //{
    //    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    //    {
    //        // 这里不对 request 做任何处理,直接调用下一个中间件
    //        await next(context);
    //    }
    //}

    #endregion

    #region 33333333

    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        // 需要实现一个构造函数,参数为 RequestDelegate
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // 需要实现一个叫做 InvokeAsync 方法
        public async Task InvokeAsync(HttpContext context)
        {
            // 不处理任何 request, 直接调用下一个中间件
            await _next.Invoke(context);
        }
    }

    #endregion
}
