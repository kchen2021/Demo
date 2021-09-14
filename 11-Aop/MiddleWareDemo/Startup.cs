using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddleWareDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region 22222

            // 在服务容器中注册自定义中间件
            //services.AddSingleton<MyMiddleware>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 11111

            // 使用匿名函数实现一个内联中间件
            //app.Use(async (context, next) =>
            //{
            //    // 这里不对 request 做任何处理,直接调用下一个中间件
            //    await next.Invoke();
            //});

            #endregion

            #region 22222&33333333

            // 使用 UseMiddleware() 把自定义中间件添加到管道中
            app.UseMiddleware<MyMiddleware>();

            #endregion

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

        }
    }

}
