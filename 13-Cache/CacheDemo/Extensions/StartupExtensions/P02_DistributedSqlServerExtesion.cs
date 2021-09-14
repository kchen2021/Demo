using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CacheDemo.Extensions.StartupExtensions
{
    public static class P02_DistributedSqlServerExtesion
    {
        //Microsoft.Extensions.Caching.SqlServer
        public static void AddDistributedSqlServerConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDistributedSqlServerCache(options =>
            {
                //options.SystemClock = new BLL.LocalSystemClock();
                options.ConnectionString = Configuration["ConnectionString"];
                options.SchemaName = "dbo";
                options.TableName = "AspNetCoreCache";
                options.DefaultSlidingExpiration = TimeSpan.FromMinutes(1);
                options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(5);
            });
        }

        public static void UseDistributedSqlServerConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
