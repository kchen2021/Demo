using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheDemo.Extensions.StartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CacheDemo.Extensions
{
    public static class StartupExtention
    {

        public static void AddStartupConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddMemoryCacheConfigureServices();
            services.AddDistributedSqlServerConfigureServices(Configuration);
        }

        public static void UseStartupConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseMemoryCacheConfigure(env);
            app.UseDistributedSqlServerConfigure(env);
        }
    }
}
